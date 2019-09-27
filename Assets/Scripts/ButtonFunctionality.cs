using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour
{

    public void RestartGame() {

        SceneManager.LoadScene("SampleScene");
    }

    public void startGame() {
        SceneManager.LoadScene("swipeAway_Game");
    }
}
