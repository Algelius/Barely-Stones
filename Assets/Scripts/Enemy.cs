using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Tile CurrentTile { get; private set; } = null;

    private void Start()
    {
        SetCurrentTile();
    }

    void SetCurrentTile()
    {
        Tile[,] tiles = FindObjectOfType<TileGrid>().tileArray;

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] == null)
                {
                    continue;
                }

                if (transform.position.x == tiles[x, y].transform.position.x && transform.position.z == tiles[x, y].transform.position.z)
                {
                    CurrentTile = tiles[x, y];
                }
            }
        }
    }
}
