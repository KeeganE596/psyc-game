using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBackgroundType : MonoBehaviour
{
    void Start() {
        GetBackground();
    }

    public void GetBackground() {
        string bg = "ocean";
        
        if(PlayerPrefs.HasKey("background")) {
            bg = PlayerPrefs.GetString("background");
        }

        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);

        switch (bg) {
            case "ocean":
                this.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "mountains":
                this.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
        
    }
}
