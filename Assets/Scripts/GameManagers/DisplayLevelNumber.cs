using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevelNumber : MonoBehaviour
{
    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "Lvl " + (GameManagerStatic.GetCurrentLevelNumber() + 1);
    }
}
