using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection_ThoughtObject : MonoBehaviour
{
    string thought;
    bool isPositive;
    bool isChecked;

    Color32 green = new Color32(74, 156, 48, 255);
    Color32 red = new Color32(190, 74, 44, 255);

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        isChecked = false;
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

    public void ShowFocusRing() {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void HideFocusRing() {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void turnGreen() {
        sprite.color = green;
    }

    public void turnToSpark() {
        sprite.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        isChecked = true;
    }

    public void turnToGnatt() {
        sprite.enabled = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        isChecked = true;
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

    public bool GetIfChecked() {
        return isChecked;
    }
}
