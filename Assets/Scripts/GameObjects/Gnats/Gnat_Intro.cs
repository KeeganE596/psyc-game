using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Intro : Gnat
{
    public Vector2 target = new Vector2 (0f, 0f);

    // Start is called before the first frame update
    void Start(){  
        base.Start();
    }

    // Update is called once per frame
    void Update(){
        this.transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);
    }

    public override void Despawn() {
        base.Despawn();
    }

    public override void ChooseSprite() {
        int num = Random.Range(0, 3);
        this.transform.GetChild(num).gameObject.SetActive(true);
    }
}
