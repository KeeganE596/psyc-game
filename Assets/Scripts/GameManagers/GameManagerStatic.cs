using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManagerStatic
{
    private static bool playingRandomGame = false;
    private static string currentGameName = "";
    private static int _currentLevel = 0;
    private static int _currentLevelSet = 0;

    private static LevelSettings _currentLevelSettings;

    private static LevelSettings[] _levelSettingsArray;


    public static void ChooseGameType(string type) 
    {
        if(type == "map")
        {
            SceneLoadManager.ToMap();
        }
        else if(type == "random")
        {
            playingRandomGame = true;
            SceneLoadManager.LoadRandomGame();
        }
        else if(type == "choose") 
        {
            SceneLoadManager.ToChooseGame();
        }
    }

    public static void NextGame() 
    {
        int lvl = _currentLevel + 1;
        PickGame(lvl);
    }

    public static void SetLevels(LevelSettings[] ls)
    {
        _levelSettingsArray = ls;
    }

    public static void PickGame(LevelSettings lm)
    {
        //_levelSettings = lm;
        currentGameName = "Main";
        _currentLevel = lm.GetLevelNumber();
        SceneLoadManager.LoadGame("Main");
    }

    public static void PickGame(int lvl) 
    {
        _currentLevel = lvl;
        _currentLevelSettings = _levelSettingsArray[lvl];
        SceneLoadManager.LoadGame("Main");
    }

    public static void PickGame(int lvl, int levelSet)
    {
        _currentLevel = lvl;
        _currentLevelSet = levelSet;
        _currentLevelSettings = _levelSettingsArray[lvl];
        SceneLoadManager.LoadGame("Main");
    }

    public static void PickGame(string gameName) 
    {
        currentGameName = gameName;
        SceneLoadManager.LoadGame(gameName);
    }


    public static void PlaySameGame()
    {
        SceneLoadManager.LoadSameGame();
    }

    public static bool GetPlayingRandomGame() 
    {
        return playingRandomGame;
    }

    public static int GetCurrentLevelNumber() 
    {
        return _currentLevel;
    }

    public static int GetCurrentLevelSetNumber()
    {
        return _currentLevelSet;
    }

    public static int GetCurrentRealLevelNumber()
    {
        return (_currentLevel + (20 * _currentLevelSet));
    }

    public static LevelSettings GetCurrentLevelSettings()
    {
        return _currentLevelSettings;
    }
}
