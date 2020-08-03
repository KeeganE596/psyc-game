using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{

    public static void LoadGame(string name) {
        SceneManager.LoadScene(name + "_Game");
    }

    public static void LoadSameGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ToChooseGame() {
        SceneManager.LoadScene("chooseGame");
    }

    public static void ToMap() {
        SceneManager.LoadScene("Map");
    }

    public static void ToIntro() {
        SceneManager.LoadScene("Intro");
    }

    public static void ToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public static void LoadRandomGame() {
        int rand = Random.Range(0, 4);
        switch (rand){
            case 0:
                LoadGame("swipeAway");
                break;
            case 1:
                LoadGame("wordAssociation");
                break;
            case 2:
                LoadGame("breathing");
                break;
            case 3:
                LoadGame("inspection");
                break;
            default:
                LoadGame("swipeAway");
                break;
        }
    }
}
