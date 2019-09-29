using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordSpawner : MonoBehaviour
{
    List<string> words;

    public GameObject canvas;
    LevelManager levelManager;
    public GameObject word;
    GameObject obj;
    bool playing;

    float timer;

    // Start is called before the first frame update
    void Start() {
        words = new List<string>();

        words.Add("courage");   words.Add("caring");   words.Add("excitement");
        words.Add("encourage");   words.Add("fun");   words.Add("humour");
        words.Add("gratitude");   words.Add("patience");   words.Add("persistence");

        timer = 0.5f;

        levelManager = canvas.GetComponent<LevelManager>();
        playing = levelManager.isPlaying();
    }

    // Update is called once per frame
    void Update() {
        //Check player sin't on instruction screen
        if(playing) {
            timer += Time.deltaTime;

            if(timer > 1 && words.Count > 0) {
                //Get rendom position to spawn word at
                Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));

                //check position isnt too close to middle
                if((spawnPos.x < -0.75 || spawnPos.x > 0.75) && (spawnPos.y < -0.75 || spawnPos.y > 0.75)) {
                    //spawn word object and picka word from list, remove from list after spawn so no duplicates
                    obj = Instantiate(word, spawnPos, Quaternion.identity);
                    int wordNum = Random.Range(0, words.Count);
                    obj.GetComponent<TextMeshPro>().text = words[wordNum];
                    words.RemoveAt(wordNum);

                    timer = 0;
                }
            }
        }
        else {
            playing = levelManager.isPlaying();
        }
    }
}
