﻿using UnityEngine;
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
        { InputAlias.Cancel, KeyCode.Escape }
    };

    private Dictionary<InputAlias, XboxButton> _xboxAliases = new Dictionary<InputAlias, XboxButton>() {
        { InputAlias.Start, XboxButton.Start },
        { InputAlias.Submit, XboxButton.A },
        { InputAlias.Cancel, XboxButton.B }
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

}
