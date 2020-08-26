using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Fast : Gnat
{
    public override void Start() {
        speedMultiplier = (GameManager.gamesWon + 1) * 0.85f;
        base.Start();
    }
}
