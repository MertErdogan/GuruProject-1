using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : SingleInstance<FinishController> {

    [SerializeField] private float _levelLength;

    #region Move Finish Line

    public void MoveFinishLine() {
        transform.position += Vector3.forward * _levelLength;
    }

    #endregion

}
