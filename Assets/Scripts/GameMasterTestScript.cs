using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterTestScript : MonoBehaviour
{


    GameObject TileGrid;

    void Start()
    {
        TileGrid = GameObject.FindGameObjectWithTag("TileGrid");
    }

    bool done = false;

    void Update()
    {
        if (!done)
        {
            TileGrid.GetComponent<TileGridScript>().Initialize();
            done = true;
        }
    }
}
