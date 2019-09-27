using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_InformationButton : MonoBehaviour
{

    private Animator anim;
    public GameObject CloseInfoButton;

    private void Awake()
    {
        CloseInfoButton.SetActive(false);
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Toggle_InformationPanel(bool newValue)
    {

        anim.SetBool("showInfo", newValue);
        CloseInfoButton.SetActive(newValue);
    }
    public void Close_InformationPanel(bool newValue) {
        anim.SetBool("showInfo", newValue);
        CloseInfoButton.SetActive(newValue);
        
    }
}
