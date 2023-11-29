using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleSystem : MonoBehaviour
{
    [Header("Energy Related Variables")]
    public float energyNeeded;
    private float currentEnergy;
    private int energyStrength;
    public int voltsNeeded;
    public bool startingEnergy = false;
    private bool finished = false;
    private bool exchange = false;
    private int extraVoltage;

    [Header("Draining Variables")]
    public bool drainingCharge = false;
    public float drainSpeed = 0f;
    public float drainAmount = 0f;
    private float drainProgress;
    public float energyThreshold;

    [Header("External Resources")]
    public GameObject player;
    private PlayerHealth energyManagement;
    public GameObject[] energyBoxes;

    [Header("After solving Puzzle")]
    public UnityEvent thingsToDo;
    public UnityEvent thingsToDoIfDrained;

    // Start is called before the first frame update
    void Start()
    {
        if (startingEnergy)
        {
            currentEnergy = Random.Range(0f, (energyNeeded / 1.5f));
            energyNeeded -= currentEnergy;
        }

        if (player != null)
        {
            energyManagement = player.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnergyCheck();

        if (drainingCharge)
        {
            drainProgress += Time.deltaTime;

            if (drainProgress >= drainSpeed)
            {
                drainProgress = 0;
                currentEnergy -= drainAmount;
            }
        }
    }

    public void VoltageCheck()
    {
        for (int i = 0; i < energyBoxes.Length; i++)
        {
            energyBoxes[i].CompareTag("Energy Box");
        }
    }

    public void HealthExchange()
    {
        if (!exchange)
        {
            StartCoroutine(RecoveringEnergy());
            exchange = true;
        }
    }

    private IEnumerator RecoveringEnergy()
    {
        energyManagement.draining = true;
        energyManagement.energyDrain = (int)energyNeeded;

        while (currentEnergy < energyNeeded)
        {
            yield return new WaitForSecondsRealtime(energyManagement.drainSpeed);
            
            if (currentEnergy < energyNeeded)
            {
                currentEnergy += 1;
                energyManagement.currentEnergy -= 1;

                if (drainingCharge)
                {
                    drainProgress = 0;
                }
            }
        }

        energyManagement.draining = false;
    }

    public void EnergyCheck()
    {
        if (!drainingCharge)
        {
            if (currentEnergy >= energyNeeded)
            {
                if (thingsToDo.GetPersistentEventCount() > 0 && !finished)
                {
                    thingsToDo.Invoke();
                    finished = true;
                }

                else if (thingsToDo.GetPersistentEventCount() == 0 && !finished)
                {
                    Debug.Log("Puzzle solved.");
                    finished = true;
                }

            }
        }

        if (drainingCharge)
        {
            if (currentEnergy < energyThreshold)
            {
                if (thingsToDoIfDrained.GetPersistentEventCount() > 0 && finished)
                {
                    thingsToDoIfDrained.Invoke();
                    finished = false;
                }

                else if (thingsToDoIfDrained.GetPersistentEventCount() == 0 && finished)
                {
                    Debug.Log("Puzzle unsolved.");
                    finished = false;
                }
            }
        }
    }
}
