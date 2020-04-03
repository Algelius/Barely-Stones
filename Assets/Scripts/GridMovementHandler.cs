using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementHandler : MonoBehaviour
{
    RaycastHit mouseRaycast;

    Pathfinding pathfinding;
    TileGrid tileGrid;
    Unit selectedUnit;

    public Material unitMat;
    public Material unitHighlightMat;

    Unit[] unitsToMove;
    Enemy[] enemies;

    bool isAttacking = false;
    List<Tile> enemyTiles;

    void Awake()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        tileGrid = FindObjectOfType<TileGrid>();
        tileGrid.Initialize();
        enemies = FindObjectsOfType<Enemy>();
        NextTurn();
    }

    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseRay, out mouseRaycast, LayerMask.GetMask("TileLayer"));

        if (isAttacking)
        {
            for (int i = 0; i < enemyTiles.Count; i++)
            {
                enemyTiles[i].Highlight();
            }

            if (mouseRaycast.collider != null && mouseRaycast.collider.GetComponent<Tile>() != null)
            {
                Tile tileHit = mouseRaycast.collider.GetComponent<Tile>();

                if (Input.GetMouseButtonDown(0) && selectedUnit != null)
                {
                    for (int j = 0; j < enemies.Length; j++)
                    {
                        if (enemies[j].CurrentTile == tileHit)
                        {
                            Debug.Log(string.Format("Attacked {0}", enemies[j].name));
                        }
                    }

                    isAttacking = false;
                }
            }
        }

        else
        {
            if (mouseRaycast.collider != null && mouseRaycast.collider.GetComponent<Tile>() != null)
            {
                Tile tileHit = mouseRaycast.collider.GetComponent<Tile>();

                tileHit.Highlight();

                if (Input.GetMouseButtonDown(1) && selectedUnit != null)
                {
                    for (int i = 0; i < unitsToMove.Length; i++)
                    {
                        if (selectedUnit == unitsToMove[i])
                        {
                            unitsToMove[i] = null;
                            selectedUnit.FindPath(tileHit, pathfinding);
                        }
                    }
                }
            }

            if (mouseRaycast.collider != null && mouseRaycast.collider.GetComponent<Unit>() != null)
            {
                Unit unitHit = mouseRaycast.collider.GetComponent<Unit>();

                if (Input.GetMouseButtonDown(0))
                {
                    if (selectedUnit != null)
                    {
                        selectedUnit.GetComponentInChildren<Renderer>().material = unitMat;
                    }

                    selectedUnit = unitHit;
                    selectedUnit.GetComponentInChildren<Renderer>().material = unitHighlightMat;
                }
            }
        }
    }

    public void NextTurn()
    {
        unitsToMove = FindObjectsOfType<Unit>();
    }

    public void Attack()
    {
        enemyTiles = new List<Tile>();

        Tile[] adjacentTiles = tileGrid.GetAdjacentTiles(selectedUnit.CurrentTile);

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = 0; j < adjacentTiles.Length; j++)
            {
                if (enemies[i].CurrentTile == adjacentTiles[j])
                {
                    enemyTiles.Add(enemies[i].CurrentTile);
                }
            }
        }

        if (enemyTiles.Count > 0)
        {
            isAttacking = true;
        }
    }
}
