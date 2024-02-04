using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hearts;

    void Awake(){
        hearts = GameObject.Find("Hearts");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        // Check if the collided object is on the specified layer
        if (collision.gameObject.layer != 7)
        {
            if(collision.gameObject.layer == 8){
                hearts.GetComponent<Health>().health -= 1;
            }
            Destroy(this.gameObject);

            // Perform actions or functions when a collision with the specified layer occurs
        }
    }
}
