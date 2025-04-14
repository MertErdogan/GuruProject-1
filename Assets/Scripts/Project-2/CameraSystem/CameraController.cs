using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : GameStateListener {

    [SerializeField] private GameState _targetState;

    private CinemachineVirtualCamera _virtualCamera;

    protected override void Awake() {
        base.Awake();

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    #region Overrides

    protected override void HandleGameStateChanged(GameState newState) {
        base.HandleGameStateChanged(newState);

        _virtualCamera.Priority = _targetState == newState ? 11 : 10;
    }

    #endregion

}
