using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBlob : MonoBehaviour
{
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


    private void Start()
    {
        anim = blob.GetComponent<Animator>();
    }

    public void Update()
    {
        if (anim.GetBool("leftblob") == true) {
        }
        else {
            PlayText.SetActive(false);
            startGameButton.SetActive(false);
        }
    }

    public void Toggle_changedL(bool newValue) {
        anim.SetBool("leftblob", newValue);
    }
    public void Toggle_changedR(bool newValue) {
        anim.SetBool("rightblob", newValue); 
    }

  }


