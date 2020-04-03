using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Tile[,] tileArray { get; private set; }//index i denna ska alltid vara deras verkliga position. Använd därför bara heltal som position på tiles

    public Vector2Int arraySize = new Vector2Int(200, 200);//tänker att det är tillräckligt stor 200x200 tiles blir stort liksom?
    //Bygger för säkerhets skull in en funktion som gör arrayen större om det behövs.

    //Jag tänker att denna klassen kan innehålla metoder för att selecta en area av 

    /// <summary>
    /// Reads all it's childgameobjects with tag Tile and stores them in an Array[][], if there are gaps between tiles, there will be gaps in the Array[][]
    /// </summary>
    public void Initialize()
    {
        tileArray = new Tile[arraySize.x, arraySize.y];

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Tile>() != null)
            {
                Vector2 childPos = new Vector2(child.position.x, child.position.z);
                if (childPos.x > tileArray.GetLength(0))
                {
                    ResizeTileArray();
                }
                tileArray[(int)childPos.x, (int)childPos.y] = child.GetComponent<Tile>();
                child.GetComponent<Tile>().Activate();
            }
        }
    }

    private void ResizeTileArray()//skapa ny temporär längre array, läs över data, 
    {
        Tile[,] tempTileArray = new Tile[tileArray.GetLength(0) * 2, tileArray.GetLength(1) * 2];

        for (int i = 0; i < tileArray.GetLength(0); i++)
        {
            for (int j = 0; j < tileArray.GetLength(1); j++)
            {
                tempTileArray[i, j] = tileArray[i, j];
            }
        }

        tileArray = tempTileArray;
    }

    public Tile GetTileAt(Vector2 pos)
    {
        return null;
    }

    public GameObject SnapToTile(Vector2 pos)
    {
        return null;
    }

    public Tile GetAdjacentTile(Tile tile, int direction)
    {
        Vector2 tileNumber = tile.gridPosition;

        if (tileNumber.x - 1 >= 0 && tileNumber.y - 1 >= 0 && tileNumber.x + 1 < tileArray.GetLength(0) && tileNumber.y + 1 < tileArray.GetLength(1))
        {
            switch (direction)
            {
                case 0:
                    return tileArray[(int)tileNumber.x, (int)tileNumber.y - 1];
                case 1:
                    return tileArray[(int)tileNumber.x + 1, (int)tileNumber.y];
                case 2:
                    return tileArray[(int)tileNumber.x, (int)tileNumber.y + 1];
                default://allt över 2 pekar alltså på den till vänster om tilen
                    return tileArray[(int)tileNumber.x - 1, (int)tileNumber.y];
            }
        }

        return null;
    }

    public Tile[] GetAdjacentTiles(Tile tile)
    {
        Tile[] adjacentTiles = new Tile[4];

        for (int i = 0; i < 4; i++)
        {
            adjacentTiles[i] = GetAdjacentTile(tile, i);
        }

        return adjacentTiles;
    }
}
