using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public TileGrid currentTileGrid;

    public List<Tile> FindPath(Tile startTile, Tile goalTile)
    {
        List<Tile> openList = new List<Tile>();
        HashSet<Tile> closedList = new HashSet<Tile>();

        openList.Add(startTile);

        while (openList.Count > 0)
        {
            Tile currentTile = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].myFCost <= currentTile.myFCost && openList[i].myHCost < currentTile.myHCost)
                {
                    currentTile = openList[i];
                }
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile == goalTile)
            {
                return GetFinalPath(startTile, goalTile);
            }

            foreach (Tile neighbourTile in FindTileNeighbours(currentTile))
            {
                if (neighbourTile == null || closedList.Contains(neighbourTile))
                {
                    continue;
                }

                int moveCost = currentTile.myGCost + GetManhattanDistance(currentTile, neighbourTile);

                if (moveCost < neighbourTile.myGCost || !openList.Contains(neighbourTile))
                {
                    neighbourTile.myGCost = moveCost;
                    neighbourTile.myHCost = GetManhattanDistance(neighbourTile, goalTile);
                    neighbourTile.myParentTile = currentTile;

                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }

        return null;
    }

    List<Tile> GetFinalPath(Tile aStartTile, Tile aEndTile)
    {
        List<Tile> finalPath = new List<Tile>();
        Tile currentTile = aEndTile;

        while (currentTile != aStartTile)
        {
            finalPath.Add(currentTile);
            currentTile = currentTile.myParentTile;
        }

        finalPath.Reverse();

        return finalPath;
    }

    int GetManhattanDistance(Tile aTileA, Tile aTileB)
    {
        int iX = Mathf.Abs(aTileA.gridPosition.x - aTileB.gridPosition.x);
        int iY = Mathf.Abs(aTileA.gridPosition.y - aTileB.gridPosition.y);

        return iX + iY;
    }

    List<Tile> FindTileNeighbours(Tile aSourceTile)
    {
        List<Tile> neighbourList = new List<Tile>();
        Tile sourceTile = aSourceTile;

        //Above
        if (sourceTile.gridPosition.y > 0)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.gridPosition.x, sourceTile.gridPosition.y - 1]);
        }
        //Below
        if (sourceTile.gridPosition.y < currentTileGrid.arraySize.y - 1)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.gridPosition.x, sourceTile.gridPosition.y + 1]);
        }
        //Left
        if (sourceTile.gridPosition.x > 0)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.gridPosition.x - 1, sourceTile.gridPosition.y]);
        }
        //Right
        if (sourceTile.gridPosition.x < currentTileGrid.arraySize.x - 1)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.gridPosition.x + 1, sourceTile.gridPosition.y]);
        }

        return neighbourList;
    }
}