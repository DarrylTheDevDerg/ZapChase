using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoicePuzzle : MonoBehaviour
{
    public GameObject[] toggleableObjects;
    public GameObject[] switchChoices;
    public int[] switchOrder;

    // Start is called before the first frame update
    void Start()
    {
        switchOrder = new int[switchChoices.Length];

        for (int i = 0; i < switchOrder.Length; i++)
        {
            switchOrder[i] = i + 1; // Assigning values counting up starting from 1
        }

        System.Random rng = new System.Random();

        int n = switchOrder.Length;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = switchOrder[k];
            switchOrder[k] = switchOrder[n];
            switchOrder[n] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleableSwitches()
    {

    }
}
