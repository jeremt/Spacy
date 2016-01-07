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

    public const int NumberOfPlayers = 5;

    private PlayerData[] _players = new PlayerData[5];
    private List<Color> _colors = new List<Color> {
        new Color(0.8f, 0.2f, 0.2f, 1f),
        new Color(0.2f, 0.8f, 0.2f, 1f),
        new Color(0.2f, 0.2f, 0.8f, 1f),
        new Color(0.8f, 0.8f, 0.2f, 1f),
        new Color(0.8f, 0.2f, 0.8f, 1f)
    };

    protected GameManager() {}

    public void SetPlayer(int index, int inputIndex) {
        var color = _colors[Random.Range(0, NumberOfPlayers)];
        _colors.Remove(color);
        _players[index] = new PlayerData(inputIndex, color);
    }

    public void RemovePlayer(int index) {
        _colors.Add(_players[index].SkinColor);
        _players[index] = null;
    }

    public PlayerData GetPlayer(int index) {
        return _players[index];
    }

    public int GetNextPlayerIndex() {
        for (int i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] == null) {
                return i;
            }
        }
        return NumberOfPlayers;
    }

    public bool IsInputAvailable(int inputIndex) {
        for (int i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] != null && _players[i].InputIndex == inputIndex) {
                return false;
            }
        }
        return true;
    }

}
