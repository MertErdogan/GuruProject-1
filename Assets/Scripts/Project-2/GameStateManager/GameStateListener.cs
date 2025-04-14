using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateListener : MonoBehaviour {

    protected virtual void Awake() {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    protected virtual void OnDestroy() {
        if (GameStateManager.Instance != null) {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    #region Events

    protected virtual void HandleGameStateChanged(GameState newState) {
        
    }

    #endregion

}
