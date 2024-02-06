using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hearts;
    public GameObject explosionPrefab;
    public GameObject player;

    void Awake(){
        hearts = GameObject.Find("Hearts");
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);
        // Check if the collided object is on the specified layer
        if (collision.gameObject.layer != 7)
        {
            Explode();
            if(collision.gameObject.layer == 8){
                hearts.GetComponent<Health>().health -= 1;
                player.GetComponent<Animator>().SetTrigger("Damage");
            }
            Destroy(this.gameObject);

            // Perform actions or functions when a collision with the specified layer occurs
        }
    }

    void Explode()
    {
        // Instantiate the explosion effect
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Get the duration of the particle system
        float duration = explosion.GetComponent<ParticleSystem>().main.duration;

        // Destroy the explosion GameObject after the particle system finishes
        Destroy(explosion, duration);
    }
}
