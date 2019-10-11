using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Animations : MonoBehaviour
{
    public GameObject blob;
    private Animator anim;
    public GameObject leftToggle;
    public GameObject rightToggle;
    public GameObject PlayText;

    // Start is called before the first frame update
    void Start()
    {
        PlayText.SetActive(false);
        anim = blob.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else {
            PlayText.SetActive(false);
        }
    }
    public void Toggle_changedR(bool newValue) {
        anim.SetBool("rightblob", newValue); 
    }

    public void openSettingsPanel(bool newValue)
    {
        anim.SetBool("centerblob", newValue);


    }




}
