using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingToStartUI : MonoBehaviour {

    public event EventHandler OnStartPressed;
    private void Update() {
        if (IsInput()) {
            gameObject.SetActive(false);
            OnStartPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    /*Function that checks for any input.*/
    private bool IsInput() {
        if (Input.anyKeyDown) {
            return true;
        }
        if (Input.GetMouseButtonDown(0)) {
            return true;
        }
        if (Input.GetMouseButtonDown(1)) {
            return true;
        }
        if (Input.GetMouseButtonDown(2)) {
            return true;
        }
        return false;
    }

}
