using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;

    // for lane switching
    private float pos1X, pos2X, pos3X, pos4X, myPosX;
    private float enemyMoveSpeed;

    public float speed = .01f;
    private Vector3 hMoveVector = new Vector3(0.02f, 0f, 0f);
    private Vector3 vMoveVector;
    private int rand;

    // Only back indicators required for level 1
    public ParticleSystem backRightIndicator;
    public ParticleSystem backLeftIndicator;

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

        enemyMoveSpeed = .1f;            

        player = GameObject.Find("Player");
        // set Vector to move enemy on vertical axis
        vMoveVector = new Vector3(0f, 0f, enemyMoveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // all lanes
        // move away from player
        gameObject.transform.position += vMoveVector;

        // Stops MissingReferenceException
        if (gameObject != null && player.gameObject != null)
        {
            // if car is close enough to player
            if (player.transform.position.z + 40 > gameObject.transform.position.z)
            {
                // Cars Chane Lane
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
        backLeftIndicator.Play();
        // move to lane 3
        if (gameObject.transform.position.x > pos3X)
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

    private void LaneThree()
    {
        backRightIndicator.Play();
        // move to lane 4
        if (gameObject.transform.position.x < pos4X)
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

    private void LaneTwo()
    {
        backLeftIndicator.Play();
        // move to lane 1
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
        // move to lane 2
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
