using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingleInstance<ScoreManager> {

    public Action<int> OnScoreUpdated;

    private int _score;
    public int Score {
        get => _score;
        private set {
            _score = Mathf.Max(0, value);

            OnScoreUpdated?.Invoke(_score);
        }
    }

    private void Start() {
        ResetScore();
    }

    #region Score Operations

    public void IncreaseScore() {
        Score++;
    }

    public void ResetScore() {
        Score = 0;
    }

    #endregion

}
