using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    public Text nameText;
    public Text priceText;
    public Image icon;
    private GameController gc;

    public Texture2D cursorTexture;
    public Texture2D baseTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.one;



    // Start is called before the first frame update
    void Start()
    {
        nameText.text = plant.plantName;
        priceText.text = plant.price.ToString("c2");
        icon.sprite = plant.icon;
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    public void buyPlant()
    {
        Debug.Log("Bought " + plant.plantName);

        if (plant.plantName == "Corn") {
            if (gc.cursorState == CursorType.CORN)
            {
                Debug.Log("ALSKDJHFAJKSDGFGJKADSJKGADSFASJG");
                gc.selectedPlant = TileType.EMPTY;
                gc.cursorState = CursorType.EMPTY;
                cursorTexture = null;
                cursorMode = CursorMode.Auto;
                hotSpot = Vector2.one;
            }
            else
            {
                gc.selectedPlant = TileType.CORN;
                gc.cursorState = CursorType.CORN;
                Texture2D newCursor = baseTexture;
                hotSpot = new Vector2(24, 24);
                cursorTexture = newCursor;
            }
        }
        else if (plant.plantName == "Beans") {
            if (gc.cursorState == CursorType.BEANS)
            {
                gc.selectedPlant = TileType.EMPTY;
                gc.cursorState = CursorType.EMPTY;
                cursorTexture = null;
                cursorMode = CursorMode.Auto;
                hotSpot = Vector2.one;
            }
            else
            {
                gc.selectedPlant = TileType.BEANS;
                gc.cursorState = CursorType.BEANS;
                Texture2D newCursor = baseTexture;
                hotSpot = new Vector2(24, 24);
                cursorTexture = newCursor;
            }
        }
        else if (plant.plantName == "Rice") {
            if (gc.cursorState == CursorType.RICE)
            {
                gc.selectedPlant = TileType.EMPTY;
                gc.cursorState = CursorType.EMPTY;
                cursorTexture = null;
                cursorMode = CursorMode.Auto;
                hotSpot = Vector2.one;
            }
            else
            {
                gc.selectedPlant = TileType.RICE;
                gc.cursorState = CursorType.RICE;
                Texture2D newCursor = baseTexture;
                hotSpot = new Vector2(24, 24);
                cursorTexture = newCursor;
            }
        }

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

    }
}

