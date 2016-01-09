using UnityEngine;
using System.Collections;

public class DebugPlayer : MonoBehaviour {

    public Player Player;

    // Adds one default player (when the level is not created from the menu)
	void Start () {
        GameManager.Instance.SetPlayer(0, 0);
        Player.GetComponent<SpriteRenderer>().color = GameManager.Instance.GetPlayer(0).SkinColor;
        Player.GetComponent<Player>().Index = 0;
	}

}
