﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData {
    public int InputIndex;
    public Color SkinColor;
    public int NumberOfKills = 0;
    public int NumberOfDeaths = 0;

    public PlayerData(int inputIndex, Color skinColor) {
        InputIndex = inputIndex;
        SkinColor = skinColor;
    }

}

public class ModeData {
    public int Index;
    protected int Option;

    public ModeData(int index, int option) {
        Index = index;
        Option = option;
    }
};

public class TimeModeData : ModeData {
    public TimeModeData(int option) : base(0, option) { }

    public float Duration {
        get {
            switch (Option) {
                case 0:
                    return 60f;
                case 1:
                    return 3f * 60f;
                case 2:
                    return 5f * 60f;
                case 3:
                    return 10f * 60f;
                case 4:
                    return 15f * 60f;
                default:
                    return 60f;
            }
        }
    }
}

public class SurvivalModeData : ModeData {
    public SurvivalModeData(int option) : base(1, option) { }

    public int NumberOfLives {
        get {
            switch (Option) {
                case 0:
                    return 1;
                case 1:
                    return 3;
                case 2:
                    return 5;
                default:
                    return 1;
            }
        }
    }
}

public class GameManager : Singleton<GameManager> {

    public const int NumberOfPlayers = 5;

    private PlayerData[] _players = new PlayerData[NumberOfPlayers];
    private List<Color> _colors = new List<Color> {
        new Color(0.6f, 0.4f, 0.4f, 1f),
        new Color(0.4f, 0.6f, 0.4f, 1f),
        new Color(0.4f, 0.4f, 0.6f, 1f),
        new Color(0.4f, 0.6f, 0.6f, 1f),
        new Color(0.6f, 0.6f, 0.4f, 1f),
        new Color(0.6f, 0.4f, 0.6f, 1f),
    };
    private ModeData _mode;

    protected GameManager() {}

    public void SetPlayer(int index, int inputIndex) {
        var color = _colors[Random.Range(0, _colors.Count)];
        _colors.Remove(color);
        _players[index] = new PlayerData(inputIndex, color);
    }

    public void RemovePlayer(int index) {
        _colors.Add(_players[index].SkinColor);
        _players[index] = null;
    }

    public void RemovePlayers() {
        for (var i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] != null) {
                _colors.Add(_players[i].SkinColor);
                _players[i] = null;
            }
        }
    }

    public void ResetScores() {
        for (var i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] != null) {
                _players[i].NumberOfDeaths = 0;
                _players[i].NumberOfKills = 0;
            }
        }
    }

    public PlayerData GetPlayer(int index) {
        return _players[index];
    }

    public void ChangePlayerColor(int index) {
        if (_colors.Count > 0) {
            var oldColor = _players[index].SkinColor;
            var newColor = _colors[Random.Range(0, _colors.Count)];
            _colors.Remove(newColor);
            _colors.Add(oldColor);
            _players[index].SkinColor = newColor;
        }
    }

    public int GetNextPlayerIndex() {
        for (var i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] == null) {
                return i;
            }
        }
        return NumberOfPlayers;
    }

    public bool IsInputAvailable(int inputIndex) {
        for (var i = 0; i < NumberOfPlayers; ++i) {
            if (_players[i] != null && _players[i].InputIndex == inputIndex) {
                return false;
            }
        }
        return true;
    }

    public void SetMode(int modeIndex, int option) {
        switch (modeIndex) {
            case 0:
                _mode = new TimeModeData(option);
                break;
            case 1:
                _mode = new SurvivalModeData(option);
                break;
            default:
                throw new System.InvalidOperationException("Mode does not exist");
        };
    }

    public ModeData GetMode() {
        return _mode;
    }

}
