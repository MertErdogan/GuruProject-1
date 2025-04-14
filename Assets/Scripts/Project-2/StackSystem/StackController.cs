using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour, IPoolObject {

    [SerializeField] private float _moveSpeed;

    private bool _isEvenStack = true;
    private bool _isActivated = false;

    private void Update() {
        if (!_isActivated) return;

        transform.position += transform.right * Time.deltaTime * _moveSpeed * (_isEvenStack ? 1 : -1);
    }

    #region Set Even Stack

    public void IsEvenStack(bool isEven) {
        _isEvenStack = isEven;
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
