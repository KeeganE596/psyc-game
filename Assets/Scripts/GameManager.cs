using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<string> gamesList;

    int gamesWon;

    // Start is called before the first frame update
    void Start()
    {
        gamesWon = 0;

        gamesList = new List<string>();
        gamesList.Add("swipeAway_Game");
        gamesList.Add("wordAssociation_Game");

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame() {
        SceneManager.LoadScene(gamesList[Random.Range(0, gamesList.Count)]);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame() {
        gamesWon = 0;
        SceneManager.LoadScene(gamesList[Random.Range(0, gamesList.Count)]);
    }

    public int NumberOfGamesWon() {
        return gamesWon;
    }

    public void AddToGamesWon() {
        gamesWon++;
    }
}
