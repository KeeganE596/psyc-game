using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevelunlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "lvl unlck: " + PlayerPrefs.GetInt("LevelUnlocked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
