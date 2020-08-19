using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBackgroundButtons : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgroundButtons;
    [SerializeField] private GetBackgroundType GetBackgroundType;

    void Start() {
        HighlightButton(PlayerPrefs.GetInt("background"));
    }

    public void PickBackgroundButton(int bgNum) {
        SaveManager.SaveBackground(bgNum);
        HighlightButton(bgNum);
        GetBackgroundType.GetBackground();
    }

    void HighlightButton(int bgNum) {
        for(int i=0; i<backgroundButtons.Count; i++) {
            if(bgNum == i) {
                backgroundButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                backgroundButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
