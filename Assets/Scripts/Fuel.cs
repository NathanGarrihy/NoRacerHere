using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [Header("Info")]
    public float current = 10f;

    [Header("Settings")]
    public float max = 10f;
    public float burnRate = 1f;

    void Update()
    {
        current -= burnRate * Time.deltaTime;

        if (current <= 0f)
        {
            // good potential place to play an ad in future
            Refuel();
        }
    }

    void Refuel()
    {
        current = max;
    }
}
