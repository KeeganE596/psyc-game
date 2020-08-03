﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManager
{

    static Color currentBgColor;
    static GameObject bgColorObj;
    static Color lightBlue = new Color32(166, 203, 209, 255);
    static Color lightPink = new Color32(214, 160, 198, 255);
    static Color lightGreen = new Color32(178, 216, 158, 255);
    static Color lightOrange = new Color32(235, 178, 118, 255);
    static Color darkBlue = new Color32(100, 150, 160, 255);
    static Color darkPink = new Color32(180, 120, 165, 255);
    static Color darkGreen = new Color32(140, 175, 120, 255);
    static Color darkOrange = new Color32(210, 150, 100, 255);
    static public GameObject mountainsBg;
    static public GameObject oceansBg;
    static string backgroundType;

    // void Awake() {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    // // Start is called before the first frame update
    // void Start() {
    //     LoadColor();
    //     LoadBackground();

    //     GameObject camParent = GameObject.FindWithTag("Cameras");
    //     //camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = currentBgColor;
    // }

    public static void SetColor(GameObject colObj) {
        if(bgColorObj != null) {
            bgColorObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = new Color32(0, 0, 0, 50);
        }

        currentBgColor = colObj.transform.GetChild(1).gameObject.GetComponent<SVGImage>().color;
        bgColorObj = colObj;

        colObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = Color.white;

        setCameraBackground();
        SaveColor();
    }

    public static void setCameraBackground() {
        GameObject camParent = GameObject.FindWithTag("Cameras");
        //camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = currentBgColor;
    }

    public static Color32 GetColor() {
        return currentBgColor;
    }

    public static Color32 GetDarkColor() {
        if(ColorsMatch(currentBgColor, lightBlue)) {
            return darkBlue;
        }
        else if(ColorsMatch(currentBgColor, lightGreen)) {
            return darkGreen;
        }
        else if(ColorsMatch(currentBgColor, lightPink)) {
            return darkPink;
        }
        else if(ColorsMatch(currentBgColor, lightOrange)) {
            return darkOrange;
        }
        return darkBlue;
    }

    public static bool ColorsMatch(Color32 c1, Color32 c2) {
        return (c1.r == c2.r && c1.g == c2.g && c1.b == c2.b);
    }

    static void LoadColor() {
        if(PlayerPrefs.HasKey("bgColorR") && PlayerPrefs.HasKey("bgColorG") && PlayerPrefs.HasKey("bgColorB")) {
            currentBgColor = new Color(PlayerPrefs.GetFloat("bgColorR"), PlayerPrefs.GetFloat("bgColorG"), PlayerPrefs.GetFloat("bgColorB"), 255);
        }
        else {
            currentBgColor = lightBlue;
        }
    }

    static void SaveColor() {
        PlayerPrefs.SetFloat("bgColorR", currentBgColor.r);
        PlayerPrefs.SetFloat("bgColorG", currentBgColor.g);
        PlayerPrefs.SetFloat("bgColorB", currentBgColor.b);
        PlayerPrefs.Save();
    }

    public static void SetBackground(string type) {
        setCameraBackground();
        switch (type){
            case "mountains":
                //mountainsBg.SetActive(true);
                //oceansBg.SetActive(false);
                backgroundType = "mountains";
                break;
            case "ocean":
                //mountainsBg.SetActive(false);
                //oceansBg.SetActive(true);
                backgroundType = "ocean";
                break;
        }
        PlayerPrefs.SetString("backgroundType", backgroundType);
        PlayerPrefs.Save();
    }

    public static void LoadBackground() {
        if(PlayerPrefs.HasKey("backgroundType")) {
            SetBackground(PlayerPrefs.GetString("backgroundType"));
        }
        else {
            SetBackground("ocean");
        }
    }

    public static void HideBackground() {
        mountainsBg.SetActive(false);
        oceansBg.SetActive(false);
    }
    
}
