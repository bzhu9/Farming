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

[System.Serializable]
public enum CursorType
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
    public SpriteRenderer shadow;
    public SpriteRenderer water;
    public RectTransform nitrogenBar;

    [HideInInspector]
    public CropController crop;
    private SpriteRenderer spriteRenderer;
    private GameController gc;

    public struct TileAttributes
    {
        public int shade;  // corn shades neighboring beans
        public int nitrogen;  // corn lower nitrogen (are affected), beans increase nitrogen (not affected)
        public int water;  // for each rice in a group, water increases (up to 5)
    }

    public TileAttributes baseAttributes;
    public TileAttributes currAttributes;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        baseAttributes.nitrogen = 0;
        baseAttributes.shade = 0;
        baseAttributes.water = 0;
    }


    private void UpdateTileAttributes()
    {
        currAttributes = baseAttributes;

        foreach ((int, int) neighbor in Utils.GetNeighbors(row, col))
        {
            TileController t = gc.tiles[neighbor.Item1, neighbor.Item2];
            if (t.GetTileType() == TileType.CORN)
            {
                currAttributes.shade += 1;
                currAttributes.shade = Math.Max(currAttributes.shade, 0);
            }
        }

        Color shadowColor = shadow.color;
        shadowColor.a = currAttributes.shade / 10f;
        shadow.color = shadowColor;


        Color waterColor = water.color;
        waterColor.a = currAttributes.water / 10f;
        water.color = waterColor;

        Vector2 size = nitrogenBar.sizeDelta;
        size.y = (currAttributes.nitrogen / 3f) * 0.65f;
        nitrogenBar.sizeDelta = size;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTileAttributes();
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
                if (crop.type == TileType.CORN)
                {
                    baseAttributes.nitrogen = Mathf.Clamp(baseAttributes.nitrogen - 1, 0, 3);
                }
                if (crop.type == TileType.BEANS)
                {
                    baseAttributes.nitrogen = Mathf.Clamp(baseAttributes.nitrogen + 1, 0, 3);
                }

                gc.SellCrop(crop.type);
                Destroy(crop.gameObject);
                Instantiate(sellFx, transform.position, Quaternion.identity);
                crop = null;
                gc.UpdateGroupedRice();
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
