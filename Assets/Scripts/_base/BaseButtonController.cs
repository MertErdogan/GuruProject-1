using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButtonController : MonoBehaviour {

    protected Button button;

    protected virtual void Awake() {
        button = GetComponent<Button>();

        button.onClick.AddListener(HandleButtonClick);
    }

    protected virtual void OnDestroy() {
        button.onClick.RemoveAllListeners();
    }

    #region Events

    protected virtual void HandleButtonClick() {
        
    }

    #endregion

}
