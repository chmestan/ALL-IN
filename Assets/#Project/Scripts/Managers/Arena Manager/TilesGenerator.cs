using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] groundTiles;
    [SerializeField] private Tile[] holeTiles;
    [SerializeField] private Vector2Int size = new Vector2Int(28, 14);
    [SerializeField] private int numberOfHoles = 3;
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

        for (int i = 0; i < numberOfHoles; i++)
        {
            CreateHole();
        }

    }

    private void CreateHole()
    {
        int x = Random.Range(1, size.x - 1);
        int y = Random.Range(1, size.y - 1);

        Vector3Int holePosition = new Vector3Int(x + offset.x, y + offset.y, 0);

        Tile holeTile = holeTiles[Random.Range(0, holeTiles.Length)];
        tilemap.SetTile(holePosition, holeTile);

        if (debug) Debug.Log($"[TilesGenerator] Creating a hole at position ({holePosition.x}, {holePosition.y})");
    }
}
