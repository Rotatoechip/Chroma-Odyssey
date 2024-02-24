using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public bool isGrounded;

    private Vector2 moveDirection;
    private bool canDoubleJump = false;

    void Update()
    {
        // Input
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.Normalize();

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void SetDoubleJump(bool enable)
    {
        canDoubleJump = enable;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
