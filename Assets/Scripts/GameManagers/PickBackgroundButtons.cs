using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBackgroundButtons : MonoBehaviour
{
    [SerializeField] private GameObject bgButton_1;
    [SerializeField] private GameObject bgButton_2;

    [SerializeField] private GetBackgroundType setBackgroundScript;

    void Start() {
        HighlightButton(PlayerPrefs.GetString("background"));
    }

    public void PickBackgroundButton(string bg) {
        SaveManager.SaveBackground(bg);
        HighlightButton(bg);
        setBackgroundScript.GetBackground();
    }

    void HighlightButton(string bg) {
        bgButton_1.SetActive(false);
        bgButton_2.SetActive(false);

        switch(bg) {
            case "ocean":
                bgButton_1.SetActive(true);
                break;
            case "mountains":
                bgButton_2.SetActive(true);
                break;
        }
    }
}
