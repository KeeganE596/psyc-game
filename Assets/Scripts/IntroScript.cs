using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public void ButtonPressed() {
        SaveManager.SaveLevelUnlocked(1);
        GameManagerStatic.PickGame("swipeAway", 1);
    }
}
