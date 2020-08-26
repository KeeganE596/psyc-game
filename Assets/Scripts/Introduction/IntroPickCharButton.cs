using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPickCharButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterButtons;

    void Start() {
        SaveManager.SaveCharacter(0);
        HighlightButton(PlayerPrefs.GetInt("playerCharacter"));
    }

    public void PickCharacterButton(int num) {
        //SaveManager.SaveCharacter(num);
        HighlightButton(num);
    }

    void HighlightButton(int num) {
        for(int i=0; i<characterButtons.Count; i++) {
            if(num == i) {
                characterButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                characterButtons[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else {
                characterButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                characterButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
