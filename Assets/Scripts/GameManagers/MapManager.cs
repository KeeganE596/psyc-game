using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject button_0;
    [SerializeField] private GameObject button_1;
    [SerializeField] private GameObject button_2;
    [SerializeField] private GameObject button_3;
    [SerializeField] private GameObject button_4;
    [SerializeField] private GameObject button_5;
    [SerializeField] private GameObject button_6;
    [SerializeField] private GameObject button_7;
    [SerializeField] private GameObject button_8;
    [SerializeField] private GameObject button_9;
    [SerializeField] private GameObject button_10;

    void Awake() {
    }

    // Start is called before the first frame update
    void Start() {
        CheckLevelUnlock();
    }

    public void ChooseLevel(int lvl) {
        GameManagerStatic.PickGame("swipeAway", lvl);
    }

    public void ChooseIntro() {
        SceneLoadManager.ToIntro();
    }

    void CheckLevelUnlock() {
        int currentLevelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("LevelButton");
        for(int i=0; i<buttons.Length; i++) {
            GameObject b = GetButton(i);
            b.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            // buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            if(i > currentLevelUnlocked) { //For locked levels
                b.GetComponent<Button>().enabled = false;
                b.transform.GetChild(0).GetComponent<Image>().enabled = false;
                // buttons[i].GetComponent<Button>().enabled = false;
                // buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    GameObject GetButton(int num) {
        switch(num) {
            case 0:
                return button_0;
            case 1:
                return button_1;
            case 2:
                return button_2;
            case 3:
                return button_3;
            case 4:
                return button_4;
            case 5:
                return button_5;
            case 6:
                return button_6;
            case 7:
                return button_7;
            case 8:
                return button_8;
            case 9:
                return button_9;
            case 10:
                return button_10;
            default:
                return button_0;
        }
    }

    public void ToMenu() {
        SceneLoadManager.ToMenu();
    }
}
