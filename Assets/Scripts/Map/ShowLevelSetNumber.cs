using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLevelSetNumber : MonoBehaviour
{
    private void Start()
    {
        SetNumber(GameManagerStatic.GetCurrentLevelSetNumber());
    }
    
    public void SetNumber(int num)
    {
        GetComponent<TextMeshProUGUI>().text = num.ToString();
    }
}
