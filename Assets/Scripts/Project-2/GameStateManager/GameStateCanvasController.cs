using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCanvasController : GameStateObject {

    public Action OnCanvasAppeared;

    private Canvas _canvas;
    //private AnimationSequenceController _animationSequenceController;

    protected override void Awake() {
        base.Awake();

        _canvas = GetComponent<Canvas>();
        //_animationSequenceController = GetComponent<AnimationSequenceController>();

        GameStateManager.Instance.OnGameStateChangedWithPreviousState += HandleGameStateChangedWithPrevious;
    }

    protected override void OnDestroy() {
        if (GameStateManager.Instance != null) {
            GameStateManager.Instance.OnGameStateChangedWithPreviousState -= HandleGameStateChangedWithPrevious;
        }

        base.OnDestroy();
    }

    #region Events

    private void HandleGameStateChangedWithPrevious(GameState newState, GameState previousState) {
        if (TargetGameState == newState) {
            //if (_animationSequenceController.IsEmpty()) {
            //    _canvas.enabled = true;
            //    gameObject.SetActive(true);

            //    return;
            //}

            _canvas.enabled = true;
            gameObject.SetActive(true);
            //_animationSequenceController.AppearAnimationSequence(OnCanvasAppeared);
        }
        else if (TargetGameState == previousState) {
            //if (_animationSequenceController.IsEmpty()) {
            //    _canvas.enabled = false;
            //    gameObject.SetActive(false);

            //    return;
            //}

            //_animationSequenceController.DissapearAnimationSequence(() => {
            //    if (TargetGameState != GameStateManager.Instance.State) {
            //        _canvas.enabled = false;
            //        gameObject.SetActive(false);
            //    }
            //});
            if (TargetGameState != GameStateManager.Instance.State) {
                _canvas.enabled = false;
                gameObject.SetActive(false);
            }
        }
        else {
            //if (_animationSequenceController.IsEmpty()) {
            //    _canvas.enabled = false;
            //    gameObject.SetActive(false);

            //    return;
            //}

            //_animationSequenceController.DissappearWithoutAnimation();
            _canvas.enabled = false;
            gameObject.SetActive(false);
        }
    }

    #endregion

}
