using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour, IPoolObject {

    //public float MoveSpeed { get => _moveSpeed; }
    public Material StackMaterial { get => _renderer.sharedMaterial; }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private MeshRenderer _renderer;

    private bool _isEvenStack = true;
    private bool _isActivated = false;

    private void Update() {
        if (!_isActivated) return;
        if (GameStateManager.Instance.State != GameState.Game) return;

        transform.position += transform.right * Time.deltaTime * _moveSpeed * (_isEvenStack ? 1 : -1);
    }

    #region Set Even Stack

    public void IsEvenStack(bool isEven) {
        _isEvenStack = isEven;
    }

    #endregion

    #region Set Material

    public void SetMaterial(Material material) {
        _renderer.sharedMaterial = material;
    }

    #endregion

    #region Stop Stack

    public void StopStack() {
        _isActivated = false;
    }

    #endregion

    #region IPoolObject

    public void Activate() {
        _isActivated = true;

        gameObject.SetActive(true);
    }

    public void Deactivate() {
        _isActivated = false;

        gameObject.SetActive(false);
    }

    #endregion

}
