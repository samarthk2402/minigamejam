using UnityEngine;

public class Float : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float rotationSpeed = 50.0f;
    public float floatRange = 1.0f;

    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position for reference
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the object in a sinusoidal pattern
        float verticalMovement = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = new Vector3(startPosition.x, startPosition.y + verticalMovement, startPosition.z);

        // Rotate the object
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
