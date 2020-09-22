using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerCharacterSprite : MonoBehaviour
{
    void Start() {
        GetCharacter();
    }

    void GetCharacter() {
        int charNum = 0;
        
        if (GameManagerStatic.GetCurrentLevelNumber() < 40)
            charNum = 0;
        if (GameManagerStatic.GetCurrentLevelNumber() >= 40 && GameManagerStatic.GetCurrentLevelNumber() < 60)
            charNum = 1;
        if (GameManagerStatic.GetCurrentLevelNumber() >= 60 && GameManagerStatic.GetCurrentLevelNumber() < 80)
            charNum = 2;
        if (GameManagerStatic.GetCurrentLevelNumber() >= 80 && GameManagerStatic.GetCurrentLevelNumber() < 100)
            charNum = 3;

        transform.GetChild(charNum).gameObject.SetActive(true);
    }
}
