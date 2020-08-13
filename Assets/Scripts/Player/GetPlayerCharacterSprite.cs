using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerCharacterSprite : MonoBehaviour
{
    void Start() {
        GetCharacter();
    }

    void GetCharacter() {
        int charNum = PlayerPrefs.GetInt("playerCharacter");
        this.transform.GetChild(charNum).gameObject.SetActive(true);
    }
}
