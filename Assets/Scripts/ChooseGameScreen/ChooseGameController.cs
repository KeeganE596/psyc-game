using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGameController : MonoBehaviour
{
    //Referencing Game Manager and Color Manager
    GameManager gameManager;
    void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    public void BlobClicked(GameObject blob) {
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
}
