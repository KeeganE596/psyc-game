using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_Intro : Spark
{    
    void OnEnable() {
        anim.SetTrigger("SparkPrefab_01");
    }

    void Update() {
        //do nothing
    }
}
