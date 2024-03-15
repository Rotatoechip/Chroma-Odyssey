using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private float colorChangeTimer;
    [SerializeField] private float colorChangeInterval = 5f;
    private int currentColorIndex;
    private Color[] colors = new Color[] { Color.blue, Color.green, Color.yellow, new Color(1f, 0.64f, 0f) }; // Colors including orange

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        currentColorIndex = 0;
        ChangeColor(colors[currentColorIndex]); // Apply the initial color
    }

    private void Update()
    {
        colorChangeTimer += Time.deltaTime;

        if (colorChangeTimer >= colorChangeInterval)
        {
            colorChangeTimer = 0f;
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            ChangeColor(colors[currentColorIndex]); // Change to the next color
        }
    }

    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor; // Update the sprite renderer's color
        // Disable platform destruction and double jump by default, will be enabled specifically by color
        playerMovement.EnableDestroyPlatforms(false);
        playerMovement.SetColorAbilities(false); // Reset double jump and platform destruction abilities

        if (newColor == Color.green)
        {
            playerMovement.ModifyMoveSpeed(5f); // Set speed for green
            playerMovement.SetColorAbilities(true); // Enable double jump for green
        }
        else if (newColor == Color.blue)
        {
            playerMovement.ModifyMoveSpeed(10f); // Increase speed for blue
        }
        else if (newColor == Color.yellow)
        {
            playerMovement.ModifyMoveSpeed(2f); // Decrease speed for yellow
        }
        else if (newColor == new Color(1f, 0.64f, 0f)) // Orange
        {
            playerMovement.EnableDestroyPlatforms(true); // Enable platform destruction for orange
            playerMovement.ModifyMoveSpeed(5f); // Optionally adjust speed for orange, if different from the default
        }
    }
}
