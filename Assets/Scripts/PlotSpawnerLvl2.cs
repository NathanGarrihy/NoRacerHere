using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawnerLvl2 : MonoBehaviour
{
    private int initAmount = 5;
    private float plotSize = 50f;
    private float xPosLeft = 250f;
    private float xPosRight = -0.1f;
    private float lastZPos = 0f;

    public List<GameObject> plots;

    // Start is called before the first frame update
    void Start()
    {
        // calculation for left plot to output correctly
        xPosLeft = (plotSize * -1.45f);

        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }
    }

    public void SpawnPlot()
    {
        // get a random plot from the list of plots
        GameObject plotsLeft = plots[Random.Range(0, plots.Count)];
        GameObject plotsRight = plots[Random.Range(0, plots.Count)];

        float zPos = lastZPos + plotSize;

        // spawn the plots on both side of the road
        Instantiate(plotsLeft, new Vector3(xPosLeft + plotSize + 23f, 0f, zPos), plotsLeft.transform.rotation);
        Instantiate(plotsRight, new Vector3(xPosRight, 0f, zPos + plotSize), new Quaternion(0, 180, 0, 0));

        lastZPos += plotSize;
    }

}