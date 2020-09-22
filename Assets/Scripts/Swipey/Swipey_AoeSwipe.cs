using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_AoeSwipe: controls an invisible object which is moved to where the user swipes
//and detects gnats inside its collider, these gnats are then 'swiped' away
public class Swipey_AoeSwipe : MonoBehaviour
{
    List<GameObject> gnats;

    void Awake() {
        gnats = new List<GameObject>(5);
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Gnat")) {
            gnats.Add(col.gameObject);
        }
     }

    //Return the List of Gnats that has collided with AOE Swipe
     public List<GameObject> getGnats() {
        return gnats;
     }

    //Return if hit Gnats list is empty
     public bool isGnatsEmpty() {
        if(gnats.Count > 0) {
            return false;
        }
        return true;
     }

    //Apply force to all gantts that have been 'hit' and call their despawn method
     public void flickGnats(Vector2 direction, float timeInterval, float throwForce) {
        foreach(GameObject g in gnats) {
            //if(g.GetComponent<Gnat>().GetLives() <= 0) {
            //    //add force onto rigid body depending on swipe time, direction and throw force.
            //    Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
            //    rb.AddForce(-direction / timeInterval * throwForce);

            //    g.GetComponent<Collider2D>().enabled = false;
            //}

            //g.GetComponent<Gnat>().Despawn();
        }
        gnats.Clear();
    }

    public void clearGnats() {
        gnats.Clear();
    }
}
