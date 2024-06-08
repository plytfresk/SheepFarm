// Scripts/GrassTileManager.cs
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GrassTileManager : MonoBehaviour
{
    public Tilemap tilemap;
    private Dictionary<Vector3Int, GrassTileData> tileDataMap = new Dictionary<Vector3Int, GrassTileData>();

    void Start()
    {
        // Initialize the tile data for each grass tile in the tilemap
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = tilemap.GetTile(localPlace);

            if (tile is GrowableGrassTile grassTile)
            {
                tileDataMap[localPlace] = new GrassTileData
                {
                    currentStage = 0,
                    growthTimer = 0f,
                    grassTile = grassTile
                };
                Debug.Log($"GrassTile initialized at {localPlace}");
            }
        }
    }

    void Update()
    {
        foreach (var keyValue in tileDataMap)
        {
            Vector3Int pos = keyValue.Key;
            GrassTileData tileData = keyValue.Value;
            GrowableGrassTile grassTile = tileData.grassTile;

            tileData.growthTimer += Time.deltaTime;
            if (tileData.growthTimer >= grassTile.growthDuration && tileData.currentStage < grassTile.growthStages.Length - 1)
            {
                tileData.growthTimer = 0f;
                tileData.currentStage++;
                UpdateTileSprite(pos, tileData);
                //Debug.Log($"GrassTile at {pos} grew to stage {tileData.currentStage}");
            }
        }
    }

    private void UpdateTileSprite(Vector3Int pos, GrassTileData tileData)
    {
        GrowableGrassTile grassTile = tileData.grassTile;

        // Create a new tile instance to update its sprite
        GrowableGrassTile updatedTile = ScriptableObject.CreateInstance<GrowableGrassTile>();
        updatedTile.growthStages = grassTile.growthStages;
        updatedTile.growthDuration = grassTile.growthDuration;
        updatedTile.sprite = grassTile.growthStages[tileData.currentStage];

        // Update the tile in the tilemap
        tilemap.SetTile(pos, updatedTile);
        tilemap.RefreshTile(pos);

        //Debug.Log($"GrassTile sprite at {pos} updated to stage {tileData.currentStage}");
    }

    public void ResetTile(Vector3Int pos)
    {
        if (tileDataMap.TryGetValue(pos, out GrassTileData tileData))
        {
            tileData.currentStage = 0;
            tileData.growthTimer = 0f;
            UpdateTileSprite(pos, tileData);
            //Debug.Log($"GrassTile at {pos} was reset");
        }
    }

    private class GrassTileData
    {
        public int currentStage;
        public float growthTimer;
        public GrowableGrassTile grassTile;
    }
}