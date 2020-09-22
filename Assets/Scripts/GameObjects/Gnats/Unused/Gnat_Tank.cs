using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Tank : Gnat
{
    [SerializeField] private SpriteRenderer shieldImage;
    private int lives = 1;

    void Start() {
        velocity = 0.8f;
    }

    public override void Despawn(Vector2 gnatThrow) {
        if(lives > 0){
            lives--;
            sprite.color = new Color(1, 1, 1, alpha);
            sprite.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            shieldImage.enabled = false;
        }
        else {
            base.Despawn(gnatThrow);
        }
    }
}
