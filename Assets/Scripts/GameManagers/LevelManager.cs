using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//LevelManager: each level has a canvas with this levelmanager script.
//This controls the text panels before and after games - also scaling them to fit the device resolution,
//It also controls the game time slider and whether the game is playing or not. 
//In game scripts can reference this to start/end games and get information about the game type
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

    //Referencing Game Manager
    GameManager gameManager;

    //Referencing text panels
    public GameObject instructionsPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject menuButton;
    public GameObject panelBackground;
    float mainTextSize;
    float titleTextSize;
    float buttonTextSize;
    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start() {
        timeSlider.gameObject.SetActive(false);
        playing = false;
        timeRemaining = maxTime;

        //Setup text panels
        if(gameManager.isPlayingChooseGame() && gameManager.NumberOfGamesWon() > 0) {
            instructionsPanel.SetActive(false);
            StartGame();
        }
        else { instructionsPanel.SetActive(true); }
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        panelBackground.GetComponent<Image>().color = new Color32(161, 161, 161, 255);
        setFontSizes();
        setupAssets();
    }

    // Update is called once per frame
    void Update() {
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
    
    public void StartGame() {
        TurnOffInstructions();
        timeSlider.gameObject.SetActive(true);   //Turn slider on
        playing = true;
    }

    float CalculateSliderValue() {
        return timeRemaining/ maxTime;
    }

    public void GameWon() {
        EndGame();
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        panelBackground.GetComponent<Image>().color = new Color32(74, 156, 48, 255);
        
        gameManager.AddToGamesWon();
        int wins = gameManager.NumberOfGamesWon();
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
        EndGame();
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        panelBackground.GetComponent<Image>().color = new Color32(190, 74, 44, 255);

        int wins = gameManager.NumberOfGamesWon();
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

    public void NextGame() {
        if(gameManager.isPlayingChooseGame()) {
            gameManager.PlaySameGame();
        }
        else {
            gameManager.NextGame();
        }
    }

    public void RestartGame() {
        gameManager.ResetGame();
        NextGame();
    }

    public void ToMainMenu() {
        gameManager.ToMainMenu();
    }

    public int getNumberGamesWon() {
        return gameManager.NumberOfGamesWon();
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
        panelBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT); //background

        timeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        timeSlider.GetComponent<RectTransform>().localPosition = new Vector2(0, SCREEN_HEIGHT*0.5f);

        menuButton.GetComponent<RectTransform>().localPosition = new Vector2(-(SCREEN_WIDTH*0.5f)+(SCREEN_WIDTH*0.1f), (SCREEN_HEIGHT*0.5f)-(SCREEN_HEIGHT*0.1f));
        menuButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = mainTextSize;
        
        scaleInstructions(instructionsPanel);
        scaleInstructions(winPanel);
        scaleInstructions(losePanel);
    }

    //Do the scaling transformations for the panels
    public void scaleInstructions(GameObject panel) {
        GameObject titleText = panel.transform.GetChild(0).gameObject;
        titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        titleText.GetComponent<RectTransform>().localPosition = new Vector2(0, (SCREEN_HEIGHT*0.5f)-SCREEN_HEIGHT*0.25f);
        titleText.GetComponent<TextMeshProUGUI>().fontSize = titleTextSize;

        GameObject mainText = panel.transform.GetChild(1).gameObject;
        mainText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH*0.75f, SCREEN_HEIGHT*0.25f);
        mainText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        mainText.GetComponent<TextMeshProUGUI>().fontSize = mainTextSize;

        GameObject playButton = panel.transform.GetChild(2).gameObject;
        playButton.GetComponent<RectTransform>().localPosition = new Vector2(0, -(SCREEN_HEIGHT*0.25f));
        playButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonTextSize;
    }

    public void TurnOffInstructions() {
        panelBackground.SetActive(false);
        menuButton.SetActive(false);
        instructionsPanel.SetActive(false); //Turn instructions off
    }

    public void EndGame() {
        playing = false;
        panelBackground.SetActive(true);
        menuButton.SetActive(true);
        timeSlider.gameObject.SetActive(false);
    }
}
