using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Numerical Attributes")]
    public int energy;
    public int health;
    public int regenCooldown;
    public int hPRegenAmount;
    public string gameOverScreen;

    public bool debugMode;

    [HideInInspector]
    public int currentHealth;

    [HideInInspector]
    public int currentEnergy;

    public int energyDrain;
    public int energyRecovery;
    public float drainSpeed;
    public float externalRecoverySpeed;

    private float limit;
    private float recoveryLimit;

    private float regenCount;

    [HideInInspector]
    public bool draining;
    [HideInInspector]
    public bool recovering;

    private void Start()
    {
        currentHealth = health;
        currentEnergy = energy;
    }

    private void Update()
    {
        regenCount += Time.deltaTime;

        if (currentEnergy <= 0)
        {
            currentHealth -= 1;
            currentEnergy = 100;
        }

        if (currentEnergy > 0 && currentEnergy < 100 && !draining && !recovering)
        {
            if (regenCount >= regenCooldown)
            {
                currentEnergy += hPRegenAmount;
                regenCount = 0;
            }
        }

        if (currentHealth <= 0)
        {
            GameOver();
        }

        if (debugMode && !draining && !recovering && Input.GetKey(KeyCode.F1))
        {
            draining = true;
            energyDrain = 30;
            limit = drainSpeed;
            StartEnergyDrain();
        }

        if (debugMode && !draining && !recovering && Input.GetKey(KeyCode.F2))
        {
            recovering = true;
            energyRecovery = 10;
            recoveryLimit = externalRecoverySpeed;
            StartEnergyRecovery();
        }
    }

    public void GameOver()
    {
        Destroy(gameObject);
        //SceneManager.LoadScene(gameOverScreen);
    }

    public void StartEnergyDrain()
    {
        if (draining)
        {
            StartCoroutine(EnergyDrainCoroutine());
        }
    }

    private IEnumerator EnergyDrainCoroutine()
    {
        draining = true;

        while (energyDrain > 0 && draining)
        {
            yield return new WaitForSeconds(limit); // Wait for 1 second

            if (energyDrain > 0)
            {
                regenCount = 0;
                energyDrain -= 1;
                currentEnergy -= 1;
            }
        }

        draining = false;
    }

    public void StartEnergyRecovery()
    {
        if (recovering)
        {
            StartCoroutine(EnergyRecoveryCoroutine());
        }
    }

    private IEnumerator EnergyRecoveryCoroutine()
    {
        recovering = true;

        while (energyRecovery > 0 && recovering)
        {
            yield return new WaitForSeconds(recoveryLimit); // Wait for 1 second

            if (energyRecovery > 0 && currentEnergy < 100)
            {
                regenCount = 0;
                energyRecovery -= 1;
                currentEnergy += 1;
            }

            else if (energyRecovery > 0 && currentEnergy == 100)
            {
                energyRecovery = 0;
            }
        }

        recovering = false;
    }

    public void DrainEnergyWhileGrabbed()
    {
        StartCoroutine(DrainEnergyWhileGrabbedCoroutine());
    }

    private IEnumerator DrainEnergyWhileGrabbedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f); // Change this value as needed
            currentEnergy -= 1;

            if (currentEnergy <= 0)
            {
                currentHealth -= 1;
                currentEnergy = 100;
            }
        }
    }

}
