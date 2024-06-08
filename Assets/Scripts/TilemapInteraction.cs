// Scripts/TilemapInteraction.cs
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInteraction : MonoBehaviour
{
    public Tilemap tilemap;
    private GrassTileManager grassTileManager;

    void Start()
    {
        grassTileManager = FindObjectOfType<GrassTileManager>();
        if (grassTileManager == null)
        {
            //Debug.LogError("GrassTileManager not found in the scene.");
        }
    }

    void Update()
    {
        Vector3Int playerTilePos = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(playerTilePos);

        if (tile is GrowableGrassTile)
        {
            grassTileManager.ResetTile(playerTilePos);
            //Debug.Log($"Player stepped on GrassTile at {playerTilePos}");
        }
    }
}