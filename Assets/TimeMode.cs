using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeMode : MonoBehaviour {

    public Text timerLabel;
    public float Duration = 120f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Duration -= Time.deltaTime;
        if(Duration < 0) {
            GameOver();
        } else {
            timerLabel.text = string.Format("{0}:{1}", _addZero((int)(Duration / 60)), _addZero((int)(Duration % 60)));
        }
	}

    void GameOver() {
    }

    private string _addZero(int n) {
        return string.Format("{0}{1}", n > 9 ? "" : "0", n);
    }

}
