using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private int initAmount = 10;
    private int spawnInterval = 11;
    private int lastSpawnZ = 77;
    private int spawnAmount = 4;

    public List<GameObject> enemies;
    private List<float> xVals;

    public GameObject coins;
    public GameObject gasCans;

    // float for distance to spawn entities
    private float spawnDistance = 300f;
    private GameObject player;
    private int gasCanSpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // When spawning enemies.
        // x co-ordinate should be -3.75/-1.25/1.25/3.75#
        // Instantiate list object, create an instance
        xVals = new List<float>();
        // Populate xvals with list of possible x-values for enemies
        xVals.Add(-3f);
        xVals.Add(-0.9f);
        xVals.Add(0.9f);
        xVals.Add(3f);

        SpawnAtStart();
        gasCanSpawn = 0;
    }

    // Update is called once per frame
    // When spawning enemies.
    // y co-ordinate should be -0.55
    // Quaternion should be 180 degrees on y-axis
    void Update()
    {
        SpawnEnemies();
    }

    private void FixedUpdate()
    {
        // Spawn a gas can in a random lane
        SpawnGasCan();
    }

    public void SpawnEnemies()
    {
        // choose random x co-ord
        int xv = Random.Range(0, 4);
        int prevXv = 5;

        if (player.gameObject != null)
        {
            // float to retreive distance from player to lastSpawnz
            float distance = Vector3.Distance(player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, lastSpawnZ));

            // if distance from player is greater than spawnDistance spawn entities
            if (distance < spawnDistance)
            {
                lastSpawnZ += spawnInterval;
                // 33% chance
                for (int i = 0; i < spawnAmount; i++)
                {
                    if (xv != prevXv)
                    {
                        if (Random.Range(0, 4) == 0)
                        {
                            GameObject enemy = enemies[Random.Range(0, enemies.Count)];
                            if (xv == 0)
                            {
                                // Spawn enemy in Lane 1
                                Instantiate(enemy, new Vector3(xVals[xv], -0.55f, lastSpawnZ), enemy.transform.rotation);
                                // spawns coins at opposite x of enemy car (lane 2)
                                Instantiate(coins, new Vector3(xVals[xv + 1], -0.4f, lastSpawnZ), coins.transform.rotation);
                            }
                            else if (xv == 1)
                            {
                                // Spawn enemy in Lane 2
                                Instantiate(enemy, new Vector3(xVals[xv], -0.55f, lastSpawnZ), enemy.transform.rotation);
                                // spawns coins at opposite x of enemy car (lane 1)
                                Instantiate(coins, new Vector3(xVals[xv - 1], -0.4f, lastSpawnZ), coins.transform.rotation);
                            }
                            else if (xv == 2)
                            {
                                // Spawn enemy in Lane 3
                                Instantiate(enemy, new Vector3(xVals[xv], -0.55f, lastSpawnZ), enemy.transform.rotation);
                                // spawns coins at opposite x of enemy car (lane 4)
                                Instantiate(coins, new Vector3(xVals[xv + 1], -0.4f, lastSpawnZ), coins.transform.rotation);
                            }
                            else if (xv == 3)
                            {
                                // Spawn enemy in Lane 4
                                Instantiate(enemy, new Vector3(xVals[xv], -0.55f, lastSpawnZ), enemy.transform.rotation); //new Quaternion(0, 180, 0, 0
                                                                                                   // spawns coins at opposite x of enemy car (lane 3)
                                Instantiate(coins, new Vector3(xVals[xv - 1], -0.4f, lastSpawnZ), coins.transform.rotation);
                            }
                            prevXv = xv;
                        }

                        else if ((Random.Range(0, 4) == 1))
                        {
                            if (Random.Range(0, 3) == 1)
                            {
                                // Generate a coin in a random lane
                                Instantiate(coins, new Vector3(xVals[Random.Range(0, 4)], -0.4f, lastSpawnZ), coins.transform.rotation);
                            }
                        }
                    }
                    if (Random.Range(0, 60) == 1)
                    {
                        // spawn 5 coins in a row
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ - 2), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ - 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ + 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ + 2), coins.transform.rotation);
                    }
                    else if (Random.Range(0, 60) == 2)
                    {
                        Instantiate(coins, new Vector3(xVals[1], -0.4f, lastSpawnZ - 2), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[1], -0.4f, lastSpawnZ - 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[1], -0.4f, lastSpawnZ), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[1], -0.4f, lastSpawnZ + 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ + 2), coins.transform.rotation);
                    }
                    else if (Random.Range(0, 60) == 3)
                    {
                        Instantiate(coins, new Vector3(xVals[2], -0.4f, lastSpawnZ - 2), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[2], -0.4f, lastSpawnZ - 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[2], -0.4f, lastSpawnZ), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[2], -0.4f, lastSpawnZ + 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[0], -0.4f, lastSpawnZ + 2), coins.transform.rotation);
                    }
                    else if (Random.Range(0, 60) == 4)
                    {
                        Instantiate(coins, new Vector3(xVals[3], -0.4f, lastSpawnZ - 2), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[3], -0.4f, lastSpawnZ - 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[3], -0.4f, lastSpawnZ), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[3], -0.4f, lastSpawnZ + 1), coins.transform.rotation);
                        Instantiate(coins, new Vector3(xVals[3], -0.4f, lastSpawnZ + 2), coins.transform.rotation);
                    }
                }
                SpawnGasCan();
            }
        }
    }

    public void SpawnGasCan()
    {
         if (gasCanSpawn <= 500)
         {
             gasCanSpawn++;

             if (gasCanSpawn == 500)
             {
                 // Spawn a gas can at intervals
                 Instantiate(gasCans, new Vector3(xVals[Random.Range(0, 4)], -0.4f, lastSpawnZ), gasCans.transform.rotation);
                 gasCanSpawn = 0;
             }
         }
    }
    void SpawnAtStart()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnEnemies();
        }
    }
}
