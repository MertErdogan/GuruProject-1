using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewController : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake() {
        ScoreManager.Instance.OnScoreUpdated += HandleScoreUpdated;
    }

    private void OnDestroy() {
        if (ScoreManager.Instance != null) {
            ScoreManager.Instance.OnScoreUpdated -= HandleScoreUpdated;
        }
    }

    #region Events

    private void HandleScoreUpdated(int newScore) {
        _scoreText.text = "Match Count: " + newScore;
    }

    #endregion

}
