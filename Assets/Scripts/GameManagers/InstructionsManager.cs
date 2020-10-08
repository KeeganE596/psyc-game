using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsParent;
    private List<GameObject> instructions;


    // Start is called before the first frame update
    void Start()
    {
        instructions = new List<GameObject>();
        GetInstructions();
    }

    private void GetInstructions()
    {
        foreach (Transform child in instructionsParent.transform)
        {
            if (child != instructionsParent.transform && child != null)
            {
                instructions.Add(child.gameObject);
            }
        }
    }

    public void ShowInstructions(int levelNum)
    {
        for(int i = 0; i< instructions.Count; i++)
        {
            if(i == levelNum)
                instructions[i].SetActive(true);
            else
                instructions[i].SetActive(false);
        }
    }
}
