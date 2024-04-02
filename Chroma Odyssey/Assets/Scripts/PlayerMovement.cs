using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // For scene reset

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 15f;
    private Rigidbody2D rb;
    private bool canDoubleJump;
    private bool hasDoubleJumped;
    private bool canDestroyPlatforms = false;
    private int groundContactCount = 0;
    private Vector2 originalSize;

    [SerializeField] private Transform respawnPoint; // Reference to the respawn point

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
        CheckForFall(); // Call the fall checking function each frame
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
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
        transform.localScale = enable ? originalSize * 1.5f : originalSize;
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
                DestroyPlatformAfterDelay(collision.gameObject);
            }
        }
    }

    private void DestroyPlatformAfterDelay(GameObject platform)
    {
        StartCoroutine(DestroyAfterDelay(platform));
    }

    IEnumerator DestroyAfterDelay(GameObject platform)
    {
        yield return new WaitForSeconds(0.1f);
        if (platform != null)
        {
            Destroy(platform);
        }
    }

    // Check if the player has fallen below a certain threshold and respawn if so
    private void CheckForFall()
    {
        // Adjust the fall threshold according to your game's level design
        float fallThreshold = -25f;
        if (transform.position.y < fallThreshold)
        {
            RespawnAtCheckpoint();
        }
    }

    // Move the player to the respawn point
    private void RespawnAtCheckpoint()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            rb.velocity = Vector2.zero; // Reset velocity to prevent falling motion carryover
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        }
    }
}
