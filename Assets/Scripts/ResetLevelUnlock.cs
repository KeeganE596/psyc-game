using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelUnlock : MonoBehaviour
{
    public void ResetUnlock()
    {
        SaveManager.ResetLevelunlock();
    }
}
