// Scripts/Sheep.cs
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sheep : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float hunger = 0f;
    public float hungerIncreaseRate = 1f;
    public float hungerBonus = 10f;
    public Tilemap tilemap;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Move the sheep towards the target position
        if ((transform.position - targetPosition).magnitude < 1f)
        {
            // Pick a new target position
            targetPosition = GetNewRandomPosition();
            Debug.Log($"Sheep moving towards {targetPosition}");
        }
        else
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        // Increase hunger over time
        hunger += hungerIncreaseRate * Time.deltaTime;

        // Check if the sheep is on a grass tile
        Vector3Int sheepTilePos = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(sheepTilePos);

        if (tile is GrowableGrassTile)
        {
            GrassTileManager grassTileManager = FindObjectOfType<GrassTileManager>();
            if (grassTileManager != null)
            {
                grassTileManager.ResetTile(sheepTilePos);
                hunger += hungerBonus;
                Debug.Log($"Sheep ate grass at {sheepTilePos}, hunger is now {hunger}");
            }
        }
    }

    Vector3 GetNewRandomPosition()
    {
        // Generate a new random position within some bounds
        float newX = Random.Range(13f, 20f);
        float newY = Random.Range(-1f, -10f);
        return new Vector3(newX, newY, transform.position.z);
    }
}