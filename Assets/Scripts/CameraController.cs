using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    [SerializeField] float xOffset;

    [SerializeField] float yOffset;

    // no y offset, have camera positioned at player's y instead for 1st person feel
    [SerializeField] float zOffset;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        // Sets camera position depending on what car is selected
        if(player.tag=="car1")
        {
            xOffset = -0.15f;
            yOffset = .5f;
            zOffset = -.34f;
        }
        else if (player.tag == "car2")
        {
            xOffset = .9f;
            yOffset = .55f;
            zOffset = -.5f;
        }
        else
        {
            xOffset = -0.17f;
            yOffset = .6f;
            zOffset = -.2f;
        }
    }

    void Update()
    {
        // Fixes MissingReferenceException for transform being destroyed
        if (transform != null && player != null)
        {
            // Allows camera to follow player at all times
            transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, player.position.z + zOffset);
        }
    }
}
