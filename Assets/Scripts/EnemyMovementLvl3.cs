using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementLvl3 : MonoBehaviour
{
    private GameObject player;

    // for lane switching
    private float pos1X, pos2X, pos3X, pos4X, myPosX;
    private float enemyMoveSpeed;

    public float speed = .01f;
    private Vector3 hMoveVector = new Vector3(0.02f, 0f, 0f);
    private Vector3 vMoveVector;
    int rand;

    public ParticleSystem frontRightIndicator;
    public ParticleSystem frontLeftIndicator;

    // Start is called before the first frame update
    void Start()
    {
        // x co-ordinate should be -3/-0.9/0.9/3
        pos1X = -3f;
        pos2X = -0.9f;
        pos3X = 0.9f;
        pos4X = 3f;
        myPosX = gameObject.transform.position.x;

        // random chance of lane change
        rand = Random.Range(0, 10);

        enemyMoveSpeed = .5f;

        player = GameObject.Find("Player");
        vMoveVector = new Vector3(0f, 0f, enemyMoveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // all lanes
        // move away from player
        gameObject.transform.position -= vMoveVector;

        // Stops MissingReferenceException
        if (gameObject != null && player.gameObject != null)
        {
            // if car is close enough to player
            // cars are slightly further away in level 3 when switching lanes
            if (player.transform.position.z + 70 > gameObject.transform.position.z)
            {
                // Cars Move forward
                // random chance
                if (rand == 1)
                {
                    // Lane 1
                    if (myPosX == pos1X)
                    {
                        LaneOne();
                    }
                    // Lane 2
                    else if (myPosX == pos2X)
                    {
                        LaneTwo();
                    }
                    // Lane 3
                    if (myPosX == pos3X)
                    {
                        LaneThree();
                    }
                    // Lane 4
                    else if (myPosX == pos4X)
                    {
                        LaneFour();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    private void LaneFour()
    {
        frontRightIndicator.Play();
        if (gameObject.transform.position.x > pos3X)
        {
            gameObject.transform.position -= hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            frontRightIndicator.Stop();
            myPosX += 1;
        }
    }

    private void LaneThree()
    {
        frontLeftIndicator.Play();
        if (gameObject.transform.position.x < pos4X)
        {
            gameObject.transform.position += hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            frontLeftIndicator.Stop();
            myPosX += 1;
        }
    }

    private void LaneTwo()
    {
        frontRightIndicator.Play();
        if (gameObject.transform.position.x > pos1X)
        {
            gameObject.transform.position -= hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            frontRightIndicator.Stop();
            myPosX += 1;
        }
    }

    private void LaneOne()
    {
        frontLeftIndicator.Play();
        if (gameObject.transform.position.x < pos2X)
        {
            gameObject.transform.position += hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            frontLeftIndicator.Stop();
            myPosX += 1;
        }
    }
}
