using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    // Add a public field to reference the TextMeshProUGUI component
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI countdownText;

    private float colorChangeTimer;
    [SerializeField] private float colorChangeInterval = 5f;
    private int currentColorIndex;
    private Color[] colors = new Color[] { Color.blue, Color.green, Color.yellow, new Color(1f, 0.64f, 0f) }; // Colors including orange

    // Unique ability names for each color
    private string[] abilityNames = { "SwiftBoost", "DoubleLeap", "InverseControl", "PlatformSmash" };
    private bool isCountdownActive = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        currentColorIndex = 0;
        ChangeColor(colors[currentColorIndex]); // Apply the initial color
        countdownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        colorChangeTimer += Time.deltaTime;

        // Rainbow color change logic
        float hueValue = Mathf.Repeat(Time.time * 0.1f, 1);
        countdownText.color = Color.HSVToRGB(hueValue, 1, 1);

        // Start the countdown when there are 3 seconds left
        if (colorChangeInterval - colorChangeTimer <= 3 && !isCountdownActive)
        {
            StartCoroutine(ShowCountdown(3));
            isCountdownActive = true;
        }

        if (colorChangeTimer >= colorChangeInterval)
        {
            colorChangeTimer = 0f;
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            ChangeColor(colors[currentColorIndex]);
            isCountdownActive = false; // Reset the countdown flag
        }
    }

    IEnumerator ShowCountdown(int seconds)
    {
        countdownText.gameObject.SetActive(true);
        while (seconds > 0)
        {
            countdownText.text = $"Colour Change in {seconds}";
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        countdownText.gameObject.SetActive(false);
    }

    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor; // Update the sprite renderer's color
        // Ensure platform destruction is disabled by default and double jump is reset
        playerMovement.EnableDestroyPlatforms(false);
        playerMovement.SetColorAbilities(false);
        playerMovement.inverseControl = false; // Ensure inverse control is disabled by default

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
            // Enable the inverse control ability
            playerMovement.inverseControl = true;
        }
        else if (newColor == new Color(1f, 0.64f, 0f)) // Orange
        {
            playerMovement.EnableDestroyPlatforms(true);
            playerMovement.ModifyMoveSpeed(5f);
        }
    }
}
