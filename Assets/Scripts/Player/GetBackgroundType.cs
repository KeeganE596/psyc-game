using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBackgroundType : MonoBehaviour
{
    void Start() {
        GetBackground();
    }

    public void GetBackground() {
        int bg = 0;
        
        if(PlayerPrefs.HasKey("background"))
            bg = PlayerPrefs.GetInt("background");

        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
        transform.GetChild(bg).gameObject.SetActive(true);
    }
}
