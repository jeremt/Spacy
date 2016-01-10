using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject[] Cards;
    public float AnimationDuration = 0.5f;
    public float DisabledTime = 3f;
    public bool _isMoving = false;
    private bool _hidden = true;
    private float _currentTime = 0f;
    private Vector3 _startPosition;

    public void Start() {
        transform.Translate(0, -(float)Screen.height, 0);
        gameObject.SetActive(false);
    }

    public void Show() {
        _currentTime = AnimationDuration;
        gameObject.SetActive(true);
        _isMoving = true;
        _startPosition = transform.position;
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
        if (_currentTime > 0) {
            if (_isMoving) {
                transform.position = Vector3.Lerp(_startPosition, _startPosition + Vector3.up * (float)Screen.height, _applyEasing(1f - _currentTime / AnimationDuration));
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0) {
                    _currentTime = DisabledTime;
                    _isMoving = false;
                }
            } else {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0) {
                    _hidden = false;
                }
            }
        }
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

    // CubicOut easing
    private float _applyEasing(float t) {
        float f = t - 1.0f;
        return f * f * f + 1.0f;
    }

}
