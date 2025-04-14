using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildButtonController : MonoBehaviour {

    [SerializeField] private CanvasGroup _warningText;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleButtonClick);
    }

    private void OnDestroy() {
        _button.onClick.RemoveAllListeners();
    }

    #region Evetns

    private void HandleButtonClick() {
        if (GridBuilder.Instance.ShowGridSizeWarining) {
            _warningText.DOKill();
            _warningText.alpha = 1f;
            _warningText.DOFade(0f, 0.3f).SetDelay(2f).OnComplete(() => {
                GridBuilder.Instance.ShowGridSizeWarining = false;
            });
        }

        GridBuilder.Instance.BuildGrid();
    }

    #endregion

}
