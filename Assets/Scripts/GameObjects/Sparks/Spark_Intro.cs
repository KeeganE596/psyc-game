using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_Intro : Spark
{    
    void Awake() {
        anim = this.gameObject.GetComponent<Animator>();
    }

    void OnEnable() {
        anim.SetTrigger("SparkPrefab_01");
    }

    void Update() {
        //do nothing
    }
}
