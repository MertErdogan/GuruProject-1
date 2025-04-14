using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private CinemachineVirtualCamera _camera;

    private void Awake() {
        _camera = GetComponent<CinemachineVirtualCamera>();

        GridBuilder.Instance.OnGridBuild += HandleGridBuild;
    }

    private void OnDestroy() {
        GridBuilder.Instance.OnGridBuild -= HandleGridBuild;
    }

    #region Events

    private void HandleGridBuild() {
        _camera.transform.position = Vector3.up * GridBuilder.Instance.GridSize * 2;
    }

    #endregion

}
