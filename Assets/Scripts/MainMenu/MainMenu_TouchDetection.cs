using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_TouchDetection : MonoBehaviour
{
    public GameObject obj;
    bool hitObj;
    bool touching;

    public GameObject blob;
    private Animator anim;
    public GameObject leftToggle;
    public GameObject rightToggle;
    public GameObject PlayText;
    public GameObject startGameButton;

    private void Awake()
    {
        PlayText.SetActive(false);
        startGameButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        touching = false;
        hitObj = false;
        anim = blob.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(screenPos,Vector2.zero);
        //Raycast from camera
        if (hit && hit.collider.gameObject.tag == "Test")
        {
            hitObj = true;
        }
        



        if (!anim.GetBool("leftblob")) {
            PlayText.SetActive(false);
            startGameButton.SetActive(false);
        }
    }

    public void menuItemDetected(string name) {
        if(name == "Play") {
            Toggle_changedL(true);
        }
        else if(name == "Settings") {
            Toggle_changedR(true);
        }
        else {
            Toggle_changedL(false);
            Toggle_changedR(false);
        }
    }

    public void Toggle_changedL(bool newValue) {
        anim.SetBool("leftblob", newValue);

        if(newValue) {
            PlayText.SetActive(true);
            startGameButton.SetActive(true);
        }

    }
    public void Toggle_changedR(bool newValue) {
        anim.SetBool("rightblob", newValue); 
    }
}
