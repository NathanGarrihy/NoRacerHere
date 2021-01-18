using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLvl2 : MonoBehaviour
{
    RoadSpawnerLvl2 roadSpawner;
    PlotSpawnerLvl2 plotSpawner;

    void Start()
    {
        roadSpawner = GetComponent<RoadSpawnerLvl2>();
        plotSpawner = GetComponent<PlotSpawnerLvl2>();

    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
        plotSpawner.SpawnPlot();
    }
}