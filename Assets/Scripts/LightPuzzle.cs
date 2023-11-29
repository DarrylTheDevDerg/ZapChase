using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightPuzzle : MonoBehaviour
{
    [Header("Light & Color")]
    public Light lightSource;
    public Light targetColor;
    private Color currentColor;
    private Color targetLightColor;

    [Header("Game Objects")]
    public GameObject redButton;
    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject resetButton;

    [Header("Player Related")]
    public string playerTag;
    private GameObject player;

    [Header("What to do after solving?")]
    public UnityEvent[] whatToDo;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = lightSource.color;
        targetLightColor = targetColor.color;

        currentColor.r = Random.Range(0f, (targetLightColor.r / 1.2f));
        currentColor.g = Random.Range(0f, (targetLightColor.g / 1.2f));
        currentColor.b = Random.Range(0f, (targetLightColor.b / 1.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
        float minRange = 0.15f;
        float maxRange = 0.25f;

        if (currentColor.r >= targetLightColor.r - minRange && currentColor.r <= targetLightColor.r + maxRange && currentColor.g >= targetLightColor.g - minRange && currentColor.g <= targetLightColor.g + maxRange && currentColor.b >= targetLightColor.b - minRange && currentColor.b <= targetLightColor.b + maxRange)
        {
            if (whatToDo.Length < 1 && !done)
            {
                Debug.Log("Puzzle solved!");
                done = true;
            }
            else if (whatToDo.Length >= 1 && !done)
            {
                for (int i = 0; i < whatToDo.Length; i++)
                {
                    whatToDo[i].Invoke();
                    done = true;
                }
            }
        }

        lightSource.color = currentColor;
    }

    public void ReddenLight()
    {
        currentColor.r += 0.15f;
    }

    public void GreeningLight()
    {
        currentColor.g += 0.15f;
    }

    public void ColdyingLight()
    {
        currentColor.b += 0.15f;
    }

    public void ResetColor()
    {
        currentColor.r = Random.Range(0f, (targetLightColor.r / 1.2f));
        currentColor.g = Random.Range(0f, (targetLightColor.g / 1.2f));
        currentColor.b = Random.Range(0f, (targetLightColor.b / 1.2f));
    }

    public void DarkenColor()
    {
        currentColor.r -= 0.5f;
        currentColor.g -= 0.5f;
        currentColor.b -= 0.5f;
    }
}
