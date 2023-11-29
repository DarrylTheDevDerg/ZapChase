using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotatingCarouselPuzzle : MonoBehaviour
{
    public enum RotationStates
    {
        Rotation1,
        Rotation2,
        Rotation3,
        Rotation4
    };

    private RotationStates currentState;
    // Must loop back to R1 if attempting to reach any state further than R4.

    [Header("Rotation & Puzzle Related")]
    public float rotationAmnt;
    // Rotation amount calculated in float values, didn't use Vectors because it's excessively complex to code for a puzzle.
    public GameObject rotationCenter;

    public GameObject toggleButton;
    // Self-explanatory.

    public GameObject[] platforms;
    public GameObject[] prefabsToSpawn;

    private int[] platformId;

    public int minRange, maxRange;

    private float chance;

    [System.Serializable]
    public class Float
    {
        public string name;
        public float value;
    }

    private int solving;

    [Header("Solved Puzzle Related")]
    private bool solved = false;
    public UnityEvent whatTheDogDoin;
    // Despite the funny name this value has, it's essential to make stuff happen if the puzzle is solved.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRotationState(RotationStates nextRotation)
    {
        switch (nextRotation)
        {
            case RotationStates.Rotation1:
                Debug.Log("Rotation 1 to 2.");
                currentState += 1;
                break;

            case RotationStates.Rotation2:
                Debug.Log("Rotation 2 to 3.");
                currentState += 1;
                break;

            case RotationStates.Rotation3:
                Debug.Log("Rotation 3 to 4.");
                currentState += 1;
                break;

            case RotationStates.Rotation4:
                Debug.Log("Rotation 4 to 1.");
                currentState = 0;
                break;

            default:
                Debug.LogError("Unknown rotation.");
                break;
        }
    }

    public void ExecuteEvents()
    {
        if (!solved)
        {
            whatTheDogDoin.Invoke();
            solved = true;
        }
    }

    public void ExecuteRotation()
    {
        foreach (GameObject obj in platforms)
        {
            RotateAroundCenter(obj);
        }
    }

    public void RotateAroundCenter(GameObject obj)
    {
        // Calculate the relative position of the object to the rotation center
        Vector3 relativePos = obj.transform.position - rotationCenter.transform.position;

        // Calculate the rotation angle based on the rotation speed and Time.deltaTime
        float rotationAngle = rotationAmnt * Time.deltaTime;

        // Rotate the relative position
        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        relativePos = rotation * relativePos;

        // Update the object's position based on the rotated relative position
        obj.transform.position = rotationCenter.transform.position + relativePos;
    }

    public void SpawnPrefabsOverPlatforms()
    {
        int randomIndex = Random.Range(0, prefabsToSpawn.Length - 1);

        foreach (GameObject platform in platforms)
        {
            // Get the position of the platform
            Vector3 platformPosition = platform.transform.position;

            // Calculate the position to spawn the prefab above the platform (you can adjust the Y offset as needed)
            Vector3 spawnPosition = platformPosition + new Vector3(0f, 2f, 0f);

            // Instantiate the prefab at the calculated position
            GameObject spawnedPrefab = Instantiate(prefabsToSpawn[randomIndex], spawnPosition, Quaternion.identity);

            // Optionally, you can parent the spawned prefab to the platform
            spawnedPrefab.transform.parent = platform.transform;
        }
    }
}
