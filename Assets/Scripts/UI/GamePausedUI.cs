using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour {

    [SerializeField] private Transform gamePausedScreen;
    [SerializeField] private Transform helpScreen;
    [SerializeField] private Transform optionsScreen;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            Hide();
            GameManager.Instance.TogglePauseGame();
        });
        menuButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });
        helpButton.onClick.AddListener(() => {
            helpScreen.gameObject.SetActive(true);
        });
        optionsButton.onClick.AddListener(() => {
            optionsScreen.gameObject.SetActive(true);
        });
    }

    private void Start() {
        gamePausedScreen.gameObject.SetActive(false);

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void Show() {
        gamePausedScreen.gameObject.SetActive(true);
    }

    private void Hide() {
        gamePausedScreen.gameObject.SetActive(false);
    }

}
