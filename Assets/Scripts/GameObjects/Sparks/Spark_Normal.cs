using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_Normal : Spark
{
    void OnEnable() {
        //plays correct spawn animation based off instantiated name
        if (gameObject.name == "Spark_01(Clone)") {
            anim.SetTrigger("SparkPrefab_01");
        }
        else if (gameObject.name == "Spark_02(Clone)") {
            anim.SetTrigger("SparkPrefab_02");
        }
        else if (gameObject.name == "Spark_03(Clone)") {
            anim.SetTrigger("SparkPrefab_02");
        }
    }
}
