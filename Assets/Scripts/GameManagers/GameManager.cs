using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//GameManager: holds the list of games and tally of number of games won. It is used to change scenes,
//load the next game and keep track of the previous game for the different play types.
//This will persist on an object through the whole game
public class GameManager : MonoBehaviour
{
    bool playingChooseGame;

    List<string> gamesList;

    //int currentGameNum;
    string currentGameName;
    int currentLevel;
    
    public static int gamesWon;

    public GameObject CMPrefab;
    //ColorManager colorManager;

    int sparksScore;

    public static string swipeType; //For testing declare spawn type

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        // if(GameObject.FindGameObjectsWithTag("ColorManager").Length < 1) {
        //     Instantiate(CMPrefab);
        // }
        // colorManager = GameObject.FindWithTag("ColorManager").GetComponent<ColorManager>();

        SaveManager.CheckForPlayerPrefs();
    }
    // Start is called before the first frame update
    void Start() {
        playingChooseGame = false;

        gamesWon = 0;
        //currentGameNum = 0;
        currentGameName = "";

        gamesList = new List<string>();
        gamesList.Add("swipeAway_Game");
        gamesList.Add("wordAssociation_Game");
        gamesList.Add("breathing_Game");
        gamesList.Add("inspection_Game");

        ColorManager.setCameraBackground();
        ColorManager.LoadBackground();

        swipeType = "default";

        LoadSparksScore();
        SetSparksScoreText();
    }

    public void RandomGame() {
        int nextGame = Random.Range(0, gamesList.Count);
        SceneLoadManager.LoadGame(gamesList[nextGame]);
    }

    public void ToMainMenu() {
        ColorManager.LoadBackground();
        SceneLoadManager.ToMenu();
        DestroyImmediate(this.gameObject, true);   //Destroy this gameobject as new GameManager will instantiate on menu load
    }

    public void StartGame(string type) {
        //colorManager.HideBackground();
        gamesWon = 0;

        if(type == "random") {
            RandomGame();
            return;
        }
        else if(type == "choose") {
            playingChooseGame = true;
            SceneLoadManager.ToChooseGame();
            //SceneManager.LoadScene("chooseGame");
            return;
        }
        else if(type == "map") {
            SceneLoadManager.ToMap();
            //SceneManager.LoadScene("Map");
            return;
        }
    }

    public void NextGame() {
        PickGame(currentGameName, (currentLevel++));
    }

    public void PickGame(string gameName, int lvl) {
        currentGameName = gameName;
        currentLevel = lvl;
        SceneLoadManager.LoadGame(gameName);
        //SceneManager.LoadScene(gameName + "_Game");
    }

    public void PickGame(string gameName) {
        currentGameName = gameName;
        SceneLoadManager.LoadGame(gameName);
        //SceneManager.LoadScene(gameName + "_Game");
    }


    public void PlaySameGame() {
        SceneLoadManager.LoadSameGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int NumberOfGamesWon() {
        return currentLevel;
    }

    public void AddToGamesWon() {
        gamesWon++;
        sparksScore++;
        SaveSparksScore();
    }

    public bool isPlayingChooseGame() {
        return playingChooseGame;
    }

    public void ResetGame() {
        //gamesWon = 0;
        //currentGameNum = 0;
        currentGameName = "";
    }

    public void PlayAgain() {
        //gamesWon = 0;
        //currentGameNum = 0;
        currentGameName = "";
    }

    public void SetColor(GameObject colObj) {
        ColorManager.SetColor(colObj);
    }

    public void SetBackground(string type) {
        ColorManager.SetBackground(type);
    }

    void LoadSparksScore() {
        if(PlayerPrefs.HasKey("sparksScore")) {
            sparksScore = PlayerPrefs.GetInt("sparksScore");
        }
        else {
            sparksScore = 0;
        }
    }

    public void SaveSparksScore() {
        PlayerPrefs.SetInt("sparksScore", sparksScore);
        PlayerPrefs.Save();
    }

    public int GetSparksScore() {
        return sparksScore;
    }

    public void SetSparksScoreText() {
        if(GameObject.FindGameObjectsWithTag("SparksScore").Length > 0) {
            GameObject.FindWithTag("SparksScore").GetComponent<TextMeshProUGUI>().text = sparksScore.ToString();
        }
    }

    public void UnlockAll() {
        sparksScore = 100;
        SaveSparksScore();
        SetSparksScoreText();
    }

    public void ResetScore() {
        sparksScore = 0;
        SaveSparksScore();
        SetSparksScoreText();
    }

    public void setSwipeType(string type) {
        playingChooseGame = false;
        swipeType = type;
        SceneLoadManager.LoadGame("swipeAway");
        //SceneManager.LoadScene("swipeAway_Game");
    }

    public int GetCurrentLevelNumber() {
        return currentLevel;
    }
}
