using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour
{
    public static GameObject menuCanvas;
    public static GameObject insCanvas;

    void Start()
    {
        menuCanvas = GameObject.Find("CanvasMain");
        insCanvas = GameObject.Find("CanvasInstructions");
        insCanvas.SetActive(false);
    }

    public static void LoadInstructions()
    {
        menuCanvas.SetActive(false);
        insCanvas.SetActive(true);
    }
    public static void LoadGame()
    {
        SceneManager.LoadScene("main");
    }
}
