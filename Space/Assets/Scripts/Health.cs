using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // Update is called once per frame
    void Update()
    {
        switch(health){
            case 0:
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(false);
                SceneManager.LoadScene(2);
                break;
            case 1:
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(true);
                break;
            case 2:
                heart3.SetActive(false);
                heart2.SetActive(true);
                heart1.SetActive(true);
                break;
            case 3:
                heart3.SetActive(true);
                heart2.SetActive(true);
                heart1.SetActive(true);
                break;
        }
    }
}
