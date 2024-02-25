using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
    public Animator playerAnim;
    public Animator transitionAnim;
    public int health = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public float intensity1;
    public float intensity2;
    public float intensity3;

    public VolumeProfile volumeProfile;
    public float intensity = 0f;

    Vignette vignette;

    //public GameObject damage;

    //public Renderer dRenderer;

    void Start(){
        //dRenderer = damage.GetComponent<Renderer>();
        if (volumeProfile == null)
        {
            Debug.LogError("Volume Profile is not assigned.");
            return;
        }

        // Try to get the Vignette component from the volume profile
        if (!volumeProfile.TryGet(out vignette))
        {
            Debug.LogError("Vignette is not found in the Volume Profile.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = intensity;
        }
        switch(health){
            case 0:
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(false);
                StartCoroutine(LoadScene());
                break;
            case 1:
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(true);
                intensity = intensity3;
                // SetOpacity(opacity1);
                break;
            case 2:
                heart3.SetActive(false);
                heart2.SetActive(true);
                heart1.SetActive(true);
                intensity = intensity2;
                // SetOpacity(opacity2);
                break;
            case 3:
                heart3.SetActive(true);
                heart2.SetActive(true);
                heart1.SetActive(true);
                intensity = intensity1;
                // SetOpacity(opacity3);
                break;
        }
    }

    IEnumerator LoadScene(){
        transitionAnim.SetTrigger("End");
        playerAnim.SetTrigger("Death"); 
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
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
