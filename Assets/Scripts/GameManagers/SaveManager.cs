using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void CheckForPlayerPrefs() {
        if(!PlayerPrefs.HasKey("sparxScore")) {
            PlayerPrefs.SetInt("sparxScore", 0);
        }
        if(!PlayerPrefs.HasKey("LevelUnlocked")) {
            PlayerPrefs.SetInt("LevelUnlocked", 0);
        }
        if(!PlayerPrefs.HasKey("background")) {
            PlayerPrefs.SetInt("background", 0);
        }
        if(!PlayerPrefs.HasKey("playerCharacter")) {
            PlayerPrefs.SetInt("playerCharacter", 0);
        }

        PlayerPrefs.Save();
    }

    public static void ResetAllPlayerPrefs() {
        PlayerPrefs.SetInt("sparxScore", 0);
        PlayerPrefs.SetInt("LevelUnlocked", 0);
        PlayerPrefs.SetInt("background", 0);

        PlayerPrefs.Save();
    }

    public static void SaveLevelUnlocked(int lvl)
    {
        lvl = (lvl + (20 * GameManagerStatic.GetCurrentLevelSetNumber()));
        Debug.Log(lvl + " unlocking");
        PlayerPrefs.SetInt("LevelUnlocked", lvl);

        PlayerPrefs.Save();
    }

    public static void AddToSparxScore() {
        int score = PlayerPrefs.GetInt("sparxScore");
        PlayerPrefs.SetInt("sparxScore", (score + 1));

        PlayerPrefs.Save();
    }

    public static void SaveCharacter(int num) {
        PlayerPrefs.SetInt("playerCharacter", num);
        
        PlayerPrefs.Save();
    }

    // public static void SaveBackground(string bg) {
    //     PlayerPrefs.SetString("background", bg);
        
    //     PlayerPrefs.Save();
    // }

    public static void SaveBackground(int bgNum) {
        PlayerPrefs.SetInt("background", bgNum);
        
        PlayerPrefs.Save();
    }

    public static void ResetLevelunlock() {
        PlayerPrefs.SetInt("LevelUnlocked", 45);

        PlayerPrefs.Save();
    }
}
