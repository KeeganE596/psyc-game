using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //Game Slider Setup
    float timeRemaining;
    float maxTime = 8f;
    public Slider timeSlider;
    bool startTimer;

    //Referencing Game Manager
    GameObject gameManager;
    GameManager gameManagerScript;

    //Referencing text panels
    public GameObject instructionsPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    //Mouse Position for touch/click raycast
    Vector3 mousePos;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        instructionsPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        timeSlider.gameObject.SetActive(false);
        startTimer = false;
        timeRemaining = maxTime;

        gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!startTimer && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
            //Raycast for touch/click detection
            mousePos = Input.mousePosition;
            hit = Physics2D.Raycast(mousePos,Vector2.zero);

            if (hit) {
                if(hit.collider.gameObject.CompareTag("NextGame")) {
                    gameManagerScript.NextGame();
                }
                else if(hit.collider.gameObject.CompareTag("Respawn")) {
                    gameManagerScript.StartGame();
                }
                else if(hit.collider.gameObject.CompareTag("Finish")) {
                    gameManagerScript.ToMainMenu();
                }
                else if(hit.collider.gameObject.CompareTag("Play")) {
                    StartGame();
                }
            }
        }
        

        //Start game timer/slider
        if(startTimer) {
            timeSlider.value = CalculateSliderValue();

            if(timeRemaining <= 0) {
                timeRemaining = 0;
                GameLost();
            }
            if(timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
        }
    }
    
    //Starts the timer and game when user clicks play button
    public void StartGame() {
        instructionsPanel.SetActive(false); //Turn instructions off
        timeSlider.gameObject.SetActive(true);   //Turn slider on
        startTimer = true;
    }

    float CalculateSliderValue() {
        return timeRemaining/ maxTime;
    }

    public void GameWon() {
        startTimer = false;
        gameManagerScript.AddToGamesWon();
        winPanel.SetActive(true);
        string winText = "You've won " + gameManagerScript.NumberOfGamesWon() + " games in a row!";
        winPanel.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = winText;
    }

    public void GameLost() {
        startTimer = false;
        losePanel.SetActive(true);
        string loseText = "You won " + gameManagerScript.NumberOfGamesWon() + " games this time.";
        losePanel.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = loseText;
    }
}
