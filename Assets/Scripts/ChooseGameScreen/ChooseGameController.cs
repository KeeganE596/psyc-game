using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseGameController : MonoBehaviour
{
    //Referencing Game Manager and Color Manager
    GameManager gameManager;

    void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start() {
        if(gameManager.GetSparksScore() < 30) {
            GameObject inspectGame = GameObject.Find("gameButton_Inspect");
            inspectGame.GetComponentInChildren<TextMeshPro>().text = "30 x";
            inspectGame.GetComponent<Collider2D>().enabled = false;
            //GameObject.FindWithTag("Spark").SetActive(true);
        }
    }

    public void BlobClicked(GameObject blob) {
        if(blob.name == "gameButton_Inspect" && gameManager.GetSparksScore() < 30) {
            return;
        }
        UnclickAll();
        Animator anim = blob.GetComponent<Animator>();
        anim.SetBool("clicked", true);
    }

    public void UnclickAll() {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("ChooseButton")) {
            g.GetComponent<Animator>().SetBool("clicked", false);
        }
    }

    public void PlayGame(string gameName) {
        gameManager.PickGame(gameName);
    }

    public void ToMainMenu() {
        gameManager.ToMainMenu();
    }
}
