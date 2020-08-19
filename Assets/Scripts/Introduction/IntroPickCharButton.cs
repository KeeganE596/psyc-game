using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPickCharButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterButtons;
    // [SerializeField] private GameObject charButton_1;
    // [SerializeField] private GameObject charButton_2;
    // [SerializeField] private GameObject charButton_3;
    // [SerializeField] private GameObject charButton_4;

    void Start() {
        HighlightButton(PlayerPrefs.GetInt("playerCharacter"));
    }

    public void PickCharacterButton(int num) {
        SaveManager.SaveCharacter(num);
        HighlightButton(num);
    }

    void HighlightButton(int num) {
        // charButton_1.SetActive(false);
        // charButton_2.SetActive(false);
        // charButton_3.SetActive(false);
        // charButton_4.SetActive(false);

        // switch(num) {
        //     case 0:
        //         charButton_1.SetActive(true);
        //         break;
        //     case 1:
        //         charButton_2.SetActive(true);
        //         break;
        //     case 2:
        //         charButton_3.SetActive(true);
        //         break;
        //     case 3:
        //         charButton_4.SetActive(true);
        //         break;
        // }

        for(int i=0; i<characterButtons.Count; i++) {
            if(num == i) {
                characterButtons[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                characterButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
