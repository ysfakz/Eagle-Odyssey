using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update() {
        scoreText.text = GameManager.Instance.GetCurrentScore().ToString();
    }
}
