using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        // Check if the collided object is on the specified layer
        if (collision.gameObject.layer != 7)
        {
            Destroy(this.gameObject);

            // Perform actions or functions when a collision with the specified layer occurs
        }
    }
}
