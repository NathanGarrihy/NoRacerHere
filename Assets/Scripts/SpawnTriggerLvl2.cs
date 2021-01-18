using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTriggerLvl2 : MonoBehaviour
{
    public SpawnManagerLvl2 spawnManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            spawnManager.SpawnTriggerEntered();
        }
    }
}
