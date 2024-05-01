using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
    private void Update() {
        Instance = this;
        HandleMovement();
    }

    private void HandleMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        float moveAmount = verticalInput * moveSpeed * Time.deltaTime;
        // transform.Translate(0f, moveAmount, 0f);
        Vector3 newPosition = transform.position + Vector3.up * moveAmount; // Move only along the y-axis
        rb.MovePosition(newPosition);
    }
}
