using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Tile CurrentTile { get; private set; } = null;
    public List<Tile> myPath = null;

    protected Transform targetTile;
    protected int currentNode = 0;
    float moveSpeed = 3;

    private void Start()
    {
        SetCurrentTile();
    }

    private void Update()
    {
        if (myPath == null || myPath.Count == 0)
        {
            return;
        }

        if (targetTile == null)
        {
            GetNextNode();

            if (targetTile == null)
            {
                return;
            }
        }

        Vector3 direction = targetTile.position - transform.position;
        float distPerFrame = Time.deltaTime * moveSpeed;

        if (direction.magnitude <= distPerFrame)
        {
            targetTile = null;
        }

        else
        {
            transform.Translate(direction.normalized * distPerFrame, Space.World);
        }
    }

    void GetNextNode()
    {
        if (currentNode < myPath.Count)
        {
            targetTile = myPath[currentNode].transform;
            currentNode++;
        }

        else
        {
            targetTile = null;
            transform.position = myPath[myPath.Count - 1].transform.position;
            myPath = null;
            currentNode = 0;
            SetCurrentTile();
        }
    }

    public void FindPath(Tile goalTile, Pathfinding pathfinding)
    {
        myPath = pathfinding.FindPath(CurrentTile, goalTile);
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
