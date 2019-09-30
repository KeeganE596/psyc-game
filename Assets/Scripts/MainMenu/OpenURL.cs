using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    //Open URL on PC
    public void OpenWebsite() {
        Application.OpenURL("https://www.victoria.ac.nz/courses/fhss/107/2019/offering?crn=30171");

    }
}
