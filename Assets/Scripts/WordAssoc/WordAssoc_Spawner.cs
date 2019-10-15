using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordAssoc_Spawner : MonoBehaviour
{
    List<string> words;
    List<string> badWords;

    List<Vector2> wordPositions;

    public GameObject canvas;
    LevelManager levelManager;
    public GameObject word;
    public GameObject badword;
    GameObject obj;
    bool playing;

    float timer;
    float badtimer;

    // Start is called before the first frame update
    void Start() {
        words = new List<string>();
        badWords = new List<string>();
        wordPositions = new List<Vector2>();

        words.Add("courage");   words.Add("caring");   words.Add("excitement");
        words.Add("encourage");   words.Add("fun");   words.Add("humour");
        words.Add("gratitude");   words.Add("patience");   words.Add("persistence");

        badWords.Add("impatient"); badWords.Add("anger"); badWords.Add("disrespect");
        badWords.Add("can't"); badWords.Add("dishonest"); badWords.Add("impossible");

        timer = 0.5f;
        badtimer = 0.25f;

        levelManager = canvas.GetComponent<LevelManager>();
        playing = levelManager.isPlaying();
    }

    // Update is called once per frame
    void Update() {
        //Check player isn't on instruction screen
        if(playing) {
            timer += Time.deltaTime;
            badtimer += Time.deltaTime;

            //Good words spawning timer logic
            if(timer > 1 && words.Count > 0) {
                //Get rendom position to spawn word at
                Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));
    
                //check position isnt too close to middle
                if(CheckPos(spawnPos)) {
                    //spawn word object and picks word from list, remove from list after spawn so no duplicates
                    obj = Instantiate(word, spawnPos, Quaternion.identity);
                    int wordNum = Random.Range(0, words.Count);
                    obj.GetComponent<TextMeshPro>().text = words[wordNum];
                    words.RemoveAt(wordNum);

                    timer = 0;
                }
            }
            //Bad words spawning timer logic
            if (badtimer > 1.5 && badWords.Count > 0) {
                //Get rendom position to spawn word at
                Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width - 300), Random.Range(50, Screen.height - 100)));

                //check position isnt too close to middle
                if (CheckPos(spawnPos)) {
                    //spawn badword object and picks word from list, remove from list after spawn so no duplicates
                    obj = Instantiate(badword, spawnPos, Quaternion.identity);
                    int wordNum = Random.Range(0, badWords.Count);
                    obj.GetComponent<TextMeshPro>().text = badWords[wordNum];
                    badWords.RemoveAt(wordNum);

                    badtimer = 0;
                }
            }
        }
        else {
            playing = levelManager.isPlaying();
        }
    }

    bool CheckPos(Vector2 pos) {
        if ((pos.x < -0.75 || pos.x > 0.75) && (pos.y < -0.75 || pos.y > 0.75)) {   //Check not to close to center blob
            if(wordPositions.Count == 0) {  //If its the first word just spawn anywhere
                wordPositions.Add(pos);
                return true;
            }
            bool check = true;
            foreach (Vector2 p in wordPositions) {  //Check all positions of current words, if to close check=false
                if ((pos.x - p.x < 3.25) && (pos.x - p.x > -3.25)) {
                    if ((pos.y - p.y < 1.25) && (pos.y - p.y > -1.25)) {
                        check = false;
                    }
                }
            }
            if(check) {
                wordPositions.Add(pos);
                return true;
            }
        }
        return false; 
    }
}
