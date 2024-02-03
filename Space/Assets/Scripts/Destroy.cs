using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public LayerMask targetLayer;
    public GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        // Check if the collided object is on the specified layer
        if (collision.gameObject.layer == 6)
        {
            player.GetComponent<GrappleScript>().target_alive = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            // Perform actions or functions when a collision with the specified layer occurs
        }
    }
}
