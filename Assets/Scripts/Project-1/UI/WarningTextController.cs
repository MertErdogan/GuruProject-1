using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningTextController : MonoBehaviour {

    private TextMeshProUGUI _text;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();

        _text.text = "Grid size must be between " + GridBuilder.Instance.MinSize + " & " + GridBuilder.Instance.MaxSize;
    }

}
