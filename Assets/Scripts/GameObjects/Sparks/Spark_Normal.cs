using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_Normal : Spark
{
    protected override void Start() {
        //plays correct spawn animation based off instantiated name
        if (gameObject.name == "Spark_01(Clone)")
        {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetTrigger("SparkPrefab_01");
        }
        if (gameObject.name == "Spark_02(Clone)") {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetTrigger("SparkPrefab_02");
        } 

        base.Start();
    }
}
