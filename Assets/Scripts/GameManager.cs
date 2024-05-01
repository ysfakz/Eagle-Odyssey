using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance {get; private set;}
    private int currentScore;

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

    private void Update() {
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
                //     state = State.GameOver;
                //     OnStateChanged?.Invoke(this, EventArgs.Empty);
                // }
                break;
            case State.GameOver:
                break;
        }
    }

    private void IncreaseScore() {
        currentScore++;
    }
}
