using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event EventHandler OnObstacleHit;
    public event EventHandler OnObstacleDestroyed;

    /*Function to handle the obstacle collision with the player.*/
    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Player")){
            OnObstacleHit?.Invoke(this, EventArgs.Empty);
        }    
    }

    private void OnDestroy() {
        OnObstacleDestroyed?.Invoke(this, EventArgs.Empty);
    }
}
