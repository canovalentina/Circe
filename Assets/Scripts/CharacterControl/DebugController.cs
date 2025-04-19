using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // Left/Right
        float moveZ = Input.GetAxis("Vertical");   // Forward/Backward

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed;
        rb.AddForce(movement, ForceMode.Force);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Jump when Space is pressed
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}