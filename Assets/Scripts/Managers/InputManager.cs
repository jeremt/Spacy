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

    public int MasterIndex = -1;

    Dictionary<InputAlias, KeyCode>[] _aliases = new Dictionary<InputAlias, KeyCode>[5] {
    
        // P1
        new Dictionary<InputAlias, KeyCode>() {
            { InputAlias.Start, KeyCode.Return },
            { InputAlias.Submit, KeyCode.Return },
            { InputAlias.Cancel, KeyCode.Escape }
        },

        // P2
        new Dictionary<InputAlias, KeyCode>() {},

        // P3
        new Dictionary<InputAlias, KeyCode>() {},

        // P4
        new Dictionary<InputAlias, KeyCode>() {},

        // P5
        new Dictionary<InputAlias, KeyCode>() {}

    };

    protected InputManager() {}

    public bool GetButtonUp(InputAlias alias) {
        if (MasterIndex == -1) {
            throw new System.InvalidOperationException("The master index should be set or a device index specified.");
        }
        return GetButtonUp(alias, MasterIndex);
    }

    public bool GetButtonUp(InputAlias alias, int inputIndex) {
        return true;
    }

}
