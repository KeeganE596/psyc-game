using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{

    List<string> words;

    // Start is called before the first frame update
    void Start() {
        words = new List<string>();

        words.Add("courage");   words.Add("caring");   words.Add("excitement");
        words.Add("encourage");   words.Add("fun");   words.Add("humour");
        words.Add("gratitude");   words.Add("patience");   words.Add("persistence");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
