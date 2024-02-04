using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject obj;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public float spawnSpeed = 5f;

    private float timer;

    public GameObject player;

    public float margin = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Initial spawn
        timer = Random.Range(minSpawnTime, maxSpawnTime);
        SpawnObj(obj);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnObj(obj);
        }
    }

    private void SpawnObj(GameObject obj)
    {
        GameObject o = Instantiate(obj, GetRandomPositionAwayFromScreenEdge(margin), Quaternion.identity);
        Vector3 dir = (player.transform.position - o.transform.position).normalized;
        o.GetComponent<Rigidbody2D>().velocity = dir * spawnSpeed;
    }

    Vector2 GetRandomPositionAwayFromScreenEdge(float distanceFromEdge)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found.");
            return Vector2.zero;
        }

        float randomX = 0f;
        float randomY = 0f;

        // Randomly select one of the four edges
        int randomEdge = Random.Range(0, 4);

        // Get screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate random position based on the selected edge
        switch (randomEdge)
        {
            case 0: // Top edge
                randomX = Random.Range(distanceFromEdge, screenWidth - distanceFromEdge);
                randomY = screenHeight + distanceFromEdge;
                break;

            case 1: // Bottom edge
                randomX = Random.Range(distanceFromEdge, screenWidth - distanceFromEdge);
                randomY = -distanceFromEdge;
                break;

            case 2: // Left edge
                randomX = -distanceFromEdge;
                randomY = Random.Range(distanceFromEdge, screenHeight - distanceFromEdge);
                break;

            case 3: // Right edge
                randomX = screenWidth + distanceFromEdge;
                randomY = Random.Range(distanceFromEdge, screenHeight - distanceFromEdge);
                break;
        }

        // Convert screen position to world position
        Vector3 randomWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));

        // Ensure Z position is set to zero for 2D
        randomWorldPosition.z = 0f;

        // Return a Vector2
        return new Vector2(randomWorldPosition.x, randomWorldPosition.y);
    }

}
