using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;
    private bool canDoubleJump;
    private bool hasDoubleJumped; // Track if the double jump has been used
    private bool isGreen; // Track if the player is currently green

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        Move();
        JumpInput();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump();
                if (isGreen)
                {
                    canDoubleJump = true; // Enable double jump when grounded and green
                }
                hasDoubleJumped = false; // Reset double jump flag
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                Jump();
                hasDoubleJumped = true; // Set flag to indicate double jump has been used
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        // Use a raycast or other method to check if the player is grounded
        return groundContactCount > 0;
    }

    // Use a counter to keep track of how many platforms the player is standing on
    private int groundContactCount = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            groundContactCount++; // Increment on entering collision with a platform
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            groundContactCount--; // Decrement on exiting collision with a platform
        }
    }

    public void SetColorAbilities(bool doubleJumpEnabled)
    {
        isGreen = doubleJumpEnabled;
        canDoubleJump = false; // Reset double jump whenever the color changes
        hasDoubleJumped = false; // Reset the double jump used flag
    }

    public void ModifyMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetAbilities()
    {
        ModifyMoveSpeed(5f); // Reset move speed to default
        SetColorAbilities(false); // By default, the player is not green and cannot double jump
    }
}
