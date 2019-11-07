using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //Game Slider Setup
    public bool usingTimer = false;
    float timeRemaining;
    public float maxTime = 15f;
    public Slider timeSlider;
    bool playing;
    //Game Points Setup
    public bool usingPoints = false;
    public int maxPoints = 3;

    //Referencing Game Manager
    GameObject gameManager;
    GameManager gameManagerScript;

    //Referencing text panels
    public GameObject instructionsPanel;
    public GameObject randomWinPanel;
    public GameObject chooseWinPanel;
    GameObject winPanel;
    public GameObject losePanel;
    float mainTextSize;
    float titleTextSize;
    float buttonTextSize;
    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    //Mouse Position for touch/click raycast
    Vector3 mousePos;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        timeSlider.gameObject.SetActive(false);
        playing = false;
        timeRemaining = maxTime;

        gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();

        if(gameManagerScript.isPlayingChooseGame()) {
            winPanel = chooseWinPanel;
        }
        else {
            winPanel = randomWinPanel;
        }
        instructionsPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        
        setFontSizes();
        setupAssets();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playing && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
            //Raycast for touch/click detection
            mousePos = Input.mousePosition;
            hit = Physics2D.Raycast(mousePos,Vector2.zero);

            if (hit) {
                if(hit.collider.gameObject.CompareTag("NextGame")) {
                    if(gameManagerScript.isPlayingChooseGame()) {
                        gameManagerScript.PlaySameGame();
                    }
                    else {
                        gameManagerScript.NextGame();
                    }
                }
                else if(hit.collider.gameObject.CompareTag("Respawn")) {
                    if(gameManagerScript.isPlayingChooseGame()) {
                        gameManagerScript.StartGame("choose");
                    }
                    else {
                        gameManagerScript.StartGame("random");
                    }
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
        if(playing && usingTimer) {
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
        playing = true;
    }

    float CalculateSliderValue() {
        return timeRemaining/ maxTime;
    }

    public void GameWon() {
        playing = false;
        gameManagerScript.AddToGamesWon();
        winPanel.SetActive(true);
        
        int wins = gameManagerScript.NumberOfGamesWon();
        string winText;
        if(wins == 1) {
            winText = "You've won 1 game!";
        }
        else {
            winText = "You've won " + wins + " games in a row!";
        }
        winPanel.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = winText;
    }

    public void GameLost() {
        playing = false;
        losePanel.SetActive(true);

        int wins = gameManagerScript.NumberOfGamesWon();
        string loseText;
        if(wins == 0) {
            loseText = "You won no games this time.";
        }
        else if(wins == 1) {
            loseText = "You won 1 game this time.";
        }
        else {
            loseText = "You won " + wins + " games this time.";
        }
        losePanel.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "You ran out of time to finish that game.\n\n" + loseText;
    }

    public bool isPlaying() {
        return playing;
    }

    public void setFontSizes() {
        mainTextSize = SCREEN_HEIGHT/20.5f;
        titleTextSize = SCREEN_HEIGHT/15f;
        buttonTextSize = SCREEN_HEIGHT/19f;
    }


    //Scale instructions/win/lose text panels
    public void setupAssets() {
        timeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        timeSlider.GetComponent<RectTransform>().localPosition = new Vector2(0, SCREEN_HEIGHT*0.5f);
        
        GameObject instructionsPanel = gameObject.transform.GetChild(1).gameObject;
        scaleInstructions(instructionsPanel);

        GameObject winPanel = gameObject.transform.GetChild(2).gameObject;
        scaleInstructions(winPanel);

        GameObject losePanel = gameObject.transform.GetChild(3).gameObject;
        scaleInstructions(losePanel);
    }

    //Do the scaling transformations for the panels
    public void scaleInstructions(GameObject panel) {
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT);

        GameObject titleText = panel.transform.GetChild(0).gameObject;
        titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        titleText.GetComponent<RectTransform>().localPosition = new Vector2(0, (SCREEN_HEIGHT*0.5f)-SCREEN_HEIGHT*0.25f);
        titleText.GetComponent<TextMeshProUGUI>().fontSize = titleTextSize;

        GameObject mainText = panel.transform.GetChild(1).gameObject;
        mainText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH*0.75f, SCREEN_HEIGHT*0.25f);
        mainText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        mainText.GetComponent<TextMeshProUGUI>().fontSize = mainTextSize;

        if(panel.transform.childCount > 3) {    //If lose panel
            GameObject playAgainButton = panel.transform.GetChild(2).gameObject;
            playAgainButton.GetComponent<RectTransform>().localPosition = new Vector2(-(SCREEN_HEIGHT*0.25f), -(SCREEN_HEIGHT*0.25f));
            playAgainButton.GetComponent<TextMeshProUGUI>().fontSize = buttonTextSize;
            GameObject menuButton = panel.transform.GetChild(3).gameObject;
            menuButton.GetComponent<RectTransform>().localPosition = new Vector2(SCREEN_HEIGHT*0.25f, -(SCREEN_HEIGHT*0.25f));
            menuButton.GetComponent<TextMeshProUGUI>().fontSize = buttonTextSize;
        }
        else {  //If win or instructions panel
            GameObject playButton = panel.transform.GetChild(2).gameObject;
            playButton.GetComponent<RectTransform>().localPosition = new Vector2(0, -(SCREEN_HEIGHT*0.25f));
            playButton.GetComponent<TextMeshProUGUI>().fontSize = buttonTextSize;
        }
    }
}
