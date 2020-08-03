using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntroEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyGnats() {
        GameObject[] gnats = GameObject.FindGameObjectsWithTag("Gnatt");
        foreach(GameObject g in gnats) {
            g.GetComponentInChildren<Animator>().SetTrigger("destroy");
        }
    }

    public void DestroySparks() {
        GameObject[] sparks = GameObject.FindGameObjectsWithTag("Spark");
        foreach(GameObject s in sparks) {
            s.GetComponent<Animator>().SetTrigger("Activate");
        }
    }

    public void StartSparks() {
        GameObject[] sparks = GameObject.FindGameObjectsWithTag("Spark");
        foreach(GameObject s in sparks) {
            if (s.name == "Spark_01(Clone)") {
                s.GetComponent<Animator>().SetTrigger("SparkPrefab_01");
            }
            else if (s.name == "Spark_02(Clone)" || s.name == "Spark_03(Clone)") {
                s.GetComponent<Animator>().SetTrigger("SparkPrefab_02");
            }
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("swipeAway_Game");
    }
}
