using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour {

    public int Index = 0;
    public Image PlayerImage;
    public Text Text;
    public Image DeviceImage;
    public Sprite[] DevicesSprite;

    public void Start() {
        Text.text = "P" + (Index + 1);
    }

    public void SelectPlayer(int inputIndex) {
        GameManager.Instance.SetPlayer(Index, inputIndex);
        PlayerImage.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        Text.color = GameManager.Instance.GetPlayer(Index).SkinColor;
        DeviceImage.overrideSprite = DevicesSprite[inputIndex];
    }

    public void ChangePlayerSkin() {
        var player = GameManager.Instance.GetPlayer(Index);
        if (player != null) {
            GameManager.Instance.ChangePlayerColor(Index);
            PlayerImage.color = player.SkinColor;
            Text.color = player.SkinColor;
        }
    }

    public void DeselectPlayer() {
        if (GameManager.Instance.GetPlayer(Index) != null) {
            PlayerImage.color = new Color(1f, 1f, 1f, 125f/255f);
            Text.color = new Color(1f, 1f, 1f, 125f/255f);
            DeviceImage.overrideSprite = DevicesSprite[5];
            GameManager.Instance.RemovePlayer(Index);
        }
    }

}
