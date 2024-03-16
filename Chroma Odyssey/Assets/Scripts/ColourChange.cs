using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    // Add a public field to reference the TextMeshProUGUI component
    public TextMeshProUGUI abilityText;

    private float colorChangeTimer;
    [SerializeField] private float colorChangeInterval = 5f;
    private int currentColorIndex;
    private Color[] colors = new Color[] { Color.blue, Color.green, Color.yellow, new Color(1f, 0.64f, 0f) }; // Colors including orange

    // Unique ability names for each color
    private string[] abilityNames = { "Swiftboost", "Leapforce", "Timeslow", "Platformsmash" };

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
        // Ensure platform destruction is disabled by default and double jump is reset
        playerMovement.EnableDestroyPlatforms(false);
        playerMovement.SetColorAbilities(false);

        // Update the ability text and its color to match the character's color
        abilityText.text = abilityNames[currentColorIndex];
        abilityText.color = newColor;

        if (newColor == Color.green)
        {
            playerMovement.ModifyMoveSpeed(5f);
            playerMovement.SetColorAbilities(true);
        }
        else if (newColor == Color.blue)
        {
            playerMovement.ModifyMoveSpeed(10f);
        }
        else if (newColor == Color.yellow)
        {
            playerMovement.ModifyMoveSpeed(2f);
        }
        else if (newColor == new Color(1f, 0.64f, 0f)) // Orange
        {
            playerMovement.EnableDestroyPlatforms(true);
            playerMovement.ModifyMoveSpeed(5f);
        }
    }
}
