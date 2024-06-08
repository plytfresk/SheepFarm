using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Growable Grass Tile", menuName = "Tiles/GrowableGrassTile")]
public class GrowableGrassTile : Tile
{
    public Sprite[] growthStages; // Array of sprites for different growth stages
    public float growthDuration = 2f; // Duration for each growth stage in seconds

    // Override GetTileData to customize the tile
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(location, tilemap, ref tileData);
        //tileData.sprite = growthStages[0]; // Initially set to the first sprite
    }
}