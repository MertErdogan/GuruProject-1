using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : SingleInstance<GridBuilder> {

    public Action OnGridBuild;

    public int GridSize { get => _gridSize; }
    public int MinSize { get => _minSize; }
    public int MaxSize { get => _maxSize; }
    public bool ShowGridSizeWarining { get; set; } = false;

    [SerializeField] private Transform _gridContainer;
    [SerializeField] private InputFieldController _inputField;
    [SerializeField] private int _startSize;
    [SerializeField] private int _minSize;
    [SerializeField] private int _maxSize;

    private int _gridSize;

    private void Awake() {
        _inputField.OnInputEntered += HandleInputEntered;
    }

    private void Start() {
        _gridSize = _startSize;

        BuildGrid();
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        _inputField.OnInputEntered -= HandleInputEntered;
    }

    #region Events

    private void HandleInputEntered(string sizeString) {
        if (int.TryParse(sizeString, out int size)) {
            if (size >= MinSize && size <= MaxSize) {
                _gridSize = size;
            } else {
                ShowGridSizeWarining = true;
            }
        }
    }

    #endregion

    #region Build

    public void BuildGrid() {
        GridManager.Instance.ClearGridControllers(_gridSize);
        ScoreManager.Instance.ResetScore();

        for (int i = 0; i < _gridSize; i++) {
            for (int j = 0; j < _gridSize; j++) {
                GridController grid = GridManager.Instance.SpawnGridController();

                Vector3 gridPosition = Vector3.right * (j - (_gridSize / 2)) + Vector3.forward * (i - (_gridSize / 2));

                if (_gridSize % 2 == 0) {
                    gridPosition += (Vector3.right * 0.5f + Vector3.forward * 0.5f);
                }

                grid.SetPosition(_gridContainer, gridPosition);
            }
        }

        OnGridBuild?.Invoke();
    }

    #endregion

}
