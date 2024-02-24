using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private float colorChangeTimer;
    [SerializeField] private float colorChangeInterval = 5f; // Time interval for color change
    private int currentColorIndex; // Index to track the current color
    private Color[] colors = new Color[] { Color.blue, Color.green }; // Array of colors to switch between

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        currentColorIndex = 0; // Start with the first color
        ChangeColor(colors[currentColorIndex]); // Apply the initial color
    }

    private void Update()
    {
        colorChangeTimer += Time.deltaTime;

        // Check if it's time to change the color
        if (colorChangeTimer >= colorChangeInterval)
        {
            colorChangeTimer = 0f; // Reset timer
            currentColorIndex = (currentColorIndex + 1) % colors.Length; // Move to the next color
            ChangeColor(colors[currentColorIndex]); // Change to the new color
        }
    }

    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor; // Change the sprite's color

        // Check the new color and apply abilities accordingly
        if (newColor == Color.green)
        {
            playerMovement.ModifyMoveSpeed(5f); // Set the move speed for green
            playerMovement.SetColorAbilities(true); // Enable double jump for green
        }
        else if (newColor == Color.blue)
        {
            playerMovement.ModifyMoveSpeed(10f); // Increase speed for blue
            playerMovement.SetColorAbilities(false); // Disable double jump for blue
        }
    }
}
