# Barely Stones

Barely Stones is a single-player Tactical RPG being developed in Unity.
It'll be awesome, guyse.







OSSIANS REGION:
kanske, vi kanske inte borde ha sån här info i readmen?

Har lagt till en folder i Scenes som heter workscenes där alla kan skapa sin egen scen att arbeta i.

SnapToGridScript:
  Fulkopierade. Det executas bara i editorn och passar bra att sätta på allt som ska snappa till gridet.
  Den gridstorlek jag använder i detta fallet är 1.

TileGrid:
  innehåller alla Tile-GameObjects
  TileGridScript:
    Intialize, läser in alla tiles, sparar i en array[][]. Om det inte finns en tile på ett ställe är arraystället där null.

    SnapToGrid(Vector3 position): om ingen tile, hitta närmaste


    GetTileAt(Position): om ingen tile return null


GameObjects med tag-gameEntity upptäcks av tile tänker jag.


Eftersom det ska vara turnbased tänker jag att man kan strukturera det som så att:
    Det finns ett gameobject som kallas gameMaster eller något sådant som går igenom alla gameEntitys som ska
    agera under rundan och kallar deras typ YourTurn_Metod. och sedan när alla har gåtts igenom, ser den om motståndarna är döda eller om spelaren är död och agerar därefter, eller om det är dags för en ny runda.
