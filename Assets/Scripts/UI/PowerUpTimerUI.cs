using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimerUI : MonoBehaviour {

    [SerializeField] private GameObject powerUpTimerUI;
    [SerializeField] private Image timerImage;

    private void Awake() {
        powerUpTimerUI.SetActive(false);
    }

    private void Update() {
        if (Player.Instance.IsPowered()) {
            powerUpTimerUI.SetActive(true);
        }
        if (!Player.Instance.IsPowered()) {
            powerUpTimerUI.SetActive(false);
        }

        timerImage.fillAmount = Player.Instance.GetPoweredUpTimerNormalized();
    }

}
