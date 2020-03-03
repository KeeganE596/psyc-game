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

    int currentGameNum;
    string currentGameName;
    
    public static int gamesWon;

    public GameObject CMPrefab;
    ColorManager colorManager;

    int sparksScore;

    public static string swipeType; //For testing declare spawn type

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(GameObject.FindGameObjectsWithTag("ColorManager").Length < 1) {
            Instantiate(CMPrefab);
        }
        colorManager = GameObject.FindWithTag("ColorManager").GetComponent<ColorManager>();
    }
    // Start is called before the first frame update
    void Start() {
        playingChooseGame = false;

        gamesWon = 0;
        currentGameNum = 0;
        currentGameName = "";

        gamesList = new List<string>();
        gamesList.Add("swipeAway_Game");
        gamesList.Add("wordAssociation_Game");
        gamesList.Add("breathing_Game");
        gamesList.Add("inspection_Game");

        colorManager.setCameraBackground();

        swipeType = "default";

        LoadSparksScore();
        SetSparksScoreText();
    }

    public void NextGame() {
        colorManager.HideBackground();
        if(playingChooseGame) {
            SceneManager.LoadScene("chooseGame");
        }
        else {
            SceneManager.LoadScene("swipeAway_Game");   //FOR TESTING set to any game to play just this in random, comment out all below
            // int nextGame = Random.Range(0, gamesList.Count);    //Pick a random game from list
            // if(nextGame == currentGameNum) { NextGame(); } //Choose another game if the current game (just played) is picked
            // else {
            //     currentGameNum = nextGame;
            //     SceneManager.LoadScene(gamesList[nextGame]);
            // }
        }
    }

    public void ToMainMenu() {
        colorManager.SetBackground("ocean");
        SceneManager.LoadScene("Menu");
        DestroyImmediate(this.gameObject, true);   //Destroy this gameobject as new GameManager will instantiate on menu load
    }
    
    public void ToLeaderboard() {
        SceneManager.LoadScene("Leaderboard");
    }

    public void StartGame(string type) {
        colorManager.HideBackground();
        gamesWon = 0;

        if(type == "random") {
            NextGame();
        }
        if(type == "choose") {
            playingChooseGame = true;
            SceneManager.LoadScene("chooseGame");
        }
    }

    public void PickGame(string gameName) {
        currentGameName = gameName;
        SceneManager.LoadScene(gameName + "_Game");
    }

    public void PlaySameGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int NumberOfGamesWon() {
        return gamesWon;
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
        currentGameNum = 0;
        currentGameName = "";
    }

    public void PlayAgain() {
        //gamesWon = 0;
        currentGameNum = 0;
        currentGameName = "";
    }

    public void SetColor(GameObject colObj) {
        colorManager.SetColor(colObj);
    }

    public void SetBackground(string type) {
        colorManager.SetBackground(type);
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
        GameObject.FindWithTag("SparksScore").GetComponent<TextMeshProUGUI>().text = sparksScore.ToString();
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
        SceneManager.LoadScene("swipeAway_Game");
    }
}
