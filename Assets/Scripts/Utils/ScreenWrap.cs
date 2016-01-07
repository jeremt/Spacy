using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    public void Update() {
        var xDelta = transform.position.x;
        var yDelta = transform.position.y;
        var aspectRatio = Screen.width / (float) Screen.height;
        if (transform.position.x < -1f * aspectRatio) {
            xDelta = 1f * aspectRatio;
        }
        else if (transform.position.x > 1f * aspectRatio) {
            xDelta = -1f * aspectRatio;
        }
        if (transform.position.y < -1f) {
            yDelta = 1f;
        }
        else if (transform.position.y > 1f) {
            yDelta = -1f;
        }
        transform.position = new Vector3(xDelta, yDelta, 0);
    }
}