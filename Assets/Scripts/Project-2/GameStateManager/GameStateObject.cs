using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateObject : GameStateListener {

    public GameState TargetGameState { get => _targetState; }

    [Header("GameStateObject")]
    [SerializeField] private GameState _targetState;

    #region Events

    protected override void HandleGameStateChanged(GameState newState) {
        if (newState != _targetState) {
            Deactivate();
        } else {
            Activate();
        }
    }

    #endregion

    #region Activate/Deactivate

    protected virtual void Activate() {

    }

    protected virtual void Deactivate() {

    }

    #endregion

}
