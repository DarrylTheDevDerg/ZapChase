using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OwlEyesPuzzle : MonoBehaviour
{
    public GameObject[] switches;
    public GameObject[] owlsLight;

    private int[] switchIds;
    private int[] owlIds;

    public float rayLength = 5f;

    public float deactivationDuration;
    private Dictionary<GameObject, float> objectTimers = new Dictionary<GameObject, float>();

    // Start is called before the first frame update
    void Start()
    {
        switchIds = new int[switches.Length];
        owlIds = new int[owlsLight.Length];

        for (int i = 0; i < switchIds.Length; i++)
        {
            switchIds[i] = i + 1; // Assigning values counting up starting from 1
        }

        for (int i = 0; i < owlIds.Length; i++)
        {
            owlIds[i] = i + 1; // Assigning values counting up starting from 1
        }
    }

    // Update is called once per frame
    void Update()
    {
        var playerObjects = switches
            .Where(obj => obj.CompareTag("Player"))
            .ToList();

        // Update timers for each object in the dictionary
        foreach (var kvp in objectTimers.ToList())
        {
            GameObject obj = kvp.Key;
            float timer = kvp.Value;

            // Increment the timer for the object
            timer += Time.deltaTime;

            // Do something based on the timer, e.g., remove the object after a certain time
            if (timer >= deactivationDuration)
            {
                obj.SetActive(false);  // Deactivate the object
                objectTimers[obj] = 0f;  // Reset the timer
            }
            else
            {
                // Update the timer in the dictionary
                objectTimers[obj] = timer;
            }
        }


        foreach (GameObject playerObj in playerObjects)
        {   
            // Cast a ray from the object's position in its forward direction
            RaycastHit hit;
            if (Physics.Raycast(playerObj.transform.position, playerObj.transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Handle the collision with the player switch and access the internal ID
                    int switchIndex = System.Array.IndexOf(switches, hit.collider.gameObject);
                    int internalID = switchIds[switchIndex];
                    Debug.Log("Player collided with switch ID " + internalID);
                }

                else if (hit.collider.CompareTag("OwlLight"))
                {
                    // Handle the collision with the owl light and access the internal ID
                    int owlIndex = System.Array.IndexOf(owlsLight, hit.collider.gameObject);
                    int internalID = owlIds[owlIndex];
                    Debug.Log("Player collided with owl light ID " + internalID);
                }
            }
        }
    }

    public void StartTimerForObject(GameObject obj)
    {
        if (!objectTimers.ContainsKey(obj))
        {
            objectTimers.Add(obj, 0f);
        }
    }

    public void SwitchActivation()
    {

    }
}
