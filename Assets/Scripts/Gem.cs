using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    public event EventHandler OnGemCollected;
    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Player")){
            OnGemCollected?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }    
    }

}
