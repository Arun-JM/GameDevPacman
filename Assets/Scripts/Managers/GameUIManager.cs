using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public int Score = 0;
    private Text scoreLabel;
    private Text timeLabel;
    private GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        scoreLabel = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<Text>();
        timeLabel = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        hud = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + Score;
    }

    public void ScoreUpdater(int add)
    {
        Score += add;
    }

    private void FixedUpdate()
    {
        timeLabel.text = "Game Time: " + Time.fixedTime;
    }
}
