using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotDespawner : MonoBehaviour
{
    private GameObject player;
    private float plotSize = 52f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("plots z"+gameObject.transform.position.z);

        if(gameObject.transform.position.z < (player.transform.position.z-2*plotSize))
        {
            Destroy(gameObject);
        }
    }
}
