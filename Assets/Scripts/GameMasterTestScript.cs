using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterTestScript : MonoBehaviour
{


    GameObject TileGrid;
    TileGridScript GridScript;

    void Start()
    {
        TileGrid = GameObject.FindGameObjectWithTag("TileGrid");
        GridScript = TileGrid.GetComponent<TileGridScript>();
    }

    bool done = false;

    void Update()
    {
        if (!done)
        {
            GridScript.Initialize();
            done = true;
        }

        GridScript.SnapToTile(new Vector2(transform.position.x, transform.position.z)).GetComponent<TileScript>().Highlight();

    }
}
