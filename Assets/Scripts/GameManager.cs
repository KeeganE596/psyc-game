using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<string> gamesList;

    // Start is called before the first frame update
    void Start()
    {
        gamesList = new List<string>();
        gamesList.Add("swipeAway_Game");
        gamesList.Add("wordAssociation");

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame() {
        //Application.LoadLevel(gamesList[0]); 
        SceneManager.LoadScene(gamesList[0]);
    }
}
