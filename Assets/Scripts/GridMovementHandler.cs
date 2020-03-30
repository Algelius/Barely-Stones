using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementHandler : MonoBehaviour
{
    TileGrid TileGrid;
    public Unit selectedUnit;

    void Awake()
    {
        TileGrid = FindObjectOfType<TileGrid>();
        TileGrid.Initialize();
        selectedUnit = FindObjectOfType<Unit>();
    }
}
