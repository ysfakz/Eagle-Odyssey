using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public event EventHandler OnPowerUpCollected;
    public event EventHandler OnPowerUpDestroyed;

    /*Function to handle the power up colission with the player.*/
    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Player")){
            OnPowerUpCollected?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }    
    }

    private void OnDestroy() {
        OnPowerUpDestroyed?.Invoke(this, EventArgs.Empty);
    }

}
