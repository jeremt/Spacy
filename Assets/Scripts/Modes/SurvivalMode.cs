using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivalMode : MonoBehaviour {

    public Level CurrentLevel;
    public GameObject[] LivesBars;

    private int _numberOfPlayers;

    void Start() {
        _numberOfPlayers = GameManager.Instance.GetNextPlayerIndex();
        for (int playerIndex = 0; playerIndex < _numberOfPlayers; ++playerIndex) {
            LivesBars[playerIndex].SetActive(true);
            _setLivesColor(LivesBars[playerIndex], GameManager.Instance.GetPlayer(playerIndex).SkinColor);
        }
    }

	void Update () {
        int numberOfPlayersAlive = 0;
        for (int playerIndex = 0; playerIndex < _numberOfPlayers; ++playerIndex) {
            int deaths = GameManager.Instance.GetPlayer(playerIndex).NumberOfDeaths;
            int lives = (GameManager.Instance.GetMode() as SurvivalModeData).NumberOfLives;
            Image[] images = LivesBars[playerIndex].GetComponentsInChildren<Image>();
            for (int i = lives - deaths; i < Mathf.Min(lives, images.Length); ++i) {
                images[i].gameObject.SetActive(false);
            }
            if (lives - deaths > 0) {
                numberOfPlayersAlive++;
            }
        }
        if (numberOfPlayersAlive < 2) {
            CurrentLevel.GameOver();
            gameObject.SetActive(false);
        }
	}

    private void _setLivesColor(GameObject lifeBar, Color color) {
        Image[] images = lifeBar.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; ++i) {
            if (i < (GameManager.Instance.GetMode() as SurvivalModeData).NumberOfLives) {
                images[i].color = color;
            } else {
                images[i].gameObject.SetActive(false);
            }
        }
    }

}
