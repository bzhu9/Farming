using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    public Sprite[] stages;
    public TileType type;
    public int secondsPerStage = 2;
    private int currentStage = 0;
    private float secondsLeft;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stages[currentStage];
        secondsLeft = secondsPerStage;
        UpdateTileBaseAttributes();
    }

    void UpdateTileBaseAttributes()
    {
        TileController tc = GetComponentInParent<TileController>();
        // TODO
    }

    void UpdateGrowthRate()
    {
        TileController tc = GetComponentInParent<TileController>();
        TileController.TileAttributes ta = tc.GetTileAttributes();
        if (type == TileType.RICE)
        {
            if (ta.groupedRice >= 5)
            {
                secondsPerStage = 1;
                secondsLeft = Mathf.Min(secondsLeft, secondsPerStage);
            }
        }
        else if (type == TileType.CORN)
        {
            if (ta.nitrogen > 10)
            {
                secondsPerStage = 1;
                secondsLeft = Mathf.Min(secondsLeft, secondsPerStage);
            }
        }
    }

    void Update()
    {
        UpdateGrowthRate();

        if (secondsLeft > 0)
        {
            secondsLeft -= Time.deltaTime;
        }
        else if (currentStage < stages.Length - 1)
        {
            IncrementStage();
            secondsLeft = secondsPerStage;
        }
    }

    void IncrementStage()
    {
        if (currentStage < stages.Length - 1)
        {
            currentStage++;
            spriteRenderer.sprite = stages[currentStage];
        }
    }

    public bool isHarvestable()
    {
        return currentStage == stages.Length - 1;
    }
}
