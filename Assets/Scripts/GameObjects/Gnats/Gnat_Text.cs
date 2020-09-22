using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gnat_Text : Gnat
{
    TextMeshPro word;

    private void Awake() {
        word = GetComponentInChildren<TextMeshPro>();
    }

    public void SetText(string setWord) {
        word.text = setWord;
    }

    public override void FlipSprite() {
        return;
    }

    
}
