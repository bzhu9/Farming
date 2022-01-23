using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    public Sprite[] stages;
    public TileType type;
    public float secondsPerStage = 2;
    private int currentStage = 0;
    private float secondsLeft;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stages[currentStage];

        UpdateGrowthRate();
        secondsLeft = secondsPerStage;
    }

    void UpdateGrowthRate()
    {
        TileController tc = GetComponentInParent<TileController>();
        TileController.TileAttributes ta = tc.currAttributes;
        if (type == TileType.RICE)
        {
            secondsPerStage = 10;
            secondsPerStage -= Mathf.Lerp(0, 3, ta.water / 5f);
        }
        else if (type == TileType.CORN)
        {
            secondsPerStage = 2;
            secondsPerStage -= Mathf.Lerp(0, 1, ta.nitrogen / 3f);
        }
        else if (type == TileType.BEANS)
        {
            secondsPerStage = 5;
            secondsPerStage -= Mathf.Lerp(0, 2, ta.shade / 3f);
        }

        secondsLeft = Mathf.Min(secondsLeft, secondsPerStage);
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
