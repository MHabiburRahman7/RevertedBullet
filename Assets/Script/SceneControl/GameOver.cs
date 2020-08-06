using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public string score;
    public Text score_text;

    private void Awake()
    {
        score_text.text = "Your Score: " + score;
    }

    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void BackToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
