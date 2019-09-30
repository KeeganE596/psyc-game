using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordAssoc_TouchBlob : MonoBehaviour
{

    int wordsCaught;
    public GameObject canvas;
    LevelManager levelManager;

    public int winAmount = 6;

    List<GameObject> caughtWords;

    void Start() {
        wordsCaught = 0;
        levelManager = canvas.GetComponent<LevelManager>();

        caughtWords = new List<GameObject>();
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.CompareTag("Word") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            col.enabled = false;
            hitWord(col.gameObject);
            wordsCaught++;
            caughtWords.Add(col.gameObject);

            if(wordsCaught > winAmount) {
                levelManager.GameWon();
            }
        }
        if (col.gameObject.CompareTag("BadWord") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            col.enabled = false;
            hitBadWord(col.gameObject);
            wordsCaught = 0;

        }
    }

    void hitWord(GameObject word) {
        word.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(255, 255, 255, 255);
        
        word.AddComponent<LineRenderer>();
        LineRenderer line = word.GetComponent<LineRenderer>();
        line.startWidth = (0.05f);
        line.endWidth = (0.05f);
        line.SetPosition(1, new Vector2(0, 0));
        line.SetPosition(0, word.transform.position);
        line.gameObject.layer = 11;
    }

    void hitBadWord(GameObject word) {
        word.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(230, 52, 39, 255);

        foreach(GameObject w in caughtWords) {
            Destroy(w.GetComponent<LineRenderer>());
        }

    }
}
