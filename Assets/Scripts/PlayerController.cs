using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 20f;
    private float hMoveSpeed;
    public SpawnManager spawnManager;
    public GameManager gameManager;

    public ParticleSystem coinCollected;
    public ParticleSystem explosion;
    public ParticleSystem gasCollected;

    public static float hMovement;
    private bool resetMovement;
    private bool carHasFuel;

    private float carMinX;
    private float carMaxX;

    // Variables for multiple cars
    private int currentCar;
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;

    // settings menu
    public GameObject deathScreen;
    public GameObject uiScreen;
    public GameObject arrowScreen;

    private float playerMaxSpeed;
    private float startSpeed = 10f;

    private void Awake()
    {
        // chooses car from playerprefs, sets car to 1 if not chosen
        if (!PlayerPrefs.HasKey("CarSelected")) PlayerPrefs.SetInt("CarSelected", 1);
        currentCar = PlayerPrefs.GetInt("CarSelected");
        ActivateUI();

        if (currentCar == 1)
        {
            car1.SetActive(true);
            car2.SetActive(false);
            car3.SetActive(false);
        }
        else if (currentCar == 2)
        {
            // car 2 is unlocked
            if (PlayerPrefs.GetInt("Car2Status") == 1)
            {
                car1.SetActive(false);
                car2.SetActive(true);
                car3.SetActive(false);
            }
            else
            {
                car1.SetActive(true);
                car2.SetActive(false);
                car3.SetActive(false);
            }
        }
        else if (currentCar == 3)
        {
            // car 3 is unlocked
            if (PlayerPrefs.GetInt("Car3Status") == 1)
            {
                car1.SetActive(false);
                car2.SetActive(false);
                car3.SetActive(true);
            }
            else
            {
                car1.SetActive(true);
                car2.SetActive(false);
                car3.SetActive(false);
            }
        }
        moveSpeed = startSpeed;
    }

    void Start()
    {
        if (gameObject.tag == "car1")
        {
            // top speed is 102
            playerMaxSpeed = 20.4f;
            // sets horizontal movement a bit slower
            hMoveSpeed = moveSpeed / 2f;
            carMinX = -3.44f;
            carMaxX = 3.35f;
        }
        else if (gameObject.tag == "car2")
        {
            // top speed is 158
            playerMaxSpeed = 31.6f;
            // sets horizontal movement a bit slower
            hMoveSpeed = moveSpeed / 1.9f;
            carMinX = -4.2f;
            carMaxX = 2f;
        }
        else if (gameObject.tag == "car3")
        {
            // top speed is 208
            playerMaxSpeed = 41f;
            // sets horizontal movement a bit slower
            hMoveSpeed = moveSpeed / 1.8f;
            carMinX = -3.3f;
            carMaxX = 3f;
        }

        carHasFuel = true;
    }

    void FixedUpdate()
    {
        if (carHasFuel == true)
        {
            gameManager.UseFuel();

            // Setting Z speed for gas pedals
            // if brakes arent pressed
            if (CrossPlatformInputManager.GetAxis("Vertical") > 0 && moveSpeed < playerMaxSpeed)
            {
                moveSpeed += CrossPlatformInputManager.GetAxis("Vertical");
            }
            else if (CrossPlatformInputManager.GetAxis("Vertical") < 0 && moveSpeed > startSpeed)
            {
                // brakes being pressed, minus brake speed
                // Player can only brake if the car is going faster than 10f
                moveSpeed += CrossPlatformInputManager.GetAxis("Vertical");
            }
            else if (moveSpeed > startSpeed)
            {
                // No input, gradually decrease move speed due to lack of acceleration
                moveSpeed -= .075f;
            }

            // Decide which control scheme to use
            if (PlayerPrefs.GetInt("ControlSystem") == 1)
            {
                // cross platform movement

                hMovement = CrossPlatformInputManager.GetAxis("Horizontal") * hMoveSpeed / 2;
            }
            else
            {
                hMovement = (Input.acceleration.x * 8);
            }
            if (gameObject.transform.position.x >= carMinX && gameObject.transform.position.x <= carMaxX)
            {
                //hMovement = Input.GetAxis("Horizontal") * hMoveSpeed;
                //hMovement = Input.acceleration.x;
                transform.Translate(new Vector3(hMovement, 0, moveSpeed) * Time.deltaTime);
                resetMovement = true;
            }
            else if (gameObject.transform.position.x <= carMinX)
            {
                if (resetMovement)
                {
                    hMovement = 0;
                    transform.Translate(new Vector3(hMovement, 0, moveSpeed) * Time.deltaTime);
                    resetMovement = false;
                }
                //if (hMovement != 0) hMovement = 0;
                //hMovement = Input.GetAxis("Horizontal") * hMoveSpeed;#
                //hMovement = Input.acceleration.x;
                if (hMovement >= 0)
                    transform.Translate(new Vector3(hMovement, 0, moveSpeed) * Time.deltaTime);
                else
                {
                    transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);
                }
            }
            else if (gameObject.transform.position.x >= carMaxX)
            {
                if (resetMovement)
                {
                    transform.Translate(new Vector3(hMovement, 0, moveSpeed) * Time.deltaTime);
                    resetMovement = false;
                }
                //if (hMovement != 0) hMovement = 0;
                //hMovement = Input.GetAxis("Horizontal") * hMoveSpeed;
                //hMovement = Input.acceleration.x;
                if (hMovement <= 0)
                    transform.Translate(new Vector3(hMovement, 0, moveSpeed) * Time.deltaTime);
                else
                {
                    transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);
                }
            }
            CheckCarFuel();
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("CoinCollected");
            gameManager.CoinCollected();

            Instantiate(coinCollected, other.transform.position, other.transform.rotation);
        }

        if (other.tag == "GasCan")
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Fuel1Collected");
            gameManager.FuelCollected();
            Instantiate(gasCollected, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 5f), other.transform.rotation);
        }

        if (other.tag == "EnemyCar")
        {
            //Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(explosion, other.transform.position + Vector3.forward, other.transform.rotation);
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Explosion");
            //StartCoroutine(WaitAndRestart(1f));
            //Destroy(gameObject);
            CheckCarFuel();
            if (carHasFuel == true && gameManager.currentFuel > 50)
            {
                gameManager.currentFuel -= 50f;
            }
            else
            {
                gameManager.currentFuel = 0;
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                gameManager.deathMessage.text = "You crashed into a car :(";
                gameManager.PlayerDied();
                ActivateDeathScreen();
                FindObjectOfType<AudioManager>().Play("GameOver");
            }
        }
    }

    private void CheckCarFuel()
    {
        if (gameManager.currentFuel <= 0)
        {
            gameManager.deathMessage.text = "You ran out of fuel :(";
            gameManager.PlayerDied();
            ActivateDeathScreen();
            carHasFuel = false;
            FindObjectOfType<AudioManager>().Play("GameOver");
        }
    }

    private void ActivateDeathScreen()
    {
        uiScreen.SetActive(false);
        deathScreen.SetActive(true);
    }

    private void ActivateUI()
    {
        deathScreen.SetActive(false);
        uiScreen.SetActive(true);
        if (PlayerPrefs.GetInt("ControlSystem") == 1)
        {
            arrowScreen.SetActive(true);
        }
        else
        {
            arrowScreen.SetActive(false);
        }

    }
}
