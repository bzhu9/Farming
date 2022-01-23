using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    public GameObject bgTilePrefab;
    private List<GameObject> bgTiles = new List<GameObject>();

    private float dx;
    private float dy;

    // Start is called before the first frame update
    void Start()
    {
        dx = 0;
        dy = 0;
        for (int r = -5; r < 5; r++)
        {
            for (int c = -5; c < 5; c++)
            {
                GameObject bgTile = Instantiate(bgTilePrefab) as GameObject;
                bgTile.transform.position = new Vector3(r, c, 1);
                bgTiles.Add(bgTile);
            }
        }
    }

    void Update()
    {
        float cdx = Time.deltaTime;
        float cdy = Time.deltaTime;
        dx += cdx;
        dy += cdy;

        while (dy >= 1f)
        {
            cdy -= 1f;
            dy -= 1f;
        }
        while (dx >= 1f)
        {
            cdx -= 1f;
            dx -= 1f;
        }

        foreach (GameObject o in bgTiles)
        {
            Vector3 pos = o.transform.position;
            o.transform.position = new Vector3(pos.x + cdx, pos.y + cdy, pos.z);
        }
    }
}
