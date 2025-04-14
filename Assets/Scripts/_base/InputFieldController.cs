using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldController : MonoBehaviour {

    public Action<string> OnInputEntered;

    [SerializeField] private GameObject _placeholder;
    [SerializeField] private TMP_InputField _inputField;

    private bool _isFocusing = false;

    private void Awake() {
        _inputField.onEndEdit.AddListener(HandleEndEditting);
    }

    private void Start() {
        _inputField.text = "5";
    }

    private void Update() {
        if (_inputField.isFocused) {
            if (!_isFocusing) {
                SetPlaceholderVisibility(false);

                _isFocusing = true;
            }
        } else {
            if (string.IsNullOrEmpty(_inputField.text) || string.IsNullOrWhiteSpace(_inputField.text)) {
                if (_isFocusing) {
                    SetPlaceholderVisibility(true);

                    _isFocusing = false;
                }
            }
        }
    }

    private void OnDestroy() {
        if (_inputField != null)
            _inputField.onValueChanged.RemoveAllListeners();
    }

    private void HandleEndEditting(string inputFieldText) {
        string trimmedText = inputFieldText.Trim();
        if (!string.IsNullOrEmpty(trimmedText) || !string.IsNullOrWhiteSpace(trimmedText)) {
            OnInputEntered?.Invoke(trimmedText);
        } else {
            // TODO: pop up error
        }
    }

    private void SetPlaceholderVisibility(bool visibility) {
        if (_placeholder == null) return;

        _placeholder.SetActive(visibility);
    }

    public void ClearInputField() {
        _inputField.text = "";
    }

}
