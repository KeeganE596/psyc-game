using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gnatt_Text : Gnatt
{
    TextMeshPro word;
    Animator anim;

    protected override void Awake() {
        word = this.GetComponentInChildren<TextMeshPro>();
        anim = this.GetComponent<Animator>();
        base.Awake();
    }

    public override void Start() {
        speedMultiplier = (GameManager.gamesWon + 1) * 0.1f;
        base.Start();
    }

    public void SetText(string setWord) {
        word.text = setWord;
    }

    public override void Despawn() {
        doDespawn = true;
        Debug.Log("here");
        anim.SetTrigger("destroy");
    }

    public override void FlipSprite() {
        return;
    }
}
