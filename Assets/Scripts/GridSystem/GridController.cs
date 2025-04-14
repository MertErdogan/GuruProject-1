using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour, IPoolObject {

    private bool _isClicked = true;
    public bool IsClicked {
        get => _isClicked;
        private set {
            if (value == _isClicked) return;

            _isClicked = value;

            _xText.SetActive(_isClicked);

            if (_isClicked) {
                CheckMatch();
                for (int i = 0; i < _neighbourGrids.Count; i++) {
                    GridController neighbourGrid = _neighbourGrids[i];
                    if (neighbourGrid.IsClicked) {
                        neighbourGrid.CheckMatch();
                    }
                }
            }
        }
    }

    private Vector3 _gridIndex;
    public Vector3 GridIndex {
        get => _gridIndex;
        private set {
            _gridIndex = value;
        }
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _xText;

    private List<GridController> _neighbourGrids;

    private void Awake() {
        _canvas.worldCamera = Camera.main;
    }

    #region Set Clicked

    public void SetClicked(bool isClicked = true) {
        IsClicked = isClicked;
    }

    #endregion

    #region Set Position

    public void SetPosition(Transform container, Vector3 position, bool isDeactivated = false) {
        _gridIndex = position;

        transform.SetParent(container, false);

        transform.position = position;

        if (!isDeactivated) {
            GridBuilder.Instance.OnGridBuild += HandleGridBuild;
        }
    }

    #endregion

    #region Get Neighbours

    private void GetNeighbours() {
        _neighbourGrids = new List<GridController>() {
            GridManager.Instance.GetGridController(GridIndex + Vector3.right),
            GridManager.Instance.GetGridController(GridIndex + Vector3.left),
            GridManager.Instance.GetGridController(GridIndex + Vector3.forward),
            GridManager.Instance.GetGridController(GridIndex + Vector3.back),
        };

        _neighbourGrids.RemoveAll(g => g == null);
    }

    #endregion

    #region Check Match

    public void CheckMatch() {
        int clickedGridCount = 0;
        for (int i = 0; i < _neighbourGrids.Count; i++) {
            if (_neighbourGrids[i].IsClicked) {
                clickedGridCount++;
            }
        }

        if (clickedGridCount >= 2) {
            IsClicked = false;
            for (int i = 0; i < _neighbourGrids.Count; i++) {
                _neighbourGrids[i].SetClicked(false);
            }

            ScoreManager.Instance.IncreaseScore();
        }
    }

    #endregion

    #region Events

    private void HandleGridBuild() {
        GridBuilder.Instance.OnGridBuild -= HandleGridBuild;

        GetNeighbours();

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
