using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : GameStateListener {

    [SerializeField][EnumFlags] private GameState _targetState;

    private CinemachineVirtualCamera _virtualCamera;

    protected override void Awake() {
        base.Awake();

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    #region Overrides

    protected override void HandleGameStateChanged(GameState newState) {
        base.HandleGameStateChanged(newState);

        _virtualCamera.Priority = _targetState.HasFlag(newState) ? 11 : 10;
    }

    #endregion

}
