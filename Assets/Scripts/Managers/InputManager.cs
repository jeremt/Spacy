using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public static const int NumberOfInputs = 5;

    Dictionary<InputAlias, KeyCode>[] _aliases = new Dictionary<InputAlias, KeyCode>[NumberOfInputs] {
    
        // P1
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Return },
            { InputAlias.Submit, KeyCode.Return },
            { InputAlias.Cancel, KeyCode.Escape }
        },

        // P2
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Joystick1Button8 },
            { InputAlias.Submit, KeyCode.Joystick1Button0 },
            { InputAlias.Cancel, KeyCode.Joystick1Button2 }
        },

        // P3
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Joystick2Button8 },
            { InputAlias.Submit, KeyCode.Joystick2Button0 },
            { InputAlias.Cancel, KeyCode.Joystick2Button2 }
        },

        // P4
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Joystick3Button8 },
            { InputAlias.Submit, KeyCode.Joystick3Button0 },
            { InputAlias.Cancel, KeyCode.Joystick3Button2 }
        },

        // P5
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Joystick4Button8 },
            { InputAlias.Submit, KeyCode.Joystick4Button0 },
            { InputAlias.Cancel, KeyCode.Joystick4Button2 }
        },

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
        return Input.GetKeyUp(_aliases[inputIndex][alias]);
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
        return Input.GetKeyDown(_aliases[inputIndex][alias]);
    }

}
