using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedCameraContainerController : MonoBehaviour {

    [SerializeField] private float _animationDuration;

    private void Awake() {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy() {
        if (GameStateManager.Instance != null) {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    #region Events

    private void HandleGameStateChanged(GameState newState) {
        if (newState == GameState.LevelCompleted) {
            transform.DOKill();
            transform.DOLocalRotate(Vector3.up * 360f, _animationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        } else {
            transform.DOKill();
            transform.rotation = Quaternion.identity;
        }
    }

    #endregion

}
