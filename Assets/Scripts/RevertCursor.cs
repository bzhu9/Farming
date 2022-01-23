using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertCursor : MonoBehaviour
{
    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnMouseDown()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gc.selectedPlant = TileType.EMPTY;
    }
}
