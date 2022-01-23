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
            gc.selectedPlant = TileType.CORN;
            Texture2D newCursor = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/cursors/corn_cursor.png");
            hotSpot = new Vector2(24, 24);
            cursorTexture = newCursor;
        }
        else if (plant.plantName == "Beans") {
            gc.selectedPlant = TileType.BEANS;
            Texture2D newCursor = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/cursors/beans_cursor.png");
            hotSpot = new Vector2(24, 24);
            cursorTexture = newCursor;
        }
        else if (plant.plantName == "Rice") {
            gc.selectedPlant = TileType.RICE;
            Texture2D newCursor = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/cursors/rice_cursor.png");
            hotSpot = new Vector2(24, 24);
            cursorTexture = newCursor;
        }

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

    }
}

