using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachHook : MonoBehaviour
{
    public GameObject rotPoint;
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.layer == 6){
            col.gameObject.GetComponent<Swing>().attached = true;
            rotPoint.GetComponent<Aim>().attached = col.gameObject;
            
        }
    }
}
