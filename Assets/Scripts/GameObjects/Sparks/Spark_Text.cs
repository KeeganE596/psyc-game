using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spark_Text : Spark
{
    TextMeshPro word;

    protected override void Awake() {
        word = this.GetComponentInChildren<TextMeshPro>();
        base.Awake();
    }

    protected override void Start() {
        base.Start();
    }

    public void SetText(string setWord) {
        word.text = setWord;
    }
}
