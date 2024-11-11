using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] groundTiles;
    [SerializeField] private Tile neutralTile;
    [SerializeField] private Vector2Int size = new Vector2Int(28, 14);
    [SerializeField] private int numberOfObstacles = 3;
    [SerializeField] private bool debug = false;

    private Vector2Int offset;

    private void Start()
    {
        offset = new Vector2Int(-size.x / 2, -size.y / 2);
        GenerateArena();
    }

    private void GenerateArena()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Tile randomTile = groundTiles[Random.Range(0, groundTiles.Length)];
                Vector3Int position = new Vector3Int(x + offset.x, y + offset.y, 0);
                tilemap.SetTile(position, randomTile);
            }
        }

        for (int i = 0; i < numberOfObstacles; i++)
        {
            CreateObstacles();
        }

    }

    private void CreateObstacles()
    {
        int x = Random.Range(1, size.x - 1);
        int y = Random.Range(1, size.y - 1);

        Vector3Int obstaclePos = new Vector3Int(x + offset.x, y + offset.y, 0);

        tilemap.SetTile(obstaclePos, neutralTile);

        GameObject pit = PitPool.SharedInstance.GetPooledObject();
        if (pit != null)
        {
            pit.transform.position = obstaclePos + tilemap.cellSize/2;
            pit.SetActive(true);
        }

        if (debug) Debug.Log($"[TilesGenerator] Creating obstacle at position ({obstaclePos.x}, {obstaclePos.y})");
    }
}
