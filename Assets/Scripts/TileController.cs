using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public enum TileType
{
    EMPTY,
    RICE,
    CORN,
    BEANS
}


public class TileController : MonoBehaviour
{
    [System.Serializable]
    public struct Crop
    {
        public TileType type;
        public GameObject gameObject;
    }

    public Crop[] crops;
    public GameObject sellFx;
    public int row;
    public int col;

    [HideInInspector]
    public CropController crop;
    private SpriteRenderer spriteRenderer;
    private GameController gc;

    public struct TileAttributes
    {
        public int temperature;  // corn shades neighboring beans
        public int nitrogen;  // corn lower nitrogen (are affected), beans increase nitrogen (not affected)
        public int groupedRice;  // for each rice next to it, the water is increased
    }

    public TileAttributes baseAttributes;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        baseAttributes.nitrogen = 10;
        baseAttributes.temperature = 10;
        baseAttributes.groupedRice = 0;
    }


    public TileAttributes GetTileAttributes()
    {
        TileAttributes ta = baseAttributes;

        foreach ((int, int) neighbor in Utils.GetNeighbors(row, col))
        {
            TileController t = gc.tiles[neighbor.Item1, neighbor.Item2];
            if (t.GetTileType() == TileType.CORN)
            {
                ta.temperature -= 1;
                ta.temperature = Math.Max(ta.temperature, 0);
            }
        }

        return ta;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        spriteRenderer.color = new Color(0.32f, 0.30f, 0.71f, 0.75f);
    }

    void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    public TileType GetTileType()
    {
        return crop ? crop.type : TileType.EMPTY;
    }

    void OnMouseDown()
    {
        Debug.Log("mousedown");
        if (crop)
        {
            Debug.Log("crop exists");
            if (crop.isHarvestable())
            {
                gc.SellCrop(crop.type);
                Destroy(crop.gameObject);
                Instantiate(sellFx, transform.position, Quaternion.identity);
                crop = null;
            }

            return;
        }

        GameObject cropObject = null;
        foreach (Crop crop in crops)
        {
            if (crop.type == gc.selectedPlant)
            {
                if (gc.BuyCrop(crop.type))
                {
                    cropObject = crop.gameObject;
                }
                break;
            }
        }

        if (!cropObject)
        {
            Debug.Log("No crop object found");
            return;
        }

        GameObject co = Instantiate(cropObject, transform.position, Quaternion.identity);
        co.transform.SetParent(this.transform);
        crop = co.GetComponent<CropController>();

        gc.UpdateGroupedRice();
    }
}
