using UnityEngine;
using System.Collections;

public class PageTransition : MonoBehaviour {

    public GameObject TargetPage;
    public float Duration = 0.5f;

    private bool _isTransitioning = false;
    private float _currentTime = 0f;
    private float _start;
    private float _targetStart;

    public void StartTransition() {
        TargetPage.transform.Translate(0, (float)Screen.height, 0);
        TargetPage.SetActive(true);
        _targetStart = TargetPage.transform.position.y;
        _start = transform.position.y;
        _isTransitioning = true;
        _currentTime = Duration;
    }

    public bool IsTransitioning() {
        return _isTransitioning;
    }

	void Awake() {
        TargetPage.SetActive(false); // ensures that the target page is not enabled yet
	}
        
	void Update() {
        if (_currentTime > 0f) {
            float offset = Mathf.Lerp(_start, _start - (float)Screen.height, _applyEasing(1f - _currentTime / Duration));
            transform.position = new Vector3(transform.position.x, offset, transform.position.z);
            float targetOffset = Mathf.Lerp(_targetStart, _targetStart - (float)Screen.height, _applyEasing(1f - _currentTime / Duration));
            TargetPage.transform.position = new Vector3(TargetPage.transform.position.x, targetOffset, TargetPage.transform.position.z);
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f) {
                gameObject.SetActive(false);
            }
        }
	}

    private float _applyEasing(float t) {
        float f = t - 1.0f;
        return f * f * f + 1.0f;
    }

}
