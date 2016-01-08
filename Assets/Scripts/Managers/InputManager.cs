using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public enum InputAlias {
//    Horizontal,
//    Vertical,

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

    private Dictionary<InputAlias, KeyCode> _keyboardAliases = new Dictionary<InputAlias, KeyCode>() {
        { InputAlias.Start, KeyCode.Return },
        { InputAlias.Submit, KeyCode.Return },
        { InputAlias.Cancel, KeyCode.Escape },
        { InputAlias.Alt, KeyCode.C },
        { InputAlias.Jump, KeyCode.Space },
        { InputAlias.Shoot, KeyCode.LeftShift },
        { InputAlias.Shield, KeyCode.X },
        { InputAlias.Crouch, KeyCode.V }
    };

    private Dictionary<InputAlias, XboxButton> _xboxAliases = new Dictionary<InputAlias, XboxButton>() {
        { InputAlias.Start, XboxButton.Start },
        { InputAlias.Submit, XboxButton.A },
        { InputAlias.Cancel, XboxButton.B },
        { InputAlias.Alt, XboxButton.Y },
        { InputAlias.Jump, XboxButton.A },
        { InputAlias.Shoot, XboxButton.RightBumper }, // TODO: use RightTrigger but it's an axis :(
        { InputAlias.Shield, XboxButton.X },
        { InputAlias.Crouch, XboxButton.B }
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
            return Input.GetKeyUp(_keyboardAliases[alias]);
        } else {
            return XCI.GetButtonUp(_xboxAliases[alias], inputIndex);
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
            return Input.GetKeyDown(_keyboardAliases[alias]);
        } else {
            return XCI.GetButtonDown(_xboxAliases[alias], inputIndex);
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
            return Input.GetKey(_keyboardAliases[alias]);
        } else {
            return XCI.GetButton(_xboxAliases[alias], inputIndex);
        }
    }

}
