using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//WordAssoc_Spawner: spanws good and bad words at the appropriate times
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
    float badSpawnTime;

    int levelScaler;
    
    void Awake() {
        levelManager = canvas.GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        playing = levelManager.isPlaying();
        levelScaler = levelManager.getNumberGamesWon();

        words = new List<string>();
        badWords = new List<string>();
        wordPositions = new List<Vector2>();

        // words.Add("courage");   words.Add("caring");   words.Add("excitement");
        // words.Add("encourage");   words.Add("fun");   words.Add("humour");
        // words.Add("gratitude");   words.Add("patience");   words.Add("persistence");
        // words.Add("kindness");  words.Add("leadership"); words.Add("creativity");
        // words.Add("honesty");   words.Add("hope");  words.Add("thankful");
        // words.Add("respect");
        words.Add("Going for a walk");  words.Add("Talking with a friend");  words.Add("Reading a book");
        words.Add("Exercising");  words.Add("Slow breathing");  words.Add("Getting enough sleep");
        words.Add("Cooking");  words.Add("Playing music");  words.Add("Time with family");
        words.Add("A balanced diet");

        badWords.Add("impatient"); badWords.Add("anger"); badWords.Add("disrespect");
        badWords.Add("can't"); badWords.Add("dishonest"); badWords.Add("impossible");
        badWords.Add("hate");   badWords.Add("lieing");    badWords.Add("give up");

        timer = 0.5f;
        badtimer = 0.25f;
        if(levelScaler <= 10) {
            badSpawnTime = 2.5f - (0.1f*levelScaler);
        }
        else { badSpawnTime = 2.5f - (0.1f*10); }
    }

    // Update is called once per frame
    void Update() {
        //Check player isn't on instruction screen
        if(levelManager.isPlaying()) {
            timer += Time.deltaTime;
            badtimer += Time.deltaTime;

            //Good words spawning timer logic
            if(timer > 1.2f && words.Count > 0) {
                //Get random position to spawn word at
                Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(175, Screen.width-175), Random.Range(50, Screen.height-50)));
    
                //check position isnt too close to middle
                if(CheckPos(spawnPos)) {
                    //spawn word object and picks word from list, remove from list after spawn so no duplicates
                    obj = Instantiate(word, new Vector3(spawnPos.x, spawnPos.y, -9), Quaternion.identity);
                    int wordNum = Random.Range(0, words.Count);
                    //Vector2 wordSize = obj.GetComponent<TextMeshPro>().GetPreferredValues(words[wordNum]);
                    //Vector2 wordSize = new Vector2(obj.GetComponent<TextMeshPro>().preferredWidth, obj.GetComponent<TextMeshPro>().preferredHeight);
                    obj.GetComponent<TextMeshPro>().text = words[wordNum];
                    //obj.transform.localScale = wordSize;
                    //Debug.Log(wordSize);
                    //obj.GetComponentInChildren<BoxCollider2D>().size = wordSize;//new Vector3(wordSize.x, wordSize.y, 0);
                    words.RemoveAt(wordNum);

                    timer = 0;
                }
            }
            //Bad words spawning timer logic
            // if (badtimer > badSpawnTime && badWords.Count > 0) {
            //     //Get rendom position to spawn word at
            //     Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width - 300), Random.Range(50, Screen.height - 100)));

            //     //check position isnt too close to middle
            //     if (CheckPos(spawnPos)) {
            //         //spawn badword object and picks word from list, remove from list after spawn so no duplicates
            //         obj = Instantiate(badword, spawnPos, Quaternion.identity);
            //         int wordNum = Random.Range(0, badWords.Count);
            //         obj.GetComponent<TextMeshPro>().text = badWords[wordNum];
            //         badWords.RemoveAt(wordNum);

            //         badtimer = 0;
            //     }
            // }
        }
    }

    bool CheckPos(Vector2 pos) {
        if ((pos.x < -1.4 || pos.x > 1.4) && (pos.y < -1.25 || pos.y > 1.25)) {   //Check not to close to center blob
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
