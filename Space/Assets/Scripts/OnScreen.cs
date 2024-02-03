using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreen : MonoBehaviour
{
    public float farThreshold = 2.0f;

    // Function to check if an object is really far outside the screen
    bool IsObjectReallyFarOutsideScreen(GameObject obj)
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

        // Calculate distance outside the screen for each axis
        float distanceOutsideX = Mathf.Max(0f, Mathf.Abs(viewportPoint.x - 0.5f) - 0.5f);
        float distanceOutsideY = Mathf.Max(0f, Mathf.Abs(viewportPoint.y - 0.5f) - 0.5f);

        // Calculate the overall distance outside the screen
        float totalDistanceOutside = Mathf.Sqrt(distanceOutsideX * distanceOutsideX + distanceOutsideY * distanceOutsideY);

        // Check if the object is really far outside the screen based on the threshold
        return totalDistanceOutside > farThreshold;
    }

    void Update()
    {
        // Example usage: Check if this GameObject is really far outside the screen
        if (IsObjectReallyFarOutsideScreen(gameObject))
        {
            Destroy(gameObject);
        }
    }
}
