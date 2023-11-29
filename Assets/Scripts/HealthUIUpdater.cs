using UnityEngine;
using TMPro;

public class HealthUIUpdater : MonoBehaviour
{
    public TMP_Text healthText; // Reference to your UI TextMeshPro element
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script

    public string valueToDisplay = "currentHealth"; // The value from PlayerHealth to display (default is "currentHealth")

    private void Start()
    {
        if (playerHealth == null || healthText == null)
        {
            Debug.LogError("PlayerHealth or healthText references are missing. Assign them in the Inspector.");
        }

        // Initialize the UI TextMeshPro with the specified value from PlayerHealth
        UpdateHealthText(GetValueToDisplay());
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            // Check for updates in the PlayerHealth script and update the UI TextMeshPro accordingly
            UpdateHealthText(GetValueToDisplay());
        }
    }

    private void UpdateHealthText(int value)
    {
        // Update the UI TextMeshPro with the specified value from PlayerHealth
        healthText.text = value.ToString();
    }

    private int GetValueToDisplay()
    {
        // Retrieve the specified value from PlayerHealth based on the string input
        int result = 0;

        switch (valueToDisplay)
        {
            case "currentHealth":
                result = playerHealth.currentHealth;
                break;

            case "currentEnergy":
                result = playerHealth.currentEnergy;
                break;

                // Add cases for other values you want to display
                // case "someOtherValue":
                //     result = playerHealth.someOtherValue;
                //     break;
        }

        return result;
    }
}
