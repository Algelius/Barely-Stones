using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//KOPIERASTULET FRÅN:https://www.alanzucconi.com/2015/07/22/how-to-snap-to-grid-in-unity3d/
//Executar bara i editorn, lägg dessa på tiles så kan de 

[ExecuteInEditMode]
public class SnapToGridScript : MonoBehaviour
{
#if UNITY_EDITOR
    public bool snapToGrid = true;
    public float snapValue = 0.5f;

    public bool sizeToGrid = false;
    public float sizeValue = 0.25f;

    // Adjust size and position
    void Update()
    {
        if (snapToGrid)
            transform.position = RoundTransform(transform.position, snapValue);

        if (sizeToGrid)
            transform.localScale = RoundTransform(transform.localScale, sizeValue);
    }

    // The snapping code
    private Vector3 RoundTransform(Vector3 v, float snapValue)
    {
        return new Vector3
        (
        snapValue * Mathf.Round(v.x / snapValue),
        v.y,
        snapValue * Mathf.Round(v.z / snapValue)
        );
    }
#endif
}

