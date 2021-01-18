using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    private static int highestDistance;
    private static int totalCoins;
    private int distance;

    public float currentFuel = 100f;
    public float maxFuel = 100f;
    public float burnRate = 10;

    public Text uiDistance;
    public Text uiHighestDistance;
    public Text uiCoins;
    public Slider fuelIndicator;
    public Text fuelTxt;
    public Text deathMessage;

    public Text deathHighScore;
    public Text deathScore;
    public Text coinsCollected;


    private void Awake()
    {
        highestDistance = PlayerPrefs.GetInt("HighScore");
        uiHighestDistance.text = "Highest: " + highestDistance.ToString() + "m";
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
        uiCoins.text = totalCoins.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }

        fuelIndicator.maxValue = maxFuel;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Stops MissingReferenceException
        if (gameObject != null && player.gameObject != null)
        {
            distance = Mathf.RoundToInt(player.transform.position.z);

            uiDistance.text = "Current: " + distance.ToString() + "m";
            uiHighestDistance.text = "Highest: " + highestDistance.ToString() + "m";

            uiCoins.text = totalCoins.ToString();

            UpdateHighScore();
        }
    }

    public void CoinCollected()
    {
        totalCoins++;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }

    public void FuelCollected()
    {
        // when the player collects fuel, add 20 fuel to total and
        if (currentFuel < maxFuel - 20f)
        {
            currentFuel += 20f;
        }
        else
        {
            currentFuel = maxFuel;
        }
        // sets the fuel consumption rate slightly higher
        burnRate += .2f;
    }

    void UpdateUI()
    {
        fuelIndicator.value = currentFuel;
        fuelTxt.text = "Fuel: " + currentFuel.ToString("0") + "%";
        if (currentFuel <= 0)
        {
            currentFuel = 0;
            fuelTxt.text = "Fuel Empty!";
        }
    }

    public void UseFuel()
    {
        currentFuel -= burnRate * Time.deltaTime;
        UpdateUI();
    }

    public void UpdateHighScore()
    {
        if (distance > highestDistance)
        {
            highestDistance = distance;

            PlayerPrefs.SetInt("HighScore", highestDistance);
        }
    }


    public void PlayerDied()
    {
        deathHighScore.text = "High: " + highestDistance + "m";
        deathScore.text = "Score: " + distance + "m";
        coinsCollected.text = totalCoins.ToString();
        distance = 0;
    }
}
