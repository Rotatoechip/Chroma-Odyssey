using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 15f;
    private Rigidbody2D rb;
    private bool canDoubleJump;
    private bool hasDoubleJumped;
    private bool canDestroyPlatforms = false;
    private int groundContactCount = 0;
    private Vector2 originalSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        originalSize = transform.localScale;
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
            if (IsGrounded() || (canDoubleJump && !hasDoubleJumped))
            {
                Jump();
                if (!IsGrounded() && canDoubleJump)
                {
                    hasDoubleJumped = true;
                }
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (IsGrounded() && canDoubleJump)
        {
            hasDoubleJumped = false; // Reset double jump when grounded and green
        }
    }

    private bool IsGrounded()
    {
        return groundContactCount > 0;
    }

    public void SetColorAbilities(bool doubleJumpEnabled)
    {
        canDoubleJump = doubleJumpEnabled;
        if (doubleJumpEnabled)
        {
            hasDoubleJumped = false; // Ensure double jump is reset when enabling
        }
    }

    public void ModifyMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void EnableDestroyPlatforms(bool enable)
    {
        canDestroyPlatforms = enable;
        transform.localScale = enable ? originalSize * 1.5f : originalSize; // Adjust size only when enabling platform destruction
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            groundContactCount++;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            groundContactCount--;
            if (canDestroyPlatforms)
            {
                // Optional: Add a delay or condition to ensure the character can jump off before the platform is destroyed
                DestroyPlatformAfterDelay(collision.gameObject);
            }
        }
    }

    private void DestroyPlatformAfterDelay(GameObject platform)
    {
        // Coroutine to wait for a brief moment before destroying the platform
        StartCoroutine(DestroyAfterDelay(platform));
    }

    IEnumerator DestroyAfterDelay(GameObject platform)
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        if (platform != null) // Check if the platform hasn't already been destroyed
        {
            Destroy(platform);
        }
    }
}
