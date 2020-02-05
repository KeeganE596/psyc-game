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
    bool winOnTimeOut = false;

    //Referencing Game Manager and Color Manager
    GameManager gameManager;
    ColorManager colorManager;
    bool playingChooseGame;

    //Referencing text panels
    public GameObject instructionsPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject menuButton;
    public GameObject panelBackground;

    //Referencing pause objects
    public GameObject pausePanel;
    public GameObject pauseButton;
    float mainTextSize;
    float titleTextSize;
    float buttonTextSize;
    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        colorManager = GameObject.FindWithTag("ColorManager").GetComponent<ColorManager>();

        playingChooseGame = gameManager.isPlayingChooseGame();
    }

    // Start is called before the first frame update
    void Start() {
        if(GameManager.swipeType != "default") {
            playingChooseGame = false;
        }

        timeSlider.gameObject.SetActive(false);
        playing = false;
        timeRemaining = maxTime;

        pauseButton.SetActive(false);
        pausePanel.SetActive(false);

        panelBackground.GetComponent<Image>().color = colorManager.GetColor();
        colorManager.setCameraBackground();
        
        //Setup text panels
        if(playingChooseGame && gameManager.NumberOfGamesWon() > 0) {
            instructionsPanel.SetActive(false);
            StartGame();
        }
        else { instructionsPanel.SetActive(true); }
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        
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
                if(winOnTimeOut) {
                    GameWon();
                } else {
                    GameLost();
                }
            }
            if(timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
        }
    }
    
    public void StartGame() {
        TurnOffInstructions();
        if(usingTimer) {
            timeSlider.gameObject.SetActive(true);   //Turn slider on
        }
        pauseButton.SetActive(true);    //Turn on pause button
        playing = true;
    }

    float CalculateSliderValue() {
        return timeRemaining/ maxTime;
    }

    public void GameWon() {
        StartCoroutine("WaitAtEndGameWon");
    }

    public void GameLost() {
        StartCoroutine("WaitAtEndOfGameLost");
    }

    public void NextGame() {
        if(playingChooseGame) {
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

    public void PlayAgain() {
        if(playingChooseGame) {
            gameManager.PlayAgain();
            NextGame();
        }
        else {
            gameManager.ResetGame();
            NextGame();
        }
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
    void setupAssets() {
        panelBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT); //background
        panelBackground.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        timeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH*0.85f, SCREEN_HEIGHT*0.06f);
        timeSlider.GetComponent<RectTransform>().localPosition = new Vector2(0, 0+SCREEN_HEIGHT*0.45f);

        menuButton.GetComponent<RectTransform>().localPosition = new Vector2(-(SCREEN_WIDTH*0.5f)+(SCREEN_WIDTH*0.1f), (SCREEN_HEIGHT*0.5f)-(SCREEN_HEIGHT*0.1f));
        menuButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = mainTextSize;

        scalePausePanel();
        scaleInstructions(instructionsPanel);
        scaleInstructions(winPanel);
        scaleInstructions(losePanel);
    }

    //Do the scaling transformations for the panels
    void scaleInstructions(GameObject panel) {
        GameObject titleText = panel.transform.GetChild(0).gameObject;
        titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        titleText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0+(SCREEN_HEIGHT*0.25f));
        titleText.GetComponent<TextMeshProUGUI>().fontSize = titleTextSize;

        GameObject mainText = panel.transform.GetChild(1).gameObject;
        mainText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH*0.75f, SCREEN_HEIGHT*0.25f);
        mainText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        mainText.GetComponent<TextMeshProUGUI>().fontSize = mainTextSize;

        GameObject playButton = panel.transform.GetChild(2).gameObject;
        playButton.GetComponent<RectTransform>().localPosition = new Vector2(0, 0-(SCREEN_HEIGHT*0.25f));
        playButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonTextSize;

        if(panel.transform.childCount > 3) {
            mainText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0-(SCREEN_HEIGHT*0.1f));

            GameObject sparksScore = panel.transform.GetChild(3).gameObject;
            sparksScore.GetComponent<RectTransform>().localPosition = new Vector2(0-(SCREEN_WIDTH*0.05f), 0+(SCREEN_HEIGHT*0.1f));
            sparksScore.GetComponent<TextMeshProUGUI>().fontSize = mainTextSize;
            
            sparksScore.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_HEIGHT*0.07f, SCREEN_HEIGHT*0.07f);
            sparksScore.transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_HEIGHT*0.05f, SCREEN_HEIGHT*0.05f);
        }
    }

    void scalePausePanel() {
        //pauseButton.GetComponent<RectTransform>().localPosition = new Vector2(-(SCREEN_WIDTH*0.5f)+(SCREEN_WIDTH*0.06f), (SCREEN_HEIGHT*0.5f)-(SCREEN_HEIGHT*0.9f));
        pauseButton.GetComponent<RectTransform>().localPosition = new Vector2(0-(SCREEN_WIDTH*0.465f), 0+SCREEN_HEIGHT*0.45f);
        pauseButton.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_HEIGHT*0.07f, SCREEN_HEIGHT*0.07f);
        pauseButton.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_HEIGHT*0.05f, SCREEN_HEIGHT*0.05f);

        pausePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT); //background
        pausePanel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        GameObject contButton = pausePanel.transform.GetChild(0).gameObject;
        contButton.GetComponent<RectTransform>().localPosition = new Vector2(0, -(SCREEN_HEIGHT*0.2f));
        contButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonTextSize;

        GameObject menuButton = pausePanel.transform.GetChild(1).gameObject;
        menuButton.GetComponent<RectTransform>().localPosition = new Vector2(0, (SCREEN_HEIGHT*0.2f));
        menuButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonTextSize;
    }

    public void TurnOffInstructions() {
        panelBackground.SetActive(false);
        menuButton.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void EndGame() {
        //playing = false;
        pauseButton.SetActive(false);
        panelBackground.SetActive(true);
        menuButton.SetActive(true);
        timeSlider.gameObject.SetActive(false);
    }

    public void PauseGame() {
        playing = !playing;

        if(playing) {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
        else {
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
    }

    public bool getIfPlayingChooseGame() {
        return playingChooseGame;
    }

    public int getGamesWon() {
        return gameManager.NumberOfGamesWon();
    }

    public void setToWinOnTimeOut() {
        winOnTimeOut = true;
    }

    public bool getIfWinOnTimeOut() {
        return winOnTimeOut;
    }

    IEnumerator WaitAtEndGameWon() {
        playing = false;

        yield return new WaitForSeconds(0.4f);

        EndGame();
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        panelBackground.GetComponent<Image>().color = new Color32(110, 190, 90, 255);
        
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

        gameManager.SetSparksScoreText();

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, wins);
        PlayerPrefs.Save();
    }
    
    IEnumerator WaitAtEndOfGameLost() {
        playing = false;
        
        yield return new WaitForSeconds(0.4f);

        EndGame();
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        panelBackground.GetComponent<Image>().color = colorManager.GetDarkColor();

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
        losePanel.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = loseText;
    }
}
