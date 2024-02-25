using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Destroy : MonoBehaviour
{
    public GameObject cinemachineCam;
    private CinemachineVirtualCamera cam;
    public GameObject scoreText;
    public float shakeIntensity;
    public float shakeTime;
    public LayerMask targetLayer;
    public GameObject player;
    public GameObject explosionPrefab;
    public float min_velocity;

    private float shakeTimer = 0;

    void Start(){
        player = GameObject.Find("Player");
        cinemachineCam = GameObject.Find("TrackCam");
        cam = cinemachineCam.GetComponent<CinemachineVirtualCamera>();
        scoreText = GameObject.Find("Canvas");
    }

    public void Shake(float intensity, float timer){
        CinemachineBasicMultiChannelPerlin cbmcp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = intensity;
        shakeTimer = 0;
    }

    void Update(){
        shakeTimer += Time.deltaTime;
        if(shakeTimer>shakeTime){
            CinemachineBasicMultiChannelPerlin cbmcp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cbmcp.m_AmplitudeGain = 0;
            shakeTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        // Check if the collided object is on the specified layer
        if (collision.gameObject.layer == 6)
        {
            if(collision.gameObject.GetComponent<Swing>().attached && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > min_velocity){
                Explode();
                Shake(shakeIntensity, shakeTime);
                scoreText.GetComponentInChildren<Score>().score += 1;
                //Debug.Log(scoreText);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
            //player.GetComponent<GrappleScript>().targetAlive = false;

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
