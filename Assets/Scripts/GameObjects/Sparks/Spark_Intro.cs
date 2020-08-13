using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_Intro : Spark
{
    public Vector2 target = new Vector2(0, 0);
    
    void Awake() {
        anim = this.gameObject.GetComponent<Animator>();
    }

    void OnEnable() {
        anim.SetTrigger("SparkPrefab_01");
    }

    void Update() {
    }
}
