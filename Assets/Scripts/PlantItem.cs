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
        }
        else if (plant.plantName == "Bean") {
            gc.selectedPlant = TileType.BEANS;
        }
        else if (plant.plantName == "Rice") {
            gc.selectedPlant = TileType.RICE;
        }

    }
}

