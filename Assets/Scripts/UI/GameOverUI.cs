using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Transform gameOverScreen;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake() {
        playAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        menuButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });
    }

    private void Start() {
        gameOverScreen.gameObject.SetActive(false);
    }

    /*Shows and hides the game overscreen based on the game state.*/
    private void Update() {
        if (GameManager.Instance.IsGameOver()) {
            Show();
        } else {
            Hide();
        }

        scoreText.text = "Your Score: " + GameManager.Instance.GetCurrentScore();
    }

    private void Show() {
        gameOverScreen.gameObject.SetActive(true);
    }

    private void Hide() {
        gameOverScreen.gameObject.SetActive(false);
    }

}
