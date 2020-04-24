using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt_Normal : Gnatt
{
    Animator anim;
    public GameObject destroyParticle;
    public override void Start() {
        //speedMultiplier = 0;//(GameManager.gamesWon + 1) * 0.1f;
        anim = this.GetComponentInChildren<Animator>();
        base.Start();
    }

    public override void Despawn() {
        //StartCoroutine("FadeOut");
        Instantiate(destroyParticle, gameObject.transform.position, Quaternion.identity);
        anim.SetTrigger("destroy");
        base.Despawn();
    }

    IEnumerator FadeOut() {
        sprite.color = new Color(1, 1, 1, alpha);
        while(alpha > 0) {
            alpha -= 0.04f;
            sprite.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        alpha = 1;
        sprite.color = new Color(1, 1, 1, alpha);
    }
}
