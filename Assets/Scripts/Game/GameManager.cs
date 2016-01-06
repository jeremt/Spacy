using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData {
    public int InputIndex;
    public Color SkinColor;

    public PlayerData(int inputIndex, Color skinColor) {
        InputIndex = inputIndex;
        SkinColor = skinColor;
    }

}

public class GameManager : Singleton<GameManager> {

    private PlayerData[] _players = new PlayerData[5];

    protected GameManager() {}

    public void SetPlayer(int index, PlayerData player) {
        _players[index] = player;
    }

    public void RemovePlayer(int index) {
        _players[index] = null;
    }

    public PlayerData GetPlayer(int index) {
        return _players[index];
    }

    public int GetNextPlayerIndex() {
        for (int i = 0; i < 5; ++i) {
            if (_players[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public bool InputAvailable(int inputIndex) {
        for (int i = 0; i < 5; ++i) {
            if (_players[i] != null && _players[i].InputIndex == inputIndex) {
                return false;
            }
        }
        return true;
    }

}
