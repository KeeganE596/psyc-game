using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSwipe : MonoBehaviour
{
    List<GameObject> gnatts;
    GameObject[] gs;

    // Start is called before the first frame update
    void Start()
    {
        gnatts = new List<GameObject>();
    }

    public void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.tag == "Gnatt") {
             gnatts.Add(col.gameObject);
         }
     }

    //Return the List of Gnatts that has collided with AOE Swipe
     public List<GameObject> getGnatts() {
        return gnatts;
     }

    //Return if any Gnatts have been hit
     public bool isGnattsEmpty() {
        if(gnatts.Count > 0) {
            return false;
        }
        return true;
     }
}
