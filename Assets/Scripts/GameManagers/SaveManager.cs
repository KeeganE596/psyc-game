using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void CheckForPlayerPrefs() {
        if(!PlayerPrefs.HasKey("sparksScore")) {
            PlayerPrefs.SetInt("sparksScore", 0);
        }
        if(!PlayerPrefs.HasKey("LevelUnlocked")) {
            PlayerPrefs.SetInt("LevelUnlocked", 0);
        }
        if(!PlayerPrefs.HasKey("backgroundType")) {
            PlayerPrefs.SetString("backgroundType", "ocean");
        }

        PlayerPrefs.Save();
    }

    public static void ResetAllPlayerPrefs() {
        PlayerPrefs.SetInt("sparksScore", 0);
        PlayerPrefs.SetInt("LevelUnlocked", 0);
        PlayerPrefs.SetString("backgroundType", "ocean");

        PlayerPrefs.Save();
    }

    public static void SaveLevelUnlocked(int lvl) {
        PlayerPrefs.SetInt("LevelUnlocked", lvl);

        PlayerPrefs.Save();
    }
}
