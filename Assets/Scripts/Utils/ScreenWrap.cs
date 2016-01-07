using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    [Tooltip("The margin before wrapping in X")]
    public float MarginX;

    [Tooltip("The margin before wrapping in Y")]
    public float MarginY;

    public void Update() {
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.x > 1 + MarginX) {
            newPosition.x = -newPosition.x + MarginX;
        }
        if (viewportPosition.x < -MarginX) {
            newPosition.x = -newPosition.x - MarginX;
        }
        if (viewportPosition.y > 1 + MarginY) {
            newPosition.y = -newPosition.y + MarginY;
        }
        if (viewportPosition.y < -MarginY) {
            newPosition.y = -newPosition.y - MarginY;
        }
        transform.position = newPosition;
    }
}