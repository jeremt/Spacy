using UnityEngine;
using System.Collections;

public class ScreenWrap : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        float xDelta = transform.position.x;
        float yDelta = transform.position.y;
        float aspectRatio = (float) Screen.width / (float) Screen.height;
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