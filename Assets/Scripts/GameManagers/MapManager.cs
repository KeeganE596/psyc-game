using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameButtons;

    private void Start() {
        CheckLevelUnlock();
    }

    private void CheckLevelUnlock() {
        int currentLevelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
        for(int i=0; i<_gameButtons.Count; i++) {
            GameObject b = _gameButtons[i];
            if(i > currentLevelUnlocked) { //For locked levels
                b.GetComponent<Button>().enabled = false;
                b.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    public void ChooseLevel(int lvl) {
        GameManagerStatic.PickGame("swipeAway", lvl);
    }

    public void ChooseIntro() {
        SceneLoadManager.ToIntro();
    }

    public void ToMenu() {
        SceneLoadManager.ToMenu();
    }
}
