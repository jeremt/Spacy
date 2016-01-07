using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour {

    public int Index = 0;
    public Image PlayerImage;
    public Text Text;
    public Image DeviceImage;
    public Sprite[] DevicesSprite;
    	
    void Start() {
        Text.text = "P" + (Index + 1);
    }

	// Update is called once per frame
	void Update() {
        PlayerData player = GameManager.Instance.GetPlayer(Index);
        if (player != null) {
            if (InputManager.Instance.GetKeyUp(InputAlias.Cancel, player.InputIndex)) {
                _deselectPlayer();
            }
        } else {
            if (GameManager.Instance.GetNextPlayerIndex() == Index) {
                for (int inputIndex = 0; inputIndex < InputManager.NumberOfInputs; ++inputIndex) {
                    if (InputManager.Instance.GetKeyUp(InputAlias.Submit, inputIndex) && GameManager.Instance.IsInputAvailable(inputIndex)) {
                        _selectPlayer(inputIndex);
                    }
                }
            }
        }
	}

    private void _selectPlayer(int inputIndex) {
        GameManager.Instance.SetPlayer(Index, inputIndex);
        PlayerImage.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        Text.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        DeviceImage.overrideSprite = DevicesSprite[inputIndex];
        Debug.Log("Player " + Index + " selected, inputIndex = " + inputIndex + " (available = " + GameManager.Instance.IsInputAvailable(inputIndex) + ")");
    }

    private void _deselectPlayer() {
        Debug.Log("Deselect player " + Index);
        PlayerImage.color = new Color(1f, 1f, 1f, 125f/255f);
        Text.color = new Color(1f, 1f, 1f, 125f/255f);
        DeviceImage.overrideSprite = DevicesSprite[5];
        GameManager.Instance.RemovePlayer(Index);
    }

}
