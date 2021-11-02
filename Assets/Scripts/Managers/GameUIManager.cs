using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public int Score = 0;
    private Text scoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        scoreLabel = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + Score; 
    }
}
