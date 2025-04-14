using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : SingleInstance<StackManager> {

    private StackController _currentStack;
    public StackController CurrentStack {
        get => _currentStack;
        private set {
            if (_currentStack != null) {
                LastStack = _currentStack;
            }

            _currentStack = value;
        }
    }

    private StackController _lastStack;
    public StackController LastStack {
        get => _lastStack;
        private set {
            _lastStack = value;
        }
    }

    [SerializeField] private StackPool _pool;
    [SerializeField] private int _spawnXPosition;
    [SerializeField] private Transform _stackContainer;
    [SerializeField] private int _stackSize;

    private int _spawnZPosition = 9;

    private void Awake() {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        if (GameStateManager.Instance != null ) {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    #region Events

    private void HandleGameStateChanged(GameState newState) {
        if (newState == GameState.Game) {
            SpawnStack();
        }
    }

    #endregion

    #region Spawn Stack

    private void SpawnStack() {
        _spawnZPosition += _stackSize;
        bool isEven = _spawnZPosition % 2 == 0;

        StackController stack = _pool.GetObjectFromPool();
        stack.transform.SetParent(_stackContainer);
        stack.transform.position = Vector3.right * _spawnXPosition * (isEven ? -1 : 1) + Vector3.forward * _spawnZPosition;

        stack.IsEvenStack(isEven);
        stack.Activate();
        CurrentStack = stack;
    }

    #endregion

    #region Stop Stack

    public void StopStack() {
        CurrentStack.StopStack();

        float hangover = CurrentStack.transform.position.x - (LastStack == null ? 0f : LastStack.transform.position.x);
        SplitStack(hangover);

        SpawnStack();
    }

    #endregion

    #region Split Stack

    private void SplitStack(float hangover) {
        float newXSize = (LastStack == null ? 3f : LastStack.transform.localScale.x) - Mathf.Abs(hangover);
        float fallingBlockSize = CurrentStack.transform.localScale.x - newXSize;

        float newXPosition = (LastStack == null ? 0f : LastStack.transform.position.x) + (hangover / 2f * (_spawnZPosition % 2 == 0 ? 1 : -1));

        CurrentStack.transform.localScale = new Vector3(newXSize, CurrentStack.transform.localScale.y, CurrentStack.transform.localScale.z);
        CurrentStack.transform.position = new Vector3(newXPosition, CurrentStack.transform.position.y, CurrentStack.transform.position.z);
    }

    #endregion

}
