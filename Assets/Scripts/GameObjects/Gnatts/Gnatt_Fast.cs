using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt_Fast : Gnatt
{
    public override void Start() {
        speedMultiplier = (GameManager.gamesWon + 1) * 0.85f;
        base.Start();
    }
}
