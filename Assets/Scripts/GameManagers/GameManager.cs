using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager: holds the list of games and tally of number of games won. It is used to change scenes,
//load the next game and keep track of the previous game for the different play types.
//This will persist on an object through the whole game
public class GameManager : MonoBehaviour
{
    bool playingChooseGame;

    List<string> gamesList;

    int currentGameNum;
    string currentGameName;
    int gamesWon;

    Color bgColor;
    public GameObject bgColorObj;
    Color lightBlue = new Color32(166, 203, 209, 255);
    Color lightPink = new Color32(214, 161, 198, 255);
    Color lightGreen = new Color32(179, 217, 158, 255);
    Color lightOrange = new Color32(231, 192, 148, 255);

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(bgColor[3] == 0) {
            setColor("lightBlue");
        } 
        else {
            setColor(bgColor);
        }
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

        Debug.Log(bgColor);
        
        
        bgColorObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = Color.white;
    }

    public void NextGame() {
        if(playingChooseGame) {
            SceneManager.LoadScene("chooseGame");
        }
        else {
            int nextGame = Random.Range(0, gamesList.Count);    //Pick a random game from list
            if(nextGame == currentGameNum) { NextGame(); } //Choose another game if the current game (just played) is picked
            else {
                currentGameNum = nextGame;
                SceneManager.LoadScene(gamesList[nextGame]);
            }
        }
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("Menu");
        Destroy(this.gameObject, 2);   //Destroy this gameobject as new GameManager will instantiate on menu load

        Debug.Log(GameObject.FindGameObjectsWithTag("GameManager").Length);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("GameManager")) {
            g.GetComponent<GameManager>().bgColor = this.bgColor;
            //g.GetComponent<GameManager>().setColor(this.bgColor);
            Debug.Log("here: " + this.bgColor + ", " + g.GetComponent<GameManager>().bgColor);
            //g.GetComponent<GameManager>().bgColorObj = this.bgColorObj;
            /*if(g.GetComponent<GameManager>().bgColor == null) {
                Debug.Log("here");
                g.GetComponent<GameManager>().bgColorObj = bgColorObj;//new Color32(bgColor[0], bgColor[1], bgColor[2], bgColor[3]);
                g.GetComponent<GameManager>().setColor(this.bgColor);
            }*/
        }
        
        
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

    public void ResetGame() {
        gamesWon = 0;
        currentGameNum = 0;
        currentGameName = "";
    }

    public void setColor(string col) {
        if(col == "lightBlue") { bgColor = lightBlue; }
        else if(col == "lightPink") { bgColor = lightPink; }
        else if(col == "lightGreen") { bgColor = lightGreen; }
        else if(col == "lightOrange") { bgColor = lightOrange; }
        

        GameObject camParent = GameObject.FindWithTag("Cameras");
        camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = bgColor;
    }

    public void setColor(Color col) {
        bgColor = col;

        GameObject camParent = GameObject.FindWithTag("Cameras");
        camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = bgColor;
    }

    public void setColor(GameObject colObj) {
        if(bgColorObj != null) {
            bgColorObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = new Color32(0, 0, 0, 50);
        }

        bgColor = colObj.transform.GetChild(1).gameObject.GetComponent<SVGImage>().color;
        bgColorObj = colObj;

        GameObject camParent = GameObject.FindWithTag("Cameras");
        camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = bgColor;

        colObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = Color.white;
    }

    public Color32 GetColor() {
        return bgColor;
    }
}
