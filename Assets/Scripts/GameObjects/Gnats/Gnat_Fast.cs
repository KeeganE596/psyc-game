using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Fast : Gnat
{
    protected override void Start() {
        velocity = 2.65f;
        base.Start();
    }

    public override void Despawn()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        base.Despawn();
    }
}
