using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2 {
    public class CharacterController : MonoBehaviour {

        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;

        private bool _allowMovement = false;

        private void Awake() {
            GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        }

        private void Update() {
            if (!_allowMovement) return;

            transform.position += transform.forward * Time.deltaTime * _moveSpeed;
        }

        private void OnDestroy() {
            if (GameStateManager.Instance != null) {
                GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out StackController stackController)) {
                transform.DOKill();
                transform.DOMoveX(stackController.transform.position.x, 0.3f).SetEase(Ease.InOutSine);
            }
        }

        #region Events

        private void HandleGameStateChanged(GameState newState) {
            _allowMovement = newState == GameState.Game;

            _animator.SetBool("Dance", newState == GameState.LevelCompleted);
        }

        #endregion

    }
}
