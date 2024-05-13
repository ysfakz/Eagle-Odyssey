using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingToStartUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI highScoreText;
    private const string HIGH_SCORE = "HighScore";
    public event EventHandler OnStartPressed;
    private void Update() {
        if (IsInput()) {
            gameObject.SetActive(false);
            OnStartPressed?.Invoke(this, EventArgs.Empty);
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(HIGH_SCORE, 0).ToString();
    }

    /*Checks for any input.*/
    private bool IsInput() {
        if (Input.anyKeyDown) {
            return true;
        }
        if (Input.GetMouseButtonDown(0)) {
            return true;
        }
        if (Input.GetMouseButtonDown(1)) {
            return true;
        }
        if (Input.GetMouseButtonDown(2)) {
            return true;
        }
        return false;
    }

}
