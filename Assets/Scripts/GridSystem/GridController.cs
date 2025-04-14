using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour, IPoolObject {

    public Action<GridController, bool> OnGridClickedStateChanged;

    private bool _isClicked = true;
    public bool IsClicked {
        get => _isClicked;
        private set {
            if (value == _isClicked) return;

            _isClicked = value;

            _xText.SetActive(_isClicked);

            OnGridClickedStateChanged?.Invoke(this, _isClicked);
        }
    }

    public Vector3 GridIndex { get => _gridIndex; }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _xText;

    private Vector3 _gridIndex;

    private void Awake() {
        _canvas.worldCamera = Camera.main;
    }

    #region Set Position

    public void SetPosition(Transform container, Vector3 position) {
        _gridIndex = position;

        transform.SetParent(container, false);

        transform.position = position;

        GridBuilder.Instance.OnGridBuild += HandleGridBuild;
    }

    #endregion

    #region Events

    private void HandleGridBuild() {
        GridBuilder.Instance.OnGridBuild -= HandleGridBuild;

        // search for neighbours

        Activate();
    }

    #endregion

    #region IPoolObject

    public void Activate() {
        IsClicked = false;

        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

    #endregion

}
