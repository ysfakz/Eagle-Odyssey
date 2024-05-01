using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        //Freeze the player's X and Z position.
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
    private void Update() {
        Instance = this;
        HandleMovement();
    }

    /*Function that takes input and translates it into movement.*/
    private void HandleMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        float moveAmount = verticalInput * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + Vector3.up * moveAmount;
        rb.MovePosition(newPosition);
    }
}
