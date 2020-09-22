using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat_Normal : Gnat
{
    private int gnatSpriteNumber;
    private Color angryColor = new Color32(62, 7, 7, 200);
    private Color frustratedColor = new Color32(32, 23, 47, 200);
    private Color lonelyColor = new Color32(43, 43, 43, 200);
    private Color sadColor = new Color32(5, 17, 54, 200);

    public override void ChooseSprite() 
    {
        gnatSpriteNumber = Random.Range(0, 3);
        transform.GetChild(gnatSpriteNumber).gameObject.SetActive(true);
    }

    protected override void ResetGnat()
    {
        transform.GetChild(gnatSpriteNumber).gameObject.SetActive(false);
        base.ResetGnat();
    }

    protected override void SpawnParticles()
    {
        GameObject ps = Instantiate(destroyParticle, gameObject.transform.position, Quaternion.identity);
        ParticleSystem.MainModule pModule = ps.GetComponent<ParticleSystem>().main;
        if (gnatSpriteNumber == 0)
        {
            pModule.startColor = sadColor;
        }
        else if (gnatSpriteNumber == 1)
        {
            pModule.startColor = frustratedColor;
        }
        else if (gnatSpriteNumber == 2)
        {
            pModule.startColor = angryColor;
        }
    }
}
