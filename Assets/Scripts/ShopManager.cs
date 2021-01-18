using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text msgToUser;
    public Text totalCoins;

    public Button level1;
    public Button level2;
    public Button level3;
    public Button car1;
    public Button car2;
    public Button car3;

    private int startCoins;


    // Start is called before the first frame update
    void Start()
    {
        // initialize level and car status to 0 (locked)
        if (!PlayerPrefs.HasKey("Level2Status")) PlayerPrefs.SetInt("Level2Status", 0);
        if (!PlayerPrefs.HasKey("Level3Status")) PlayerPrefs.SetInt("Level3Status", 0);
        if (!PlayerPrefs.HasKey("Car2Status")) PlayerPrefs.SetInt("Car2Status", 0);
        if (!PlayerPrefs.HasKey("Car3Status")) PlayerPrefs.SetInt("Car3Status", 0);
        msgToUser.text = "";

        startCoins = PlayerPrefs.GetInt("TotalCoins");

        level1.enabled = false;
        car1.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        totalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();


        // if they already have the item unlocked, disable button. This has priority over affordability!
        if (PlayerPrefs.GetInt("Level2Status") == 1)
        {
            level2.enabled = false;
        }
        // if the player doesn't have enough coins, make button non interactable
        else if (startCoins < 1500)
        {
            level2.interactable = false;
        }

        if (PlayerPrefs.GetInt("Level3Status") == 1)
        {
            level3.enabled = false;
        }
        else if (startCoins < 3000)
        {
            level3.interactable = false;
        }


        if (PlayerPrefs.GetInt("Car2Status") == 1)
        {
            car2.enabled = false;
        }
        else if (startCoins < 10000)
        {
            car2.interactable = false;
        }


        if (PlayerPrefs.GetInt("Car3Status") == 1)
        {
            car3.enabled = false;
        }
        else if (startCoins < 20000)
        {
            car3.interactable = false;
        }
    }

    // Level 2 transaction (completed on button click)
    public void Level2()
    {
        if (PlayerPrefs.GetInt("Level2Status") == 0)
        {
            // process transaction
            int newCoinValue = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("Level2Status", 1);
            newCoinValue -= 1500;
            startCoins = newCoinValue;
            PlayerPrefs.SetInt("TotalCoins", newCoinValue);
            msgToUser.color = Color.green;
            msgToUser.text = "Congrats, you just bought Level 2!";
        }
    }

    // Level 2 message (output on image click)
    public void Level2Msg()
    {
        if (PlayerPrefs.GetInt("Level2Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Level 2 is Unlocked";
            PlayerPrefs.SetInt("GameLevel", 3);
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Level 2 is locked ";
        }
    }

    // Level 3 transaction (completed on button click)
    public void Level3()
    {
        if (PlayerPrefs.GetInt("Level3Status") == 0)
        {
            // process transaction
            int newCoinValue = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("Level3Status", 1);
            newCoinValue -= 3000;
            startCoins = newCoinValue;
            PlayerPrefs.SetInt("TotalCoins", newCoinValue);
            msgToUser.color = Color.green;
            msgToUser.text = "Congrats, you just bought Level 3!";

        }
    }

    // Level 3 message (output on image click)
    public void Level3Msg()
    {
        if (PlayerPrefs.GetInt("Level3Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Level 3 is Unlocked";
            PlayerPrefs.SetInt("GameLevel", 3);
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Level 3 is locked";
        }
    }


    // Car 2 transaction (completed on button click)
    public void Car2()
    {
        if (PlayerPrefs.GetInt("Car2Status") == 0)
        {
            // process transaction
            int newCoinValue = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("Car2Status", 1);
            newCoinValue -= 10000;
            startCoins = newCoinValue;
            PlayerPrefs.SetInt("TotalCoins", newCoinValue);
            msgToUser.color = Color.green;
            msgToUser.text = "Congrats, you just bought Car 2!";

        }
    }

    // Car 2 message (output on image click)
    public void Car2Msg()
    {
        if (PlayerPrefs.GetInt("Car2Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Car 2 is Unlocked";
            PlayerPrefs.SetInt("CarSelected", 2);
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Car 2 is locked";
        }
    }

    // Car 3 transaction (completed on button click)
    public void Car3()
    {
        if (PlayerPrefs.GetInt("Car3Status") == 0)
        {
            // process transaction
            int newCoinValue = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("Car3Status", 1);
            newCoinValue -= 20000;
            startCoins = newCoinValue;
            PlayerPrefs.SetInt("TotalCoins", newCoinValue);
            msgToUser.color = Color.green;
            msgToUser.text = "Congrats, you just bought Car 3!";
        }
    }

    // Car 3 message (output on image click)
    public void Car3Msg()
    {
        if (PlayerPrefs.GetInt("Car3Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Car 3 is Unlocked";
            PlayerPrefs.SetInt("CarSelected", 3);
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Car 3 is locked";
        }
    }

    // Methods for car 1 and level 1 (Free)
    public void FreeLevel1()
    {
        msgToUser.color = Color.green;
        msgToUser.text = "You already own Level 1";
    }

    public void FreeCar1()
    {
        msgToUser.color = Color.green;
        msgToUser.text = "You already own Car 1";
    }
}
