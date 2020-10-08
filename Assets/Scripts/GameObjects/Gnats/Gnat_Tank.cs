using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Tank : Gnat
{
    //[SerializeField] private SpriteRenderer shieldImage;
    private int lives = 1;

    protected override void Start() {
        velocity = 0.8f;
        base.Start();
    }

    public override void Despawn(Vector2 gnatThrow) {
        if(lives > 0){
            lives--;
            //shieldImage.enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        else {
            base.Despawn(gnatThrow);
        }
    }
}
