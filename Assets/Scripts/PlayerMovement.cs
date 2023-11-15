using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float acceleration = 15f;
    private float dashPower = 7.5f;
    public bool isGrappling = false;
    private int dashCount = 1;
    private Rigidbody2D rb;
    private Vector2 grappleTarget;
    private Vector2 mousePosition;
    private Camera mainCamera;
    private Transform groundCheck;
    private LayerMask ground = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        groundCheck = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Dash(direction);
        } else if (Input.GetMouseButtonDown(1)) {
            Grapple(direction);
        }

        if (isGrappling)
        {
            // Grapple behaviours

            // Gizmo for testing connection between player and target (visual)
            Debug.DrawLine(transform.position, grappleTarget, Color.red);

            // Grapple movement
            Vector2 grappleDirection = (grappleTarget - new Vector2(transform.position.x,transform.position.y)).normalized;
            rb.velocity = rb.velocity + ((grappleDirection * acceleration) * Time.deltaTime);
        }

        if (Physics2D.OverlapCircle(groundCheck.position, 0.05f, ground))
        {
            dashCount = 1;
        }
    }

    void Dash(Vector2 direction)
    {
        if (dashCount > 0)
        {
            rb.velocity = direction * dashPower;
            isGrappling = false;
            dashCount--;
        }
    }

    void Grapple(Vector2 direction)
    {
        if (isGrappling)
        {
            isGrappling = false;
        } else {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 25f, ground);
            if(hit)
            {
                grappleTarget = hit.point;
                isGrappling = true;
            }
        }
    }
}
