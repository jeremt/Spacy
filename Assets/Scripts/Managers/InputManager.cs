using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public enum InputAlias {
    Horizontal,
    Vertical,

    Start,
    Submit,
    Cancel,
    Alt,

    Jump,
    Shoot,
    Shield,
    Crouch
}

public class InputManager : Singleton<InputManager> {

    public const int NumberOfInputs = 5;

    private Dictionary<InputAlias, KeyCode> _keyboardButtons = new Dictionary<InputAlias, KeyCode>() {
        { InputAlias.Start, KeyCode.Return },
        { InputAlias.Submit, KeyCode.Return },
        { InputAlias.Cancel, KeyCode.Escape },
        { InputAlias.Alt, KeyCode.C },
        { InputAlias.Jump, KeyCode.Space },
        { InputAlias.Shoot, KeyCode.LeftShift },
        { InputAlias.Shield, KeyCode.X },
        { InputAlias.Crouch, KeyCode.V }
    };

    private Dictionary<InputAlias, string> _keyboardAxis = new Dictionary<InputAlias, string>() {
        { InputAlias.Horizontal, "KeyboardHorizontal" },
        { InputAlias.Vertical, "KeyboardVertical" }
    };

    private Dictionary<InputAlias, XboxButton> _xboxButtons = new Dictionary<InputAlias, XboxButton>() {
        { InputAlias.Start, XboxButton.Start },
        { InputAlias.Submit, XboxButton.A },
        { InputAlias.Cancel, XboxButton.B },
        { InputAlias.Alt, XboxButton.Y },
        { InputAlias.Jump, XboxButton.A },
        { InputAlias.Shoot, XboxButton.RightBumper }, // TODO: use RightTrigger but it's an axis :(
        { InputAlias.Shield, XboxButton.X },
        { InputAlias.Crouch, XboxButton.B }
    };

    private Dictionary<InputAlias, XboxAxis> _xboxAxis = new Dictionary<InputAlias, XboxAxis>() {
        { InputAlias.Horizontal, XboxAxis.LeftStickX },
        { InputAlias.Vertical, XboxAxis.LeftStickY }
    };

    protected InputManager() {}

    public bool GetKeyUp(InputAlias alias) {
        for (int i = 0; i < NumberOfInputs; ++i) {
            if (GetKeyUp(alias, i)) {
                return true;
            }
        }
        return false;
    }

    public bool GetKeyUp(InputAlias alias, int inputIndex) {
        if (inputIndex == 0) {
            return Input.GetKeyUp(_keyboardButtons[alias]);
        } else {
            return XCI.GetButtonUp(_xboxButtons[alias], inputIndex);
        }
    }

    public bool GetKeyDown(InputAlias alias) {
        for (int i = 0; i < NumberOfInputs; ++i) {
            if (GetKeyDown(alias, i)) {
                return true;
            }
        }
        return false;
    }

    public bool GetKeyDown(InputAlias alias, int inputIndex) {
        if (inputIndex == 0) {
            return Input.GetKeyDown(_keyboardButtons[alias]);
        } else {
            return XCI.GetButtonDown(_xboxButtons[alias], inputIndex);
        }
    }

    public bool GetKey(InputAlias alias) {
        for (int i = 0; i < NumberOfInputs; ++i) {
            if (GetKey(alias, i)) {
                return true;
            }
        }
        return false;
    }

    public bool GetKey(InputAlias alias, int inputIndex) {
        if (inputIndex == 0) {
            return Input.GetKey(_keyboardButtons[alias]);
        } else {
            return XCI.GetButton(_xboxButtons[alias], inputIndex);
        }
    }

    public float GetAxis(InputAlias alias) {
        float max = 0;
        for (int i = 0; i < NumberOfInputs; ++i) {
            if (Mathf.Abs(GetAxis(alias, i)) > Mathf.Abs(max)) {
                max = GetAxis(alias, i);
            }
        }
        return max;
    }

    public float GetAxis(InputAlias alias, int inputIndex) {
        if (inputIndex == 0) {
            return Input.GetAxis(_keyboardAxis[alias]);
        } else {
            return XCI.GetAxis(_xboxAxis[alias], inputIndex);
        }
    }

}
