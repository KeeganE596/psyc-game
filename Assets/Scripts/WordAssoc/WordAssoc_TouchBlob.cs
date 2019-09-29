﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordAssoc_TouchBlob : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Word") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
            col.enabled = false;
            hitWord(col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.CompareTag("Word") && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
                col.enabled = false;
                hitWord(col.gameObject);
        }
    }

    void hitWord(GameObject word) {
        word.transform.parent.gameObject.GetComponent<TextMeshPro>().color = new Color32(255, 255, 255, 255);
        
        word.AddComponent<LineRenderer>();
        LineRenderer line = word.GetComponent<LineRenderer>();
        line.startWidth = (0.04f);
        line.endWidth = (0.03f);
        line.SetPosition(1, new Vector2(0, 0));
        line.SetPosition(0, word.transform.position);
        line.gameObject.layer = 11;
    }
}
