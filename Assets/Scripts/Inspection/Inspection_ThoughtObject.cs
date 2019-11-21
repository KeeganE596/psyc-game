using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection_ThoughtObject : MonoBehaviour
{
    string thought;
    bool isPositive;

    Color32 green = new Color32(74, 156, 48, 255);
    Color32 red = new Color32(190, 74, 44, 255);

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void setThought(string text, bool ifPositive) {
        thought = text;
        isPositive = ifPositive;
    }

    public string getThought() {
        return thought;
    }

    public bool getIfPositive() {
        return isPositive;
    }

    public void turnGreen() {
        sprite.color = green;
    }

    public void turnRed() {
        StartCoroutine("flashRed");
    }

    IEnumerator flashRed() {
        sprite.color = red;
        yield return new WaitForSeconds(0.35f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.35f);
        sprite.color = red;
        yield return new WaitForSeconds(0.35f);
        sprite.color = Color.white;
    }
}
