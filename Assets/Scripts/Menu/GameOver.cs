using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject[] Cards;

    private bool _hidden = true;

    public void Start() {
        transform.Translate(0, -(float)Screen.height, 0);
    }

    public void Show() {
        transform.Translate(0, (float)Screen.height, 0);
        _hidden = false;
        for (int playerIndex = 0; playerIndex < GameManager.Instance.GetNextPlayerIndex(); ++playerIndex) {
            Cards[playerIndex].SetActive(true);
            Transform card = Cards[playerIndex].transform;
            card.Find("Image").GetComponent<Image>().color = GameManager.Instance.GetPlayer(playerIndex).SkinColor;
            card.Find("KillsText").GetComponent<Text>().text = "Kills: " + GameManager.Instance.GetPlayer(playerIndex).NumberOfKills;
            card.Find("DeathsText").GetComponent<Text>().text = "Deaths: " + GameManager.Instance.GetPlayer(playerIndex).NumberOfDeaths;
        }
        for (int playerIndex = GameManager.Instance.GetNextPlayerIndex(); playerIndex < GameManager.NumberOfPlayers; ++playerIndex) {
            Cards[playerIndex].SetActive(false);
        }
    }

	void Update () {
        if (_hidden) {
            return;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Cancel)) {
            GameManager.Instance.RemovePlayers();
            SceneManager.LoadScene("Menu");
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Submit)) {
            SceneManager.LoadScene("Runaway");
        }
	}
}
