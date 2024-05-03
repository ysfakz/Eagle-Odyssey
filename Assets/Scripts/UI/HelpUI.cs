using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour {

    [SerializeField] private Button backButton;
    private void Awake() {
        backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    private void Start() {
        gameObject.SetActive(false);

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }

}
