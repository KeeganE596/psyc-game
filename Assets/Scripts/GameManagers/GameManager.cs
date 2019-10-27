using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float SCREEN_WIDTH = Screen.width;
    private float SCREEN_HEIGHT = Screen.height;

    List<string> gamesList;

    int currentGame;
    int gamesWon;

    // Start is called before the first frame update
    void Start() {
        gamesWon = 0;
        currentGame = 0;

        gamesList = new List<string>();
        gamesList.Add("swipeAway_Game");
        gamesList.Add("wordAssociation_Game");
        gamesList.Add("breathing_Game");

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
    }

    public void NextGame() {
        int nextGame = Random.Range(0, gamesList.Count);
        if(nextGame == currentGame) { NextGame(); } //Choose another game if the current game (just played) is picked
        else {
            currentGame = nextGame;
            SceneManager.LoadScene(gamesList[nextGame]);
        }
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame() {
        gamesWon = 0;
        //currentGame = Random.Range(0, gamesList.Count);
        //SceneManager.LoadScene("description");
        NextGame();
    }

    public int NumberOfGamesWon() {
        return gamesWon;
    }

    public void AddToGamesWon() {
        gamesWon++;
    }

    public float getWidth() {
        return SCREEN_WIDTH;
    }

    public float getHeight() {
        return SCREEN_HEIGHT;
    }
}
