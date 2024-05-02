using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    private int currentScore = 0;
    private bool gemSubscribed = false;
    private bool obstacleSubscribed = false;
    private bool isGamePaused = false;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        WaitingToStart,
        GamePlaying,
        GameOver,
    }

    private State currentState;

    private void Awake() {
        Instance = this;
        currentState = State.WaitingToStart;
    }

    private void Start() {
        AddListeners();
    }

    private void Update() {
        AddListeners();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseGame();
        }

        // Debug.Log(currentState);
        
        switch (currentState) {
            case State.WaitingToStart:
                ContinueGame();
                break;
            case State.GamePlaying:
                break;
            case State.GameOver:
                PauseGame();
                break;
        }
    }

    /*Adds listeners to events.*/
    private void AddListeners() {
        FindGem();
        FindWaitingToStartUI();
        FindObstacle();
    }

    /*Finds the next Gem object and adds listeners to the gem events.*/
    private void FindGem() {
        if (!gemSubscribed) {
            Gem gem = FindObjectOfType<Gem>();
            if (gem != null) {
                gem.OnGemCollected += Gem_OnGemCollected;
                gem.OnGemDestroyed += Gem_OnGemDestroyed;
                gemSubscribed = true;
            }
        }
    }

    /*Finds the waiting to start UI*/
    private void FindWaitingToStartUI() {
        WaitingToStartUI waitingToStartUI = FindObjectOfType<WaitingToStartUI>();
        if (waitingToStartUI != null) {
            waitingToStartUI.OnStartPressed += WaitingToStartUI_OnStartPressed;
        }
    }

    private void FindObstacle() {
        if (!obstacleSubscribed) {
            Obstacle obstacle = FindObjectOfType<Obstacle>();
            if (obstacle != null) {
                obstacle.OnObstacleHit += Obstacle_OnObstacleHit;
                obstacle.OnObstacleDestroyed += Obstacle_OnObstacleDestroyed;
                obstacleSubscribed = true;
            }
        }
    }

    /*Increases the score, plays the sound, and unsubscribes from the event if scored.*/
    private void Gem_OnGemCollected(object sender, EventArgs e) {
        IncreaseScore();
        gemSubscribed = false;
        SoundManager.Instance.PlayScoreSound();
    }

    /*Unsubscribes from the gem events when the gem is destroyed.*/
    private void Gem_OnGemDestroyed(object sender, EventArgs e) {
        gemSubscribed = false;
    }

    /*Changes the game state to playing.*/
    private void WaitingToStartUI_OnStartPressed(object sender, EventArgs e) {
        currentState = State.GamePlaying;
    }

    /*Changes the game state and unscubscribes from the event.*/
    private void Obstacle_OnObstacleHit(object sender, EventArgs e) {
        SoundManager.Instance.PlayObstacleHitSound();
        currentState = State.GameOver;
        obstacleSubscribed = false;
        ObjectSpawner.Instance.IsActiveCheck();
    }

    /*Unsubscribes from the event.*/
    private void Obstacle_OnObstacleDestroyed(object sender, EventArgs e) {
        obstacleSubscribed = false;
    }

    private void IncreaseScore() {
        currentScore++;
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void PauseGame() {
        Time.timeScale = 0f;
    }

    private void ContinueGame() {
        Time.timeScale = 1f;
    }

    public int GetCurrentScore() {
        return currentScore;
    }

    public bool IsWaitingToStart() {
        return currentState == State.WaitingToStart;
    }

    public bool IsGamePlaying() {
        return currentState == State.GamePlaying;
    }

    public bool IsGameOver() {
        return currentState == State.GameOver;
    }
}
