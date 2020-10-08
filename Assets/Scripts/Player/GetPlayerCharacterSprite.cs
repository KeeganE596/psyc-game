using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerCharacterSprite : MonoBehaviour
{
    void Start() {
        GetCharacter();
    }

    void GetCharacter() {
        int charNum = GameManagerStatic.GetCurrentLevelSetNumber();

        if (charNum == 0)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(charNum - 1).gameObject.SetActive(true);

        //if (GameManagerStatic.GetCurrentLevelNumber() < 40)
        //    charNum = 0;
        //if (GameManagerStatic.GetCurrentLevelNumber() >= 40 && GameManagerStatic.GetCurrentLevelNumber() < 60)
        //    charNum = 1;
        //if (GameManagerStatic.GetCurrentLevelNumber() >= 60 && GameManagerStatic.GetCurrentLevelNumber() < 80)
        //    charNum = 2;
        //if (GameManagerStatic.GetCurrentLevelNumber() >= 80 && GameManagerStatic.GetCurrentLevelNumber() < 100)
        //    charNum = 3;
    }
}
