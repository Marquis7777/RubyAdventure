using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Text scoreText;

    public GameObject winTextObject;

    int score = 0;

    public Image mask;
    float originalSize;

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
     if (score == 4)
            {
                winTextObject.SetActive(true);
            }

             //If game is won
     if (winTextObject)
        {
            //If R is hit, restart the current scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Load the level named "MainScene".
        Application.LoadLevel("MainScene");
            }
            
            //If Q is hit, quit the game
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Application Quit");
                Application.Quit();
            }
        }
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        scoreText.text = score.ToString() + " :FIXED ROBOTS";
        winTextObject.SetActive(false);
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void AddFixed() {
        score += 1;
        scoreText.text = score.ToString() + " :FIXED ROBOTS";
    }
}
