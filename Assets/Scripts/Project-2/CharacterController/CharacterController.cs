using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2 {
    public class CharacterController : MonoBehaviour {

        private StackController _currentStack;
        public StackController CurrentStack {
            get => _currentStack;
            private set {
                _currentStack = value;
            }
        }

        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;

        private bool _allowMovement = false;
        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();

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
                CurrentStack = stackController;

                transform.DOKill();
                transform.DOMoveX(stackController.transform.position.x, 0.3f).SetEase(Ease.InOutSine);
            } else if (other.TryGetComponent(out FinishController _)) {
                GameStateManager.Instance.SetGameState(GameState.LevelCompleted);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.TryGetComponent(out StackController stackController)) {
                if (CurrentStack == stackController) {
                    GameStateManager.Instance.SetGameState(GameState.LevelFailed);

                    _rigidbody.useGravity = true;
                }
            }
        }

        #region Events

        private void HandleGameStateChanged(GameState newState) {
            _allowMovement = newState == GameState.Game;
            _animator.transform.rotation = Quaternion.identity;

            _animator.SetBool("Dance", newState == GameState.LevelCompleted);
        }

        #endregion

    }
}
