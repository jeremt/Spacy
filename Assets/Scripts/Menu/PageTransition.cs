using UnityEngine;
using System.Collections;

public class PageTransition : MonoBehaviour {

    public GameObject PreviousPage;
    public GameObject NextPage;
    public float Duration = 0.5f;

    private float _currentTime = 0f;
    private Vector3 _start;
    private Vector3 _targetStart;
    private GameObject _targetPage;
    private bool _isReverse = false;

    public void GoPrevious() {
        if (PreviousPage != null) {
            GoPage(PreviousPage, true);
        } else {
            Debug.LogWarning("No previous page found for " + gameObject.name + ".");
        }
    }

    public void GoNext() {
        if (NextPage != null) {
            GoPage(NextPage, false);
        } else {
            Debug.LogWarning("No next page found for " + gameObject.name + ".");
        }
    }

    public bool IsTransitioning() {
        return _currentTime > 0f;
    }

    public void Update() {
        if (_currentTime > 0f) {
            float progress = ApplyEasing(1f - _currentTime / Duration);
            transform.position = Vector3.Lerp(_start, _start + Vector3.up * GetOffset(), progress);
            _targetPage.transform.position = Vector3.Lerp(_targetStart, _targetStart + Vector3.up * GetOffset(), progress);
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f) {
                gameObject.SetActive(false);
                transform.position = _start;
            }
        }
	}

    private void GoPage(GameObject page, bool isReverse) {
        PageTransition targetTransition = page.gameObject.GetComponent<PageTransition>();
        if (targetTransition != null && targetTransition.IsTransitioning() || IsTransitioning()) {
            return;
        }
        _isReverse = isReverse;
        _targetPage = page;
        _targetStart = _targetPage.transform.position + Vector3.down * GetOffset();
        _targetPage.transform.position = _targetStart;
        _start = transform.position;
        _currentTime = Duration;
        _targetPage.SetActive(true);
    }

    private float GetOffset() {
        return _isReverse ? (float)Screen.height : -(float)Screen.height;
    }

    private float ApplyEasing(float t) {
        float f = t - 1.0f;
        return f * f * f + 1.0f;
    }
}
