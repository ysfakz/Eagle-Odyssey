using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    public event EventHandler OnGemCollected;
    public event EventHandler OnGemDestroyed;
    /*Function to handle the gem colission with the player.*/
    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Player")){
            OnGemCollected?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }    
    }

    private void OnDestroy() {
        OnGemDestroyed?.Invoke(this, EventArgs.Empty);
    }

}
