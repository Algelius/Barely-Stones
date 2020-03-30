using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Tile currentTile = null;
    public Pathfinding myPathfinder;
    public List<Tile> myPath;

    protected Transform targetTile;
    protected int currentNode = 0;

    private void Start()
    {
        GetCurrentTile();
    }

    private void Update()
    {
        if (targetTile == null)
        {
            if (myPath != null) GetNextNode();

            if (targetTile == null) return;
        }

        Vector3 direction = targetTile.position - transform.position;
        float distPerFrame = Time.deltaTime;

        if (direction.magnitude <= distPerFrame)
        {
            targetTile = null;
        }

        else
        {
            transform.Translate(direction.normalized * distPerFrame, Space.World);
        }
    }

    void GetCurrentTile()
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
                    currentTile = tiles[x, y];
                }
            }
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
            // Is this executing???
            Debug.Log("Is this executing???");
            GetCurrentTile();
            Debug.Log(currentTile.gridPosition);
        }
    }

    public void FindPath(Tile goalTile)
    {
        myPath = myPathfinder.FindPath(currentTile, goalTile);
    }
}
