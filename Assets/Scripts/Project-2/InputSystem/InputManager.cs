using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2 {
    public class InputManager : SingleInstance<InputManager> {

        private bool _allowInput;

        private void Awake() {
            GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        }

        private void Update() {
            if (!_allowInput) return;

            if (Input.GetMouseButtonDown(0) && !RaycastHelper.IsPointerOverUIObject()) {
                StackManager.Instance.StopStack();
            }
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            if (GameStateManager.Instance != null ) {
                GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
            }
        }

        #region Events

        private void HandleGameStateChanged(GameState newState) {
            _allowInput = newState == GameState.Game;
        }

        #endregion

    }
}
