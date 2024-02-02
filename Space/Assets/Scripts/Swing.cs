using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{

    public bool attached = false;
    private Rigidbody2D rb;
    private Vector3 dir;

    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(attached){
            dir = (mousePos-transform.position).normalized;
            rb.velocity =dir * speed;
        }
    }
}
