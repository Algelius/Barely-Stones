using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileScript : MonoBehaviour
{
    //Ska veta vilken GameEntity som står ovanpå den just nu, GameEntityn ska samtidigt veta vilken ruta den står på.
    //Kanske ha att dennas collider känner av gameEntityns collider och säger åt båda.


    //Om vi implementerar en GameMaster så tänker jag att den borde uppdatera denna så att den då läser in GameEntitys som kolliderar med den eller är ovanför den.

    //Ha en isHighlighted bool, så om man har musen över tex lyser den lite ljusare.
    //Känna av om man klickar på den och säga till sin GridOverlord.

    //eller om aktiv känna av om man musar över den.


    MeshRenderer normalRenderer;
    MeshRenderer highlightedRenderer;
    
    bool highlightedLastUpdate = false;
    bool highlighted = false;

    bool isActive = true;
    
    //förälders tilescript
    public TileGridScript grid;
    //används för att veta sin position i TileGrids rutnät. borde än så länge bara vara dess position relativt till tilegrid.
    public Vector2 Gridposition;

    void Start()
    {
        grid = transform.GetComponentInParent<TileGridScript>();
        normalRenderer = GetComponent<MeshRenderer>();
        highlightedRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        Activate();


        //FULGREJ, borde egentligen få gridposition av TileGrid, då vi kanske implementerar
        //en gridförskjutning så att grid kan finnas på mindre koordinater än 0,0
        Gridposition = new Vector2(transform.position.x,transform.position.z);
    }


    public void Activate()
    {
        normalRenderer.enabled = true;
        highlightedRenderer.enabled = false;
        isActive = true;
    }
    public void Inactivate()
    {
        normalRenderer.enabled = false;
        highlightedRenderer.enabled = false;
        isActive = false;
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
