using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    // prefab for the game tile
    public GameObject gameTilePrefab;
    public GameObject bgTilePrefab;
    public const int gridSize = 5;

    private float money = 500;
    public Text moneyText;
    public Text cornPriceText;
    public Text beanPriceText;
    public Text ricePriceText;

    //seeds
    public Text cornSeedPriceText;
    public Text beanSeedPriceText;
    public Text riceSeedPriceText;

    public TileType selectedPlant = TileType.RICE;

    private Color newRed = new Color(1, 0.3f, 0.3f);
    private Color newGreen = new Color(0.6f, 1, 0.6f);

    private Dictionary<TileType, float> prices = new Dictionary<TileType, float>()
    {
        { TileType.RICE, 100 },
        { TileType.CORN, 100 },
        { TileType.BEANS, 100 }
    };

    private Dictionary<TileType, float> seedPrices = new Dictionary<TileType, float>()
    {
        { TileType.RICE, 100 },
        { TileType.CORN, 100 },
        { TileType.BEANS, 100 }
    };
    public TileController[,] tiles = new TileController[gridSize, gridSize];

    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    void Start()
    {
        // get width of prefab
        float size = gameTilePrefab.GetComponent<Renderer>().bounds.size.x;

        float startX = -(gridSize / 2) * size;
        float startY = -(gridSize / 2) * size;

        // instantiate 5 prefab objects
        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                // create a new game object
                GameObject gameTile = Instantiate(gameTilePrefab) as GameObject;

                // set the position of the new game object
                gameTile.transform.position = new Vector3(startX + r * size, startY + c * size, 0);
                tiles[r, c] = gameTile.GetComponent<TileController>();
                tiles[r, c].row = r;
                tiles[r, c].col = c;
            }
        }

        // instantiate background tiles
        for (int r = -10; r < 10; r++)
        {
            for (int c = -10; c < 10; c++)
            {
                GameObject bgTile = Instantiate(bgTilePrefab) as GameObject;
                bgTile.transform.position = new Vector3(startX + r * size, startY + c * size, 1);
            }
        }

        // set initial money amount text
        moneyText.text = money.ToString("c2");
        cornPriceText.text = "Corn:\n" + prices[TileType.CORN].ToString("c2");
        beanPriceText.text = "Beans:\n" + prices[TileType.BEANS].ToString("c2");
        ricePriceText.text = "Rice:\n" + prices[TileType.RICE].ToString("c2");

        cornSeedPriceText.text = seedPrices[TileType.CORN].ToString("c2");
        beanSeedPriceText.text = seedPrices[TileType.BEANS].ToString("c2");
        riceSeedPriceText.text = seedPrices[TileType.RICE].ToString("c2");

        InvokeRepeating("UpdatePrices", 0f, 3f);
        InvokeRepeating("UpdateSeedPrices", 0f, 30f);
    }

    void Update()
    {
    }

    void UpdatePrices()
    {
        // https://www.investopedia.com/articles/07/montecarlo.asp
        const float mu = 0;  // expected return
        const float sigma = 0.5f;  // std dev of returns
        // update prices via geometric brownian motion

        // previous price dictionary
        Dictionary<TileType, float> oldPrices = new Dictionary<TileType, float>()
        {
            { TileType.RICE, 100 },
            { TileType.CORN, 100 },
            { TileType.BEANS, 100 }
        };
        foreach (TileType type in System.Enum.GetValues(typeof(TileType)))
        {
            if (type == TileType.EMPTY)
            {
                continue;
            }
            float currPrice = prices[type];
            oldPrices[type] = currPrice;
            float dt = Time.deltaTime;
            currPrice += currPrice * ((mu * dt) + (sigma * RandomGaussian(-1, 1) * Mathf.Sqrt(dt)));
            prices[type] = currPrice;

        }
        UpdatePricesText(oldPrices);
    }

    void UpdateSeedPrices()
    {
        Dictionary<TileType, float> oldPrices = new Dictionary<TileType, float>()
        {
            { TileType.RICE, 100 },
            { TileType.CORN, 100 },
            { TileType.BEANS, 100 }
        };
        foreach (TileType type in System.Enum.GetValues(typeof(TileType)))
        {
            if (type == TileType.EMPTY)
            {
                continue;
            }
            float priceMultiplier = Random.Range(70, 120) / 100f;
            float currPrice = prices[type] * priceMultiplier / 1.4f;
            oldPrices[type] = seedPrices[type];
            seedPrices[type] = currPrice;

        }
        UpdateSeedPricesText(oldPrices);
    }

    public void UpdateGroupedRice()
    {
        HashSet<(int, int)> dfs(int r, int c)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((r, c));
            while (q.Count > 0)
            {
                (int, int) curr = q.Dequeue();
                r = curr.Item1;
                c = curr.Item2;
                if (visited.Contains((r, c)) || tiles[r, c].GetTileType() != TileType.RICE)
                {
                    continue;
                }
                visited.Add((r, c));

                foreach ((int, int) neighbor in Utils.GetNeighbors(r, c))
                {
                    q.Enqueue(neighbor);
                }
            }

            return visited;
        }

        bool[,] seen = new bool[gridSize, gridSize];
        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                if (seen[r, c])
                {
                    continue;
                }

                HashSet<(int, int)> group = dfs(r, c);
                int waterVal = Mathf.Min(group.Count, 5);

                if (group.Count == 0)
                {
                    group.Add((r, c));
                }

                foreach ((int, int) tile in group)
                {
                    seen[tile.Item1, tile.Item2] = true;
                    tiles[tile.Item1, tile.Item2].baseAttributes.water = waterVal;
                }
            }
        }
    }

    public void SellCrop(TileType type)
    {
        Transaction(prices[type]);
        Debug.Log("Sold " + type + " for " + prices[type]);
        Debug.Log("Money: " + money);
    }

    public bool BuyCrop(TileType type)
    {
        if (seedPrices[type] > money)
        {
            moneyText.color = newRed;
            Invoke("whiteMoney", 0.3f);
            return false;
        }
        Transaction(-1 * seedPrices[type]);
        Debug.Log("Bought " + type + " seed for " + prices[type]);
        Debug.Log("Money: " + money);
        return true;
    }

    public static void test()
    {
        Debug.Log("abc");
    }

    private void UpdatePricesText(Dictionary<TileType, float> oldPrices)
    {
        if (oldPrices[TileType.CORN] < prices[TileType.CORN])
        {
            cornPriceText.color = newGreen;
        }
        else
        {
            cornPriceText.color = newRed;
        }
        // beans price color
        if (oldPrices[TileType.BEANS] < prices[TileType.BEANS])
        {
            beanPriceText.color = newGreen;
        }
        else
        {
            beanPriceText.color = newRed;
        }
        // rice price color
        if (oldPrices[TileType.RICE] < prices[TileType.RICE])
        {
            ricePriceText.color = newGreen;
        }
        else
        {
            ricePriceText.color = newRed;
        }
        cornPriceText.text = "Corn:\n" + prices[TileType.CORN].ToString("c2");
        beanPriceText.text = "Beans:\n" + prices[TileType.BEANS].ToString("c2");
        ricePriceText.text = "Rice:\n" + prices[TileType.RICE].ToString("c2");
        Invoke("whiteText", 0.5f);
    }

    private void UpdateSeedPricesText(Dictionary<TileType, float> oldPrices)
    {
        // corn price color
        if (oldPrices[TileType.CORN] < seedPrices[TileType.CORN])
        {
            cornSeedPriceText.color = newRed;
        }
        else
        {
            cornSeedPriceText.color = newGreen;
        }
        // beans price color
        if (oldPrices[TileType.BEANS] < seedPrices[TileType.BEANS])
        {
            beanSeedPriceText.color = newRed;
        }
        else
        {
            beanSeedPriceText.color = newGreen;
        }
        // rice price color
        if (oldPrices[TileType.RICE] < seedPrices[TileType.RICE])
        {
            riceSeedPriceText.color = newRed;
        }
        else
        {
            riceSeedPriceText.color = newGreen;
        }
        cornSeedPriceText.text = seedPrices[TileType.CORN].ToString("c2");
        beanSeedPriceText.text = seedPrices[TileType.BEANS].ToString("c2");
        riceSeedPriceText.text = seedPrices[TileType.RICE].ToString("c2");
        Invoke("whiteSeedText", 0.5f);
    }

    void whiteText()
    {
        cornPriceText.color = Color.white;
        beanPriceText.color = Color.white;
        ricePriceText.color = Color.white;
    }

    void whiteSeedText()
    {
        cornSeedPriceText.color = Color.white;
        beanSeedPriceText.color = Color.white;
        riceSeedPriceText.color = Color.white;
    }

    void whiteMoney()
    {
        moneyText.color = Color.white;
    }

    // change money amount
    private void Transaction(float value)
    {
        money += value;
        moneyText.text = money.ToString("c2");
    }
}
