using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordFind_SelectWords : MonoBehaviour
{
    [HideInInspector] public List<string> wordsList_1;
    [HideInInspector] public List<string> wordsList_2;
    List<GameObject> pickWordsObjs;
    public GameObject wordObjPrefab;

    List<string> selectedWords;
    int selectedWordsLimit = 2;

    GameObject canvasPackage;
    [HideInInspector] public bool screenClear = false;

    //public GameObject circle; //for testing

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        selectedWords = new List<string>();
        pickWordsObjs = new List<GameObject>();
        canvasPackage = GameObject.FindWithTag("Word");
        Start();
    }

    // Start is called before the first frame update
    public void Start() {
        wordsList_1 = new List<string>();
        wordsList_1.Add("Adventure"); wordsList_1.Add("Beauty"); wordsList_1.Add("Caring");
        wordsList_1.Add("Challenge"); wordsList_1.Add("Compassion"); wordsList_1.Add("Connection");
        wordsList_1.Add("Courage"); wordsList_1.Add("Creativity"); wordsList_1.Add("Curiosity");

        wordsList_2 = new List<string>();
        wordsList_2.Add("Encouragement"); wordsList_2.Add("Friendliness"); wordsList_2.Add("Fun");
        wordsList_2.Add("Excitement"); wordsList_2.Add("Gratitude"); wordsList_2.Add("Honesty");
        wordsList_2.Add("Fitness"); wordsList_2.Add("Humour"); wordsList_2.Add("Self-Care");

        

        
        //canvasPackage.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(selectedWords.Count == selectedWordsLimit) {
            //canvasPackage = GameObject.FindWithTag("Word");
            canvasPackage.transform.GetChild(2).gameObject.SetActive(true);
            //continueButton.SetActive(true);
        }
        else {
            //canvasPackage = GameObject.FindWithTag("Word");
            canvasPackage.transform.GetChild(2).gameObject.SetActive(false);
            //continueButton.SetActive(false);
        }
    }

    public void SpawnWords(int listNum) {
        
        int safety = 0;
        int wordCount = 0;
        if(listNum == 1) { wordCount = wordsList_1.Count; }
        else if(listNum == 2) { wordCount = wordsList_2.Count; }

        while(pickWordsObjs.Count < wordCount) {
            Vector2 spawnPos = new Vector2(0, 0);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(200f, Screen.width - 200), Random.Range(200, Screen.height - 250)));
            // Vector2 testPos = Camera.main.ScreenToWorldPoint(new Vector2(200, 150));
            // Vector2 testPos2 = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 200, Screen.height - 150));
            // if(wordObjs.Count == 0) {
            //     Instantiate(circle, testPos, Quaternion.identity);
            //     Instantiate(circle, testPos2, Quaternion.identity);
            //     //Instantiate(circle, new Vector2(spawnPos.x+2.5f, spawnPos.y+0.8f), Quaternion.identity);
            //     //Instantiate(circle, spawnPos, Quaternion.identity);
            // }
            
            if(CheckPosForOverlap(spawnPos)) {
                pickWordsObjs.Add(Instantiate(wordObjPrefab, spawnPos, Quaternion.identity));
            }

            safety++;   //Prevent game crash from too many iterations
            if(safety > 100) {
                Debug.Log("too many attempts");
                break;
            }
        }
        for(int i=0; i<pickWordsObjs.Count; i++) {
            if(listNum == 1) {
                pickWordsObjs[i].GetComponent<WordFind_WordObject>().SetText(wordsList_1[i]);
            }
            else if(listNum == 2) {
                pickWordsObjs[i].GetComponent<WordFind_WordObject>().SetText(wordsList_2[i]);
            }
            
        }
        Debug.Log(pickWordsObjs.Count);
        canvasPackage.SetActive(true);
    }

    bool CheckPosForOverlap(Vector2 pos) {
        if(pickWordsObjs.Count == 0) {
            return true;
        }

        foreach(GameObject word in pickWordsObjs) {
            Vector2 otherPos = word.transform.position;
            if((pos.x - otherPos.x < 3.25f) && (pos.x - otherPos.x > -3.25f)) {
                if((pos.y - otherPos.y < 1f) && (pos.y - otherPos.y > -1f)) {
                    return false;
                }
            }
        }
        return true;
    }

    public bool AddWordToSelected(string word) {
        if(screenClear) {
            return true;
        }
        else if(selectedWords.Count < selectedWordsLimit) {
            selectedWords.Add(word);
            return true;
        }
        return false;
    }

    public void RemoveWordFromSelected(string word) {
        if(selectedWords.Contains(word)) {
            selectedWords.Remove(word);
        }
    }

    public void NextWordList() {
        foreach(GameObject word in pickWordsObjs) {
            Destroy(word);
        }
        pickWordsObjs.Clear();

        if(selectedWords.Count < 4){
            selectedWordsLimit = 4;
            SpawnWords(2);
        }
        else {
            canvasPackage.SetActive(false);
            screenClear = true;
        }
    }

    public bool GetIfSelectedWords() {
        if(selectedWords.Count == 4) {
            return true;
        }
        return false;
    }

    public List<string> GetSelectedWords() {
        return selectedWords;
    }
}
