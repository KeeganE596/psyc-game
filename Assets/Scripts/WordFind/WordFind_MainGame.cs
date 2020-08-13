using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordFind_MainGame : MonoBehaviour
{
    LevelManager levelManager;
    WordFind_SelectWords selectWordsScript;
    bool ifWordsSelected;

    List<string> allWords;
    List<string> selectedWords;
    List<string> currentlySelectedWords;
    List<GameObject> wordObjs;
    public GameObject wordObjPrefab;
    public GameObject SelectWordsManagerPrefab;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        
        if(GameObject.FindGameObjectsWithTag("SelectedWords").Length < 1) {
            Instantiate(SelectWordsManagerPrefab);
        }
        selectWordsScript = GameObject.FindWithTag("SelectedWords").GetComponent<WordFind_SelectWords>();
        //selectWordsScript.Start();
    }

    // Start is called before the first frame update
    void Start() {
        ifWordsSelected = selectWordsScript.GetIfSelectedWords();


        if(!ifWordsSelected) {
            selectWordsScript.SpawnWords(1);
        }

        allWords = new List<string>();
        foreach(string word in selectWordsScript.wordsList_1) {
            allWords.Add(word);
        }
        foreach(string word in selectWordsScript.wordsList_2) {
            allWords.Add(word);
        }

        wordObjs = new List<GameObject>();
        currentlySelectedWords = new List<string>();
    }

    // Update is called once per frame
    void Update() {
        if(LevelManager.Instance.GetIfGameIsPlaying() && selectWordsScript.GetIfSelectedWords() && selectWordsScript.screenClear) {
            if(wordObjs.Count == 0) {
                SpawnAllWords();
                levelManager.StartTimer();
            }

            foreach(GameObject wordObj in wordObjs) {
                string word = wordObj.GetComponent<WordFind_WordObject>().GetText();
                if(wordObj.GetComponent<WordFind_WordObject>().GetIfSelected() && selectedWords.Contains(word)) {
                    if(!currentlySelectedWords.Contains(word)) {
                        currentlySelectedWords.Add(word);
                    }
                }
            }

            if(currentlySelectedWords.Count == selectedWords.Count) {
                levelManager.GameWon();
            }
        }
    }

    void SpawnAllWords() {
        selectedWords = selectWordsScript.GetSelectedWords();
        int safety = 0;

        while(wordObjs.Count < allWords.Count) {
            Vector2 spawnPos = new Vector2(0, 0);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(225f, Screen.width - 225), Random.Range(125, Screen.height - 150)));
            
            if(CheckPosForOverlap(spawnPos)) {
                wordObjs.Add(Instantiate(wordObjPrefab, spawnPos, Quaternion.identity));
            }

            safety++;   //Prevent game crash from too many iterations
            if(safety > 100) {
                Debug.Log("too many attempts");
                break;
            }
        }
        
        for(int i=0; i<4; i++) {
            wordObjs[i].GetComponent<WordFind_WordObject>().SetText(selectedWords[i]);
            allWords.Remove(selectedWords[i]);
        }
        for(int i=4; i<wordObjs.Count; i++) {
            int index = Random.Range(0, allWords.Count);
            wordObjs[i].GetComponent<WordFind_WordObject>().SetText(allWords[index]);
            allWords.RemoveAt(index);
        }
    }

    bool CheckPosForOverlap(Vector2 pos) {
        if(wordObjs.Count == 0) {
            return true;
        }

        foreach(GameObject word in wordObjs) {
            Vector2 otherPos = word.transform.position;
            if((pos.x - otherPos.x < 3f) && (pos.x - otherPos.x > -3f)) {
                if((pos.y - otherPos.y < 0.8f) && (pos.y - otherPos.y > -0.8f)) {
                    return false;
                }
            }
        }
        return true;
    }

    public void NextList() {
        selectWordsScript.NextWordList();
    }
}
