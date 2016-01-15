using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    // API
    public GameOver GameOverPopup;
    public SurvivalMode SurvivalMode;
    public TimeMode TimeMode;

    void Start() {
        var modeData = GameManager.Instance.GetMode();
        if (modeData == null) {
            GameManager.Instance.SetMode(0, 0); // default mode
            modeData = GameManager.Instance.GetMode();
        }
        if (modeData.Index == 0) {
            var mode = Instantiate(TimeMode, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as TimeMode;
            if (mode != null) {
                mode.CurrentLevel = this;
                var timeModeData = modeData as TimeModeData;
                if (timeModeData != null) {
                    mode.Duration = timeModeData.Duration;
                }
            }
        }
        if (modeData.Index == 1) {
            var mode = Instantiate(SurvivalMode, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as SurvivalMode;
            if (mode != null) {
                mode.CurrentLevel = this;
            }
        }
    }

    public void GameOver() {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players) {
            Destroy(player);
        }
        GameOverPopup.Show();
    }

}
