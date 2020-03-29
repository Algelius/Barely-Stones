using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridScript : MonoBehaviour
{
    private GameObject[,] TileArray;//index i denna ska alltid vara deras verkliga position. Använd därför bara heltal som position på tiles


    //Jag tänker att denna klassen kan innehålla metoder för att selecta en area av tiles
    


    RaycastHit mouseRaycast;


    #region testbools
    public bool demoSelectMany = false;

    public bool highlightMouseOver = true;
    #endregion

    void Start()
    {

    }


    /// <summary>
    /// Reads all it's childgameobjects with tag Tile and stores them in an Array[][], if there are gaps between tiles, there will be gaps in the Array[][]
    /// </summary>
    public void Initialize()
    {
        TileArray = new GameObject[200, 200];//tänker att det är tillräckligt stor 200x200 tiles blir stort liksom?
        //Bygger för säkerhets skull in en funktion som gör arrayen större om det behövs.

        foreach (Transform child in transform)
        {
            if (child.tag == "Tile")
            {
                Vector2 childPos = new Vector2(child.position.x, child.position.z);
                if (childPos.x > TileArray.GetLength(0))
                {
                    ResizeTileArray();
                }
                TileArray[(int)childPos.x, (int)childPos.y] = child.gameObject;
                child.GetComponent<TileScript>().Activate();
            }
        }
    }

    private void ResizeTileArray()//skapa ny temporär längre array, läs över data, 
    {
        GameObject[,] tempTileArray = new GameObject[TileArray.GetLength(0) * 2, TileArray.GetLength(1) * 2];

        for (int i = 0; i < TileArray.GetLength(0); i++)
        {
            for (int j = 0; j < TileArray.GetLength(1); j++)
            {
                tempTileArray[i, j] = TileArray[i, j];
            }
        }

        TileArray = tempTileArray;
    }

    /// <summary>
    /// Find Tile on position pos
    /// </summary>
    /// <param name="pos"> pos.x corresponds to X coordinate, pos.y corresponds to the Z coordinate </param>
    /// <returns>Returns a Gameobject with tag = Tile. \n Returns null if no tile was found</returns>
    public GameObject GetTileAt(Vector2 pos)
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);

        if (pos.x >= 0 && pos.y >= 0 && pos.x < TileArray.GetLength(0) && pos.y < TileArray.GetLength(1))
        {
            return TileArray[(int)pos.x, (int)pos.y];
        }


        return null;
    }

    /// <summary>
    /// Finds the closest Tile to this position. 
    /// </summary>
    /// <param name="pos">pos.x corresponds to X coordinate, pos.y corresponds to the Z coordinate</param>
    /// <returns>If a tile exists this will return one</returns>
    public GameObject SnapToTile(Vector2 pos)
    {
        //FINNS PLATS FÖR SERIÖS OPTIMERING TROR JAG.
        //Just nu går den igenom alla tiles och jämför avstånd.
        GameObject returnTile = GetTileAt(pos);

        if (returnTile!=null)
        {
            return GetTileAt(pos);
        }

        float distanceToCurrent = 10f;
        float distanceToReturnTile = 10f;
        foreach(GameObject tile in TileArray)
        {
            if (tile != null)
            {
                if (returnTile == null)
                    returnTile = tile;

                distanceToCurrent = Vector2.Distance(pos, new Vector2(tile.transform.position.x, tile.transform.position.z));
                distanceToReturnTile = Vector2.Distance(pos, new Vector2(returnTile.transform.position.x, returnTile.transform.position.z));


                if (distanceToCurrent < distanceToReturnTile)
                    returnTile = tile;
            }
        }
        

        return returnTile;
    }

    /// <summary>
    /// Gets an adjacent tile to the given tile and in the given direction.
    /// </summary>
    /// <param name="tile">The Tile to which you want to find adjacent tiles</param>
    /// <param name="direction">legitimate values: 0,1,2,3. 0 indicates north, 1 indicates east, 2 indicates south, 3 indicates west </param>
    /// <returns> If no adjacent tile in the given direction exists it returns null </returns>
    public GameObject GetAdjacentTile(GameObject tile, int direction)
    {
        Vector2 tileNumber = tile.GetComponent<TileScript>().Gridposition;

        if (tileNumber.x-1 >= 0 && tileNumber.y-1 >= 0 && tileNumber.x+1 < TileArray.GetLength(0) && tileNumber.y+1 < TileArray.GetLength(1))
        {
            switch (direction)
            {
                case 0:
                    return TileArray[(int)tileNumber.x, (int)tileNumber.y - 1];
                case 1:
                    return TileArray[(int)tileNumber.x + 1, (int)tileNumber.y];
                case 2:
                    return TileArray[(int)tileNumber.x, (int)tileNumber.y + 1]; 
                default://allt över 2 pekar alltså på den till vänster om tilen
                    return TileArray[(int)tileNumber.x - 1, (int)tileNumber.y];
            }
        }

        return null;
    }

    

    void Update()
    {
        //Kanske temporärt ha en if(active) kolla med en raycast om den träffar nån tile, den tilen highlightas isåfall.

        #region raycasthit tiles
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseRay, out mouseRaycast, LayerMask.GetMask("TileLayer"));

        if (mouseRaycast.collider != null&&mouseRaycast.collider.tag == "Tile")
        {

            if(highlightMouseOver)
                mouseRaycast.collider.GetComponent<TileScript>().Highlight();

            //mest för demo kanske
            if (demoSelectMany)
            {
                for(int i= 0; i < 4; i++)//Mest för test
                {
                    if(GetAdjacentTile(mouseRaycast.collider.gameObject, i) != null)
                    {
                        GetAdjacentTile(mouseRaycast.collider.gameObject, i).GetComponent<TileScript>().Highlight();
                    }
                }
            }
        }
        
        #endregion



    }
}
