using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof (PageTransition))]
public class ModesPage : MonoBehaviour {

    public ModeSelector[] Modes;
    private int _currentMode = 0;
    bool _axisDown = false;

    void Start() {
        for (int i = 0; i < Modes.Length; ++i) {
            if (i == _currentMode) {
                Modes[i].Select();
            } else {
                Modes[i].Deselect();
            }
        }
    }

	void Update () {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Cancel)) {
            GetComponent<PageTransition>().GoPrevious();
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Submit)) {
            GameManager.Instance.SetMode(_currentMode, Modes[_currentMode].CurrentOption);
            GetComponent<PageTransition>().GoNext();
        }
        if (Mathf.Abs(InputManager.Instance.GetAxis(InputAlias.Vertical)) > float.Epsilon && !_axisDown) {
            _axisDown = true;
            _selectMode(InputManager.Instance.GetAxis(InputAlias.Vertical) > 0 ? 1 : -1);
        }
        if (Mathf.Abs(InputManager.Instance.GetAxis(InputAlias.Vertical)) < float.Epsilon && _axisDown) {
            _axisDown = false;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Alt)) {
            Modes[_currentMode].ChangeOption();
        }
	}

    private void _selectMode(int direction) {
        Modes[_currentMode].Deselect();
        _currentMode += direction;
        if (_currentMode == Modes.Length) {
            _currentMode = 0;
        } else if (_currentMode < 0) {
            _currentMode = Modes.Length - 1;
        }
        Modes[_currentMode].Select();
    }

}
