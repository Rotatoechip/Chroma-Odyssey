using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private float colorChangeTimer = 0f;
    private float colorChangeInterval = 5f; // 5 seconds for color change
    private bool isBlue = true; // Starting color

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        ChangeColor(Color.blue); // Start with blue
    }

    void Update()
    {
        colorChangeTimer += Time.deltaTime;

        if (colorChangeTimer >= colorChangeInterval)
        {
            // Reset the timer
            colorChangeTimer = 0f;

            // Switch color
            if (isBlue)
            {
                ChangeColor(Color.green);
                isBlue = false;
            }
            else
            {
                ChangeColor(Color.blue);
                isBlue = true;
            }
        }
    }

    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
        ResetAbilities();

        // Ability changes based on color
        if (newColor == Color.blue)
        {
            playerMovement.SetSpeed(10f); // Increase speed for blue
        }
        else if (newColor == Color.green)
        {
            playerMovement.SetDoubleJump(true); // Enable double jump for green
        }
    }

    private void ResetAbilities()
    {
        // Reset to default abilities
        playerMovement.SetSpeed(5f);
        playerMovement.SetDoubleJump(false);
    }
}