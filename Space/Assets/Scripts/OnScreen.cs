using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreen : MonoBehaviour
{
    bool IsObjectOnScreen(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("Object is null.");
            return false;
        }

        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found.");
            return false;
        }

        // Get the object's position in viewport coordinates
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(obj.transform.position);

        // Check if the object is on screen (within [0, 1] for both x and y axes)
        bool isOnScreen = viewportPoint.x >= 0f && viewportPoint.x <= 1f && viewportPoint.y >= 0f && viewportPoint.y <= 1f;

        return isOnScreen;
    }  
    
    void Update(){
        if(!IsObjectOnScreen(this.gameObject)){
            Destroy(this.gameObject);
        }
    }
}
