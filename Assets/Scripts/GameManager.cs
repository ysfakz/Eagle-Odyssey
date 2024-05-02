using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    private int currentScore = 0;
    private bool gemSubscribed = false;

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
        
        switch (currentState) {
            case State.WaitingToStart:
                // waitingToStartTimer -= Time.deltaTime;
                // if (waitingToStartTimer < 0f) {
                //     state = State.CountdownToStart;
                //     OnStateChanged?.Invoke(this, EventArgs.Empty);
                // }
                break;
            case State.GamePlaying:
                // gamePlayingTimer -= Time.deltaTime;
                // if (gamePlayingTimer < 0f) {
                    // currentState = State.GameOver;
                //     OnStateChanged?.Invoke(this, EventArgs.Empty);
                // }
                break;
            case State.GameOver:
                break;
        }
    }

    /*Function that adds listeners to events.*/
    private void AddListeners() {
        FindGem();
        FindWaitingToStartUI();
    }

    /*Function that finds the next Gem object and adds listeners to the gem events.*/
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

    /*Function that finds the waiting to start UI*/
    private void FindWaitingToStartUI() {
        WaitingToStartUI waitingToStartUI = FindObjectOfType<WaitingToStartUI>();
        if (waitingToStartUI != null) {
            waitingToStartUI.OnStartPressed += WaitingToStartUI_OnStartPressed;
        }
    }

    /*Function that changes the game state to playing.*/
    private void WaitingToStartUI_OnStartPressed(object sender, EventArgs e) {
        currentState = State.GamePlaying;
    }

    /*Function that increases the score, plays the sound, and unsubscribes from the event if scored.*/
    private void Gem_OnGemCollected(object sender, EventArgs e) {
        IncreaseScore();
        gemSubscribed = false;
        SoundManager.Instance.PlayScoreSound();
    }

    /*Function that unsubscribes from the gem events when the gem is destroyed.*/
    private void Gem_OnGemDestroyed(object sender, EventArgs e) {
        gemSubscribed = false;
    }

    private void IncreaseScore() {
        currentScore++;
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
