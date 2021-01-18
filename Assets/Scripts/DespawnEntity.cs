using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEntity : MonoBehaviour
{
    private GameObject player;
    // The total distance that has to be between player and gameobject to destroy it
    private float backDistance = 156f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Stops MissingReferenceException
        if (player != null && gameObject != null)
        {
            // despawns cars more quickly for more consistent spawning
            if(gameObject.tag=="EnemyCar" && gameObject.transform.position.z < (player.transform.position.z))
            {
                Destroy(gameObject);
            }
            else if (gameObject.transform.position.z < (player.transform.position.z - backDistance))
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            return;
        }
    }
}
