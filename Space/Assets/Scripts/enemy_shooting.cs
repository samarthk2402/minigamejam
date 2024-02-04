using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;

    public float bullet_speed;
    public float timer = 0;
    public float shootingTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= shootingTime){
            timer = 0;
            Shoot();
            //Debug.Log("shoot");
        }
    }

    private void Shoot(){
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D bullet_rb = b.GetComponent<Rigidbody2D>();

        bullet_rb.velocity = ((player.transform.position - transform.position).normalized)*bullet_speed;

        Vector3 rot = player.transform.position - b.transform.position;

        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        b.transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }
}
