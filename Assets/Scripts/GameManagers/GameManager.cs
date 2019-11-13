using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool playingChooseGame;

    List<string> gamesList;

    int currentGameNum;
    string currentGameName;
    int gamesWon;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
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
    }

    public void NextGame() {
        if(playingChooseGame) {
            SceneManager.LoadScene("chooseGame");
        }
        else {
            int nextGame = Random.Range(0, gamesList.Count);
            if(nextGame == currentGameNum) { NextGame(); } //Choose another game if the current game (just played) is picked
            else {
             currentGameNum = nextGame;
                SceneManager.LoadScene(gamesList[nextGame]);
            }
        }
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("Menu");
        Destroy(this.gameObject);
    }

    public void StartGame(string type) {
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
        SceneManager.LoadScene(currentGameName + "_Game");
    }

    public int NumberOfGamesWon() {
        return gamesWon;
    }

    public void AddToGamesWon() {
        gamesWon++;
    }

    public bool isPlayingChooseGame() {
        return playingChooseGame;
    }
}
