using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class FinishController : MonoBehaviour
{
    public TextMeshProUGUI timeText1;
    public TextMeshProUGUI timeText2;
    public TextMeshProUGUI nameText;
    private bool sent = false;

    // Start is called before the first frame update
    void Start()
    {
        TimeSpan time = TimeSpan.FromSeconds(Stats.seconds);
        string s = "your time was: " + time.ToString(@"mm\:ss");
        timeText1.SetText(s);
        timeText2.SetText(s);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void BackToMenu()
    {
        SceneManager.LoadScene("menu/menu");
    }

    public void SendScore()
    {
        if (!sent)
        {
            sent = true;
            HighScores.UploadScore(nameText.text, Stats.seconds);
        }
    }
}
