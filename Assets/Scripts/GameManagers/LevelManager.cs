using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//LevelManager: each level has a canvas with this levelmanager script
//It is a singleton so can be referenced with LevelManager.Instance
//This controls the text panels before and after games and the pasue menu
//It controls the game time slider and whether the game is playing or not
//In game scripts can reference this to start/end games and get information about the game type
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set;}
    
    Slider timeSlider;
    private bool usingTimer = true;
    public float maxTime = 15f;
    //public float maxtime { get { return _maxTime; } }
    float timeRemaining;
    bool playing = false;
    // [SerializeField] private bool usingPoints = false;
    bool winOnTimeOut = false;

    //Referencing canvas objects
    [SerializeField] private GameObject inGameUIObjects;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject panelBackground;
    [SerializeField] private GameObject pausePanel;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Debug.Log("Destroying LevelManager duplicate");
            Destroy(gameObject); //Don't allow two LevelManagers
        }

        timeSlider = inGameUIObjects.GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start() {
        timeRemaining = maxTime;

        panelBackground.GetComponent<Image>().color = ColorManager.GetColor();
        ColorManager.setCameraBackground();
        ColorManager.LoadBackground();
        
        inGameUIObjects.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        instructionsPanel.SetActive(false);
        StartGame(); 
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
        Time.timeScale = 1;
        TurnOffInstructions();
        inGameUIObjects.SetActive(true);
        playing = true;
    }

    float CalculateSliderValue() {
        return timeRemaining/maxTime;
    }

    public void GameWon() {
        StartCoroutine("WaitAtEndGameWon");
    }

    public void GameLost() {
        StartCoroutine("WaitAtEndOfGameLost");
    }

    public void NextLevel() {
        GameManagerStatic.NextGame();
    }

    public void RestartGame() {
        GameManagerStatic.PlaySameGame();
    }

    public void ToMainMenu() {
        PauseGame(false);
        SceneLoadManager.ToMenu();
    }

    public void ToMap() {
        PauseGame(false);
        SceneLoadManager.ToMap();
    }

    public bool GetIfGameIsPlaying() {
        return playing;
    }

    public void TurnOffInstructions() {
        panelBackground.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void EndGame() {
        Time.timeScale = 0;
        inGameUIObjects.SetActive(false);
        panelBackground.SetActive(true);
    }

    public void PauseGame(bool paused) {
        if(paused) {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SetToWinOnTimeOut() {
        winOnTimeOut = true;
    }

    public bool GetIfWinOnTimeOut() {
        return winOnTimeOut;
    }

    IEnumerator WaitAtEndGameWon() {
        playing = false;
        SaveManager.AddToSparxScore();

        yield return new WaitForSeconds(0.4f);

        EndGame();
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        //panelBackground.GetComponent<Image>().color = new Color32(110, 190, 90, 255);

        int lvlunlocked = PlayerPrefs.GetInt("LevelUnlocked");
        if((GameManagerStatic.GetCurrentRealLevelNumber()) == lvlunlocked) 
        {
            SaveManager.SaveLevelUnlocked(GameManagerStatic.GetCurrentLevelNumber() + 1);
        }
    }
    
    IEnumerator WaitAtEndOfGameLost() {
        playing = false;
        
        yield return new WaitForSeconds(0.4f);

        EndGame();
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        //panelBackground.GetComponent<Image>().color = ColorManager.GetDarkColor();
    }

    public void StartTimer() {
        inGameUIObjects.SetActive(true);
        usingTimer = true;
    }
}
