using UnityEngine;

public class SingleScreenShatterSpawner : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private Transform bottomLeft;
    [SerializeField] private Transform topRight;
    private Vector2 validSpawnLocationX;
    private Vector2 validSpawnLocationY;
    [SerializeField] private Vector2 validWidth;
    private float lenght;

    [Header("References")]
    [SerializeField] private GameObject screenShatterPrefab;

    private void Start()
    {
        validSpawnLocationX.x = bottomLeft.position.x;
        validSpawnLocationX.y = topRight.position.x;

        validSpawnLocationY.x = bottomLeft.position.y;
        validSpawnLocationY.y = topRight.position.y;

        float sizeX = validSpawnLocationX.x - validSpawnLocationX.y;
        float sizeY = validSpawnLocationY.x - validSpawnLocationY.y;

        lenght = (Mathf.Sqrt(Mathf.Pow(sizeX, 2) + Mathf.Pow(sizeY, 2)) + 50) * 2;
    }

    public void SpawnSingleScreenShatter()
    {
        // Random spawn location in valid spawn locations
        Vector2 spawnLocation = new Vector2(Random.Range(validSpawnLocationX.x, validSpawnLocationX.y), Random.Range(validSpawnLocationY.x, validSpawnLocationY.y));

        GameObject screenShatter = Instantiate(screenShatterPrefab, spawnLocation, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        screenShatter.transform.localScale = new Vector3(lenght, Random.Range(validWidth.x, validWidth.y), 0);
    }
}
