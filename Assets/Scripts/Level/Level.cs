using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    // API
    public SurvivalMode SurvivalMode;
    public TimeMode TimeMode;

    void Start() {
        ModeData modeData = GameManager.Instance.GetMode();
        if (modeData == null) {
            GameManager.Instance.SetMode(0, 0); // default mode
            modeData = GameManager.Instance.GetMode();
        }
        if (modeData.Index == 0) {
            TimeMode mode = Instantiate(TimeMode, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as TimeMode;
            mode.CurrentLevel = this;
            mode.Duration = (modeData as TimeModeData).Duration;
        }
        if (modeData.Index == 1) {
            SurvivalMode mode = Instantiate(SurvivalMode, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as SurvivalMode;
            mode.CurrentLevel = this;
        }
    }

    public void GameOver() {
        Debug.Log("Game Over");
    }

}
