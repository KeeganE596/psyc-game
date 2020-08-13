using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spark_Text : Spark
{
    TextMeshPro word;

    void Awake() {
        word = this.GetComponentInChildren<TextMeshPro>();
    }

    public void SetText(string setWord) {
        word.text = setWord;
    }
}
