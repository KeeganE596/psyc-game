using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManagerStatic
{
    private static bool playingRandomGame = false;
    private static string currentGameName = "";
    private static int currentLevel = 0;

    public static void ChooseGameType(string type) { //Previously StartGame
        if(type == "map") {
            SceneLoadManager.ToMap();
        }
        else if(type == "random") {
            playingRandomGame = true;
            SceneLoadManager.LoadRandomGame();
        }
        else if(type == "choose") {
            SceneLoadManager.ToChooseGame();
        }
    }

    public static void NextGame() {
        int lvl = currentLevel + 1;
        PickGame(currentGameName, lvl);
    }

    public static void PickGame(string gameName, int lvl) {
        currentGameName = gameName;
        currentLevel = lvl;
        SceneLoadManager.LoadGame(gameName);
    }

    public static void PickGame(string gameName) {
        currentGameName = gameName;
        SceneLoadManager.LoadGame(gameName);
    }


    public static void PlaySameGame() {
        SceneLoadManager.LoadSameGame();
    }

    public static bool GetPlayingRandomGame() {
        return playingRandomGame;
    }

    public static int GetCurrentLevelNumber() {
        return currentLevel;
    }
}
