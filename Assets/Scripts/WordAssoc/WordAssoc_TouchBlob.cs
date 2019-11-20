using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//WordAssoc_TouchBlob: Attached to an object which follows the users finger/mouse when holding a click
//If it collides with a word and the user is no longer holding then it attaches a line from center
//to the word, it calls the appropriate method depending if it is a good/bad word
public class WordAssoc_TouchBlob : MonoBehaviour
{
    int wordsCaught;
    LevelManager levelManager;

    public int winAmount = 6;

    List<GameObject> caughtWords;

    public GameObject sparkParticle;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Start() {
        wordsCaught = 0;
        caughtWords = new List<GameObject>();
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.CompareTag("Word") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            hitWord(col.gameObject);
        }
        else if(col.gameObject.CompareTag("WordParent") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            hitWord(col.gameObject.transform.GetChild(0).gameObject);
        }
        else if (col.gameObject.CompareTag("BadWord") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            hitBadWord(col.gameObject);
        }
        else if (col.gameObject.CompareTag("BadWordParent") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            hitBadWord(col.gameObject.transform.GetChild(0).gameObject);
        }
    }

    void hitWord(GameObject word) {
        Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);

        word.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(255, 255, 255, 255);   //make word white
        
        //Add line from word to center
        word.AddComponent<LineRenderer>();
        LineRenderer line = word.GetComponent<LineRenderer>();
        line.startWidth = (0.05f);
        line.endWidth = (0.05f);
        line.SetPosition(1, new Vector2(0, 0));
        line.SetPosition(0, word.transform.position);
        line.gameObject.layer = 11;

        wordsCaught++;
        caughtWords.Add(word);
        word.GetComponent<Collider2D>().enabled = false;

        if(wordsCaught >= winAmount) {
            wordsCaught = 0;
            levelManager.GameWon();
        }
    }

    void hitBadWord(GameObject word) {
        word.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(230, 52, 39, 255); //makes word red

        //remove all line connections
        foreach(GameObject w in caughtWords) {
            Destroy(w.GetComponent<LineRenderer>());
            w.GetComponentInChildren<Collider2D>().enabled = true;
            w.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(101, 101, 101, 255);
        }

        word.GetComponent<Collider2D>().enabled = false;
        wordsCaught = 0;
    }
}
