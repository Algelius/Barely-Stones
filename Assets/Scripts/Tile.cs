using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Ska veta vilken GameEntity som står ovanpå den just nu, GameEntityn ska samtidigt veta vilken ruta den står på.
    //Kanske ha att dennas collider känner av gameEntityns collider och säger åt båda.


    //Om vi implementerar en GameMaster så tänker jag att den borde uppdatera denna så att den då läser in GameEntitys som kolliderar med den eller är ovanför den.

    //Ha en isHighlighted bool, så om man har musen över tex lyser den lite ljusare.
    //Känna av om man klickar på den och säga till sin GridOverlord.

    //eller om aktiv känna av om man musar över den.

    [SerializeField] MeshRenderer normalRenderer;
    [SerializeField] MeshRenderer highlightedRenderer;

    public Tile myParentTile = null;
    public int myGCost = 0;
    public int myHCost = 0;
    public int myFCost { get { return myGCost + myHCost; } }

    bool highlightedLastUpdate = false;
    bool highlighted = false;

    bool isActive = true;

    //förälders tilescript
    public TileGrid grid;
    //används för att veta sin position i TileGrids rutnät. borde än så länge bara vara dess position relativt till tilegrid.
    public Vector2Int gridPosition;

    void Start()
    {
        grid = transform.GetComponentInParent<TileGrid>();

        Activate();


        //FULGREJ, borde egentligen få gridposition av TileGrid, då vi kanske implementerar
        //en gridförskjutning så att grid kan finnas på mindre koordinater än 0,0
        gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }

    public void SetColor(Color aColor)
    {
        normalRenderer.material.color = aColor;
    }

    public void Activate()
    {
        normalRenderer.enabled = true;
        highlightedRenderer.enabled = false;
        isActive = true;

        Debug.Log("Tile Activated");
    }

    public void Inactivate()
    {
        normalRenderer.enabled = false;
        highlightedRenderer.enabled = false;
        isActive = false;

        Debug.Log("Tile Inactivated");
    }

    public void Highlight()//kalla varje cykel som du vill att den ska vara highlightad.
    {
        highlighted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (highlighted)
            {
                highlightedRenderer.enabled = true;
                normalRenderer.enabled = false;

                Debug.Log("Highlighted tile");
            }

            else if (!highlighted && !highlightedLastUpdate)
            {
                highlightedRenderer.enabled = false;
                normalRenderer.enabled = true;
            }

            highlightedLastUpdate = highlighted;//jag vet inte vilken update som händer först därför gör jag såhär för att garantera att den är highlightad minst en update-cykel
            highlighted = false;
        }
    }
}
