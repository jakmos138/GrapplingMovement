using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float acceleration = 2f;
    private bool isGrounded = true;
    private bool isGrappling = false;
    private int dashCount = 1;
    private Rigidbody2D rb;
    private Vector3 grappleTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Dash();
        } else if (Input.GetMouseButtonDown(1)) {
            Grapple();
        }
    }

    void Dash()
    {
        if (dashCount > 0)
        {
            // Dash logic
        }
    }

    void Grapple()
    {
        if (isGrappling)
        {
            isGrappling = false;
            // Stop Grapple
        } else
        {
            isGrappling = true;
            // Begin Grapple
        }
    }
}
