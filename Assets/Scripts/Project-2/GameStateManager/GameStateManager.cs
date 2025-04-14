using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : SingleInstance<GameStateManager> {
 
    public Action<GameState> OnGameStateChanged;
    public Action<GameState, GameState> OnGameStateChangedWithPreviousState;

    private GameState _state = GameState.None;
    public GameState State {
        get => _state;
        private set {
            if (_state == value) return;

            GameState previousState = _state;
            _state = value;

            OnGameStateChangedWithPreviousState?.Invoke(_state, previousState);
            OnGameStateChanged?.Invoke(_state);
        }
    }

    private void Start() {
        State = GameState.Start;
    }

#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            SetGameState(GameState.LevelCompleted);
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            SetGameState(GameState.LevelFailed);
        }
    }
#endif

    #region Setter/Getter

    public void SetGameState(GameState state) {
        State = state;
    }

    #endregion

}
