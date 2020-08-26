using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gnat_Text : Gnat
{
    TextMeshPro word;

    private void Awake() {
        word = this.GetComponentInChildren<TextMeshPro>();
        base.Awake();
    }

    public override void Start() {
        //speedMultiplier = 0;//(GameManager.gamesWon + 1) * 0.1f;
        base.Start();
    }

    public void SetText(string setWord) {
        word.text = setWord;
    }

    public override void Despawn() {
        anim.SetTrigger("destroy");
        base.Despawn();
    }

    public override void FlipSprite() {
        return;
    }

    
}
