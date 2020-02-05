using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt_Normal : Gnatt
{
    public override void Start() {
        speedMultiplier = (GameManager.gamesWon + 1) * 0.1f;
        base.Start();
    }
}
