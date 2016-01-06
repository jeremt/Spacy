using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour {

    public int Index = 0;
    private Image _playerImage;
    public Text Text;
    public Image DeviceImage;

    public Color[] _colors = new Color[] {
        new Color(1, 0, 0, 1),
        new Color(0, 1, 0, 1),
        new Color(0, 0, 1, 1)
    };

    public Sprite[] DevicesSprite;
    	
    void Awake() {
        _playerImage = transform.Find("PlayerImage").GetComponent<Image>();
    }

	// Update is called once per frame
	void Update() {
        PlayerData player = GameManager.Instance.GetPlayer(Index);
        if (player != null) {
            if (Input.GetButtonUp(player.InputIndex + "_Shoot")) { // Cancel
                _deselectPlayer();
            }
        } else {
            // Si tout les players avant moi sont pris, je selecte
            if (GameManager.Instance.GetNextPlayerIndex() == Index) {
                for (int inputIndex = 0; inputIndex < 2; ++inputIndex) {
                    if (Input.GetButtonUp(inputIndex + "_Jump") && GameManager.Instance.InputAvailable(inputIndex)) { // Submit
                        _selectPlayer(inputIndex);
                    }
                }
            }
        }
	}

    private void _selectPlayer(int inputIndex) {
        GameManager.Instance.SetPlayer(Index, new PlayerData(inputIndex, _colors[0]));
        _playerImage.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        Text.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        DeviceImage.overrideSprite = DevicesSprite[inputIndex];
    }

    private void _deselectPlayer() {
        _playerImage.color = new Color(1f, 1f, 1f, 125f/255f);
        Text.color = new Color(1f, 1f, 1f, 125f/255f);
        DeviceImage.overrideSprite = DevicesSprite[5];
        GameManager.Instance.RemovePlayer(Index);
    }

}
