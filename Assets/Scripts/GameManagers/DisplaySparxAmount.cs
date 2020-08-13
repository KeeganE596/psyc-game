using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySparxAmount : MonoBehaviour
{
    void OnEnable() {
        if(PlayerPrefs.HasKey("sparxScore")) {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetInt("sparxScore").ToString();
        }
    }
}
