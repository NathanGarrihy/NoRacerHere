using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarSelecter : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;

    public Text msgToUser;
    private int carSelected;
    // Start is called before the first frame update
    void Start()
    {
        carSelected = PlayerPrefs.GetInt("CarSelected");
        // Decides which car to use
        if(carSelected == 1)
        {
            LoadCar1();
        }
        else if (carSelected == 2)
        {
            LoadCar2();
        }
        else if (carSelected == 3)
        {
            LoadCar3();
        }
    }

    public void LoadCar1() 
    {
        msgToUser.color = Color.green;
        msgToUser.text = "Car 1 selected";
        car1.SetActive(true);
        car2.SetActive(false);
        car3.SetActive(false);

        carSelected = 1;
        SaveSelection();
    }

    public void LoadCar2()
    {
        if (PlayerPrefs.GetInt("Car2Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Car 2 selected";
            car1.SetActive(false);
            car2.SetActive(true);
            car3.SetActive(false);

            carSelected = 2;
            SaveSelection();
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Car 2 is locked!";
        }
    }

    public void LoadCar3() 
    {
        if (PlayerPrefs.GetInt("Car3Status") == 1)
        {
            msgToUser.color = Color.green;
            msgToUser.text = "Car 2 selected";
            car1.SetActive(false);
            car2.SetActive(false);
            car3.SetActive(true);

            carSelected = 3;
            SaveSelection();
        }
        else
        {
            msgToUser.color = Color.red;
            msgToUser.text = "Car 3 is locked!";
        }
    }
    
    private void SaveSelection()
    {
        PlayerPrefs.SetInt("CarSelected", carSelected);
    }
}
