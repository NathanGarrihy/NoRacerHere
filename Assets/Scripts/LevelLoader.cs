using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private GameObject menuCanvis;
    private GameObject settingsCanvis;
    private GameObject levelSelectCanvis;
    private GameObject shopCanvis;
    private GameObject tutorialCanvas;
    private static int currentLevel;

    public Slider[] volumeSliders;
    public Toggle accelerometer;
    public Toggle arrow;
    public int[] controlType;

    public Text msgToUser;
    public Animator transition;

    public GameObject pauseMenu;
    public Button pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        // Deactivates 
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (msgToUser != null)
        {
            msgToUser.text = "";
        }

        // Menu level
        if (currentLevel == 0)
        {
            // finds menu canvis objects
            menuCanvis = GameObject.Find("MainMenuCanvis");
            settingsCanvis = GameObject.Find("SettingsCanvas");
            levelSelectCanvis = GameObject.Find("LevelSelectCanvis");
            shopCanvis = GameObject.Find("ShopCanvas");
            tutorialCanvas = GameObject.Find("TutorialCanvas");

            // sets only main menu active
            tutorialCanvas.SetActive(false);
            shopCanvis.SetActive(false);
            levelSelectCanvis.SetActive(false);
            settingsCanvis.SetActive(false);
            menuCanvis.SetActive(true);

            // Initializes playerprefs if not initialized already
            if (PlayerPrefs.GetFloat("MasterVolume") != 0f)
            {
                volumeSliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
            }
            if (PlayerPrefs.GetFloat("MusicVolume") != 0f)
            {
                volumeSliders[1].value = PlayerPrefs.GetFloat("MusicVolume");
            }
            if (PlayerPrefs.GetFloat("SfxVolume") != 0f)
            {
                volumeSliders[2].value = PlayerPrefs.GetFloat("SfxVolume");
            }

            if (PlayerPrefs.GetInt("ControlSystem") == 1)
            {
                accelerometer.isOn = false;
                arrow.isOn = true;
            }
            else
            {
                arrow.isOn = false;
                accelerometer.isOn = true;
            }
        }
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadMyLevel(level));
        currentLevel = level;
    }

    // Plays splash screen while loading
    IEnumerator LoadMyLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(1);

        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        tutorialCanvas.SetActive(false);
        shopCanvis.SetActive(false);
        settingsCanvis.SetActive(false);
        levelSelectCanvis.SetActive(false);
        menuCanvis.SetActive(true);
    }

    public void LoadMenuFromLevelSelect()
    {
        tutorialCanvas.SetActive(false);
        shopCanvis.SetActive(false);
        settingsCanvis.SetActive(false);
        levelSelectCanvis.SetActive(false);
        menuCanvis.SetActive(true);
    }

    public void LoadSettings()
    {
        tutorialCanvas.SetActive(false);
        shopCanvis.SetActive(false);
        levelSelectCanvis.SetActive(false);
        menuCanvis.SetActive(false);
        settingsCanvis.SetActive(true);
    }

    public void LoadLevelSelect()
    {
        tutorialCanvas.SetActive(false);
        shopCanvis.SetActive(false);
        settingsCanvis.SetActive(false);
        menuCanvis.SetActive(false);
        levelSelectCanvis.SetActive(true);
    }

    public void LoadShop()
    {
        tutorialCanvas.SetActive(false);
        settingsCanvis.SetActive(false);
        menuCanvis.SetActive(false);
        levelSelectCanvis.SetActive(false);
        shopCanvis.SetActive(true);
    }
    public void LoadTutorial()
    {
        shopCanvis.SetActive(false);
        settingsCanvis.SetActive(false);
        menuCanvis.SetActive(false);
        levelSelectCanvis.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(currentLevel);
    }

    // Methods to globally set and save player preferrences
    public void SetMasterVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
        AudioListener.volume = slider.value;
    }

    public void SetMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }

    public void SetSfxVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SfxVolume", slider.value);
    }

    public void SetControlSystem(int i)
    {
        // 1 or 2 for control mechanics
        PlayerPrefs.SetInt("ControlSystem", i);
    }

    public void SetGameLevel(int i)
    {
        PlayerPrefs.SetInt("GameLevel", i);
    }

    // Loads saved playerpref level
    public void LoadPrefLevel()
    {
        currentLevel = PlayerPrefs.GetInt("GameLevel");
        if (currentLevel == 1)
        {
            LoadLevel(currentLevel);
        }
        else if (currentLevel == 2)
        {
            if (PlayerPrefs.GetInt("Level2Status") == 1)
            {
                currentLevel = PlayerPrefs.GetInt("GameLevel");
                LoadLevel(currentLevel);
            }
            else
            {
                msgToUser.text = "Level 2 Locked";
            }
        }
        else if (currentLevel == 3)
        {
            if (PlayerPrefs.GetInt("Level3Status") == 1)
            {
                LoadLevel(currentLevel);
            }
            else
            {
                msgToUser.text = "Level 3 Locked";
            }
        }
        // if you don't load a scene, we reset the playerpref to level 1
        PlayerPrefs.SetInt("GameLevel", 1);
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Pause");
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("UnPause");
    }

    public void ButtonPressed()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.interactable = false;
        FindObjectOfType<AudioManager>().Play("UnPause");
    }

    public void CoinCheats()
    {
        PlayerPrefs.SetInt("TotalCoins", 35000);
    }
}
