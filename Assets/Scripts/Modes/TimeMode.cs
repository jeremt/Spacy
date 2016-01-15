using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeMode : MonoBehaviour {

    public Level CurrentLevel;
    public Text TimerText;
    public float Duration = 120f;

	void Update () {
        Duration -= Time.deltaTime;
        if(Duration < 0) {
            CurrentLevel.GameOver();
            gameObject.SetActive(false);
        } else {
            TimerText.text = string.Format("{0}:{1}", _addZero((int)(Duration / 60)), _addZero((int)(Duration % 60)));
        }
	}

    private string _addZero(int n) {
        return string.Format("{0}{1}", n > 9 ? "" : "0", n);
    }

}
