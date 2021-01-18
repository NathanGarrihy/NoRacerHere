using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementLvl2 : MonoBehaviour
{
    private GameObject player;

    // for lane switching
    private float pos1X, pos2X, pos3X, pos4X, myPosX;
    private float enemyMoveSpeed;

    private Vector3 hMoveVector = new Vector3(0.02f, 0f, 0f);
    private Vector3 vMoveVector;
    int rand;

    // indicators
    public ParticleSystem backRightIndicator;
    public ParticleSystem backLeftIndicator;
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

        enemyMoveSpeed = .2f;

        player = GameObject.Find("Player");
        vMoveVector = new Vector3(0f, 0f, enemyMoveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Stops MissingReferenceException
        if (gameObject != null && player.gameObject != null)
        {
            // lane 1 and 2
            if (myPosX == pos1X || myPosX == pos2X)
            {
                // move away from player
                gameObject.transform.position += vMoveVector;
            }
            // lane 3 and 4
            else if (myPosX == pos3X || myPosX == pos4X)
            {
                // move towards player
                gameObject.transform.position -= vMoveVector;
            }
            // if car is close enough to player
            if (player.transform.position.z + 40 > gameObject.transform.position.z)
            {
                // Cars Move forward
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
        backLeftIndicator.Play();
        if (gameObject.transform.position.x > pos1X)
        {
            gameObject.transform.position -= hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            backLeftIndicator.Stop();
            myPosX += 1;
        }
    }

    private void LaneOne()
    {
        backRightIndicator.Play();
        if (gameObject.transform.position.x < pos2X)
        {
            gameObject.transform.position += hMoveVector;

        }
        else
        {
            // stop indicators and exit statement
            backRightIndicator.Stop();
            myPosX += 1;
        }
    }
}

