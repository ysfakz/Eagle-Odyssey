using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 6f;
    [SerializeField] private Transform defaultModel;
    [SerializeField] private Transform poweredModel;
    [SerializeField] private AudioClip powerUpSound;
    private bool isPowered = false;
    private bool powerUpSubscribed = false;
    private float poweredUpTimer;
    private float poweredUpTimerMax = 10f;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        FindPowerUp();
        rb = GetComponent<Rigidbody>();
        //Freeze the player's X and Z position.
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        defaultModel.gameObject.SetActive(true);
        poweredModel.gameObject.SetActive(false);
    }
    private void Update() {
        HandleMovement();
        FindPowerUp();
        PoweredDown();

        if (GameManager.Instance.IsGameOver()) {
            PauseSound();
        }
        if (isPowered) {
            PoweredUp();
        }
    }

    /*Finds the first power up object in line.*/
    private void FindPowerUp() {
        if (!powerUpSubscribed) {
            PowerUp powerUp = FindObjectOfType<PowerUp>();
            if (powerUp != null) {
                powerUp.OnPowerUpCollected += PowerUp_OnPowerUpCollected;
                powerUp.OnPowerUpDestroyed += PowerUp_OnPowerUpDestroyed;
                powerUpSubscribed = true;
            }
        }
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        PauseSound();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        PlaySound();
    }

    /*Activates the powered up effects.*/
    private void PowerUp_OnPowerUpCollected(object sender, EventArgs e) {
        powerUpSubscribed = false;
        isPowered = true;
        poweredUpTimer = 0f;
        FindObjectOfType<AudioSource>().PlayOneShot(powerUpSound);

        defaultModel.gameObject.SetActive(false);
        poweredModel.gameObject.SetActive(true);
    }

    private void PowerUp_OnPowerUpDestroyed(object sender, EventArgs e) {
        powerUpSubscribed = false;
    }

    /*Function that takes input and translates it into movement.*/
    private void HandleMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        float moveAmount = verticalInput * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + Vector3.up * moveAmount;
        rb.MovePosition(newPosition);

        Quaternion targetRotation = Quaternion.Euler(verticalInput * -20f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
    }

    /*Activates the powered up timer.*/
    private void PoweredUp() {
        poweredUpTimer += Time.deltaTime;
    }

    /*Deactives the power up effects when the time runs out.*/
    private void PoweredDown() {
       if (poweredUpTimer >= poweredUpTimerMax) {
            isPowered = false;
            powerUpSubscribed = false;
            poweredUpTimer = 0f;

            defaultModel.gameObject.SetActive(true);
            poweredModel.gameObject.SetActive(false);
        } 
    }

    private void PauseSound() {
        GetComponent<AudioSource>().Pause();
    }

    private void PlaySound() {
        GetComponent<AudioSource>().Play();
    }

    public bool IsPowered() {
        return isPowered == true;
    }
}
