using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection_ThoughtObject : MonoBehaviour
{
    string thought;
    bool isPositive;

    // Start is called before the first frame update
    void Start() {
        thought = "Positive Thought";
        isPositive = true;
    }

    // Update is called once per frame
    void Update() {

    }

    public string getThought() {
        return thought;
    }

    public bool getIfPositive() {
        return isPositive;
    }
}
