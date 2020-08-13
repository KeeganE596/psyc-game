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
            PlayerPrefs.SetString("background", "ocean");
        }
        if(!PlayerPrefs.HasKey("playerCharacter")) {
            PlayerPrefs.SetInt("playerCharacter", 0);
        }

        PlayerPrefs.Save();
    }

    public static void ResetAllPlayerPrefs() {
        PlayerPrefs.SetInt("sparxScore", 0);
        PlayerPrefs.SetInt("LevelUnlocked", 0);
        PlayerPrefs.SetString("background", "ocean");

        PlayerPrefs.Save();
    }

    public static void SaveLevelUnlocked(int lvl) {
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

    public static void SaveBackground(string bg) {
        PlayerPrefs.SetString("background", bg);
        
        PlayerPrefs.Save();
    }
}
