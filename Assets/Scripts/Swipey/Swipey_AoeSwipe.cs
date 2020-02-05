using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_AoeSwipe: controls an invisible object which is moved to where the user swipes
//and detects gnatts inside its collider, these gnatts are then 'swiped' away
public class Swipey_AoeSwipe : MonoBehaviour
{
    List<GameObject> gnatts;

    void Awake() {
        gnatts = new List<GameObject>(5);
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Gnatt")) {
            gnatts.Add(col.gameObject);
        }
     }

    //Return the List of Gnatts that has collided with AOE Swipe
     public List<GameObject> getGnatts() {
        return gnatts;
     }

    //Return if hit Gnatts list is empty
     public bool isGnattsEmpty() {
        if(gnatts.Count > 0) {
            return false;
        }
        return true;
     }

    //Apply force to all gantts that have been 'hit' and call their despawn method
     public void flickGnatts(Vector2 direction, float timeInterval, float throwForce) {
        foreach(GameObject g in gnatts) {
            if(g.GetComponent<Gnatt>().GetLives() <= 0) {
                //add force onto rigid body depending on swipe time, direction and throw force.
                Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                rb.AddForce(-direction / timeInterval * throwForce);

                g.GetComponent<CircleCollider2D>().enabled = false;
            }

            g.GetComponent<Gnatt>().Despawn();
        }
        gnatts.Clear();
    }

    public void clearGnatts() {
        gnatts.Clear();
    }
}
