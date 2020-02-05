using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt_Tank : Gnatt
{
    int lives = 1;

    public override void Start() {
        speedMultiplier = (GameManager.gamesWon + 1) * 0.2f;
        base.Start();
    }

    public override void Despawn() {
        if(lives > 0){
            lives--;
            velocity = velocity * 1.1f;
            sprite.color = new Color(1, 1, 1, alpha);
            sprite.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        else {
            base.Despawn();
        }
    }

    public override int GetLives() {
        return lives;
    }
}
