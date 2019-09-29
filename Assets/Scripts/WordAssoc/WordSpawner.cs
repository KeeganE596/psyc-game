using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordSpawner : MonoBehaviour
{
    List<string> words;

    public GameObject canvas;
    public GameObject word;
    GameObject obj;

    float timer;

    // Start is called before the first frame update
    void Start() {
        words = new List<string>();

        words.Add("courage");   words.Add("caring");   words.Add("excitement");
        words.Add("encourage");   words.Add("fun");   words.Add("humour");
        words.Add("gratitude");   words.Add("patience");   words.Add("persistence");

        timer = 0;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if(timer > 1.5 && words.Count > 0) {
            Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));
            obj = Instantiate(word, spawnPos, Quaternion.identity);
            int wordNum = Random.Range(0, words.Count);
            obj.GetComponent<TextMeshPro>().text = words[wordNum];
            words.RemoveAt(wordNum);

            timer = 0;
        }
        
    }
}
