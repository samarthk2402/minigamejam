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

    public float opacity1;
    public float opacity2;
    public float opacity3;

    //public GameObject damage;

    //public Renderer dRenderer;

    void Start(){
        //dRenderer = damage.GetComponent<Renderer>();
    }

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
                // SetOpacity(opacity1);
                break;
            case 2:
                heart3.SetActive(false);
                heart2.SetActive(true);
                heart1.SetActive(true);
                // SetOpacity(opacity2);
                break;
            case 3:
                heart3.SetActive(true);
                heart2.SetActive(true);
                heart1.SetActive(true);
                // SetOpacity(opacity3);
                break;
        }
    }

    // void SetOpacity(float alpha)
    // {
    //     // Ensure the alpha value is within the valid range (0 to 1)
    //     alpha = Mathf.Clamp01(alpha);

    //     // Get the current material color
    //     Color currentColor = dRenderer.material.color;

    //     // Set the new alpha value
    //     currentColor.a = alpha;

    //     // Update the material color with the new alpha value
    //     dRenderer.material.color = currentColor;
    // }
}
