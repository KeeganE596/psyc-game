using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_events : MonoBehaviour
{
    public GameObject PlayText;
    public GameObject StartGameButton;
    public GameObject settingsPanel;

    public void showPlayTextEvent(){
        PlayText.SetActive(true);
        StartGameButton.SetActive(true);

    }


}
