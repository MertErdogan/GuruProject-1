using JSAM;
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

    public float RoadEnd { get => LastStack == null ? 10.5f : LastStack.transform.position.z + (LastStack.transform.localScale.z / 2f) + 0.5f; }

    [SerializeField] private StackPool _pool;
    [SerializeField] private int _spawnXPosition;
    [SerializeField] private Transform _stackContainer;
    [SerializeField] private int _stackSize;
    [SerializeField] private float _successfulPlacementTreshold;
    [SerializeField] private SoundFileObject _successfulPlacementSoundFile;
    [SerializeField] private float _pitchShift;

    private int _spawnZPosition = 9;
    private int _successfullPlacementCount;

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
        if (newState == GameState.Game && CurrentStack == null) {
            SpawnStack();
        }
    }

    #endregion

    #region Spawn/Despawn Stack

    private void SpawnStack() {
        _spawnZPosition += _stackSize;
        bool isEven = _spawnZPosition % 2 == 0;

        StackController stack = _pool.GetObjectFromPool();
        stack.transform.SetParent(_stackContainer);
        stack.transform.position = Vector3.right * _spawnXPosition * (isEven ? -1 : 1) + Vector3.forward * _spawnZPosition;
        stack.transform.localScale = CurrentStack == null ? new Vector3(3f, 1f, 3f) : CurrentStack.transform.localScale;

        stack.IsEvenStack(isEven);
        stack.Activate();
        CurrentStack = stack;
    }

    public void DespawnStack(StackController stack) {
        stack.Deactivate();
        _pool.AddObjectToPool(stack);
    }

    #endregion

    #region Stop Stack

    public void StopStack() {
        CurrentStack.StopStack();

        float hangover = CurrentStack.transform.position.x - (LastStack == null ? 0f : LastStack.transform.position.x);

        if (Mathf.Abs(hangover) >= (LastStack == null ? 3f : LastStack.transform.localScale.x)) {
            GameStateManager.Instance.SetGameState(GameState.LevelFailed);
        } else if (Mathf.Abs(hangover) <= _successfulPlacementTreshold) {
            _successfulPlacementSoundFile.startingPitch = 1f + (_pitchShift * _successfullPlacementCount);
            AudioManager.PlaySound(LibrarySounds.SuccessfulPlacement);

            CurrentStack.transform.position = new Vector3((LastStack == null ? 0f : LastStack.transform.position.x), CurrentStack.transform.position.y, CurrentStack.transform.position.z);

            _successfullPlacementCount++;
        } else {
            _successfullPlacementCount = 0;

            float direction = hangover > 0 ? 1f : -1f;
            SplitStack(hangover, direction);
        }

        SpawnStack();
    }

    #endregion

    #region Split Stack

    private void SplitStack(float hangover, float direction) {
        float newXSize = (LastStack == null ? 3f : LastStack.transform.localScale.x) - (Mathf.Abs(hangover));
        float fallingStackSize = CurrentStack.transform.localScale.x - newXSize;

        float newXPosition = (LastStack == null ? 0f : LastStack.transform.position.x) + (hangover / 2f);

        CurrentStack.transform.localScale = new Vector3(newXSize, CurrentStack.transform.localScale.y, CurrentStack.transform.localScale.z);
        CurrentStack.transform.position = new Vector3(newXPosition, CurrentStack.transform.position.y, CurrentStack.transform.position.z);

        float stackEdge = CurrentStack.transform.position.x + (newXSize / 2f * direction);
        float fallingStackXPosition = stackEdge + (fallingStackSize / 2f * direction);

        SpawnFallingStack(fallingStackXPosition, fallingStackSize);
    }

    private void SpawnFallingStack(float fallingStackXPosition, float fallingStackSize) {
        GameObject fallingStack = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingStack.transform.position = new Vector3(fallingStackXPosition, CurrentStack.transform.position.y, CurrentStack.transform.position.z);
        fallingStack.transform.localScale = new Vector3(fallingStackSize, CurrentStack.transform.localScale.y, CurrentStack.transform.localScale.z);

        fallingStack.AddComponent<Rigidbody>();
        Destroy(fallingStack.gameObject, 1f);
    }

    #endregion

}
