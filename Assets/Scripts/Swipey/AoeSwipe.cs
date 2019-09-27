using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSwipe : MonoBehaviour
{
    List<GameObject> gnatts;
    GameObject[] gs;

    // Start is called before the first frame update
    void Awake()
    {
        gnatts = new List<GameObject>(5);
    }

    public void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.CompareTag("Gnatt")) {
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

     public void flickGnatts(Vector2 direction, float timeInterval, float throwForce) {
        foreach(GameObject g in gnatts) {
            //add force onto rigid body depending on swipe time, direction and throw force.
            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
            rb.AddForce(-direction / timeInterval * throwForce);

            g.GetComponent<CircleCollider2D>().enabled = false;

            Destroy(g, 2f);
        }
        gnatts.Clear();
    }

    public void clearGnatts() {
        gnatts.Clear();
    }
}
