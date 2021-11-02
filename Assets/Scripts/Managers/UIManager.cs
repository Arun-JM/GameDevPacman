using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int Score = 0;
    private Text scoreLabel;
    private bool GameLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLoaded == true) { scoreLabel.text = "Score: " + Score; }
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadSecondLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void ExitFirstLevel()
    {
        SceneManager.LoadSceneAsync(0);
        GameLoaded = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       if (scene.buildIndex == 1) { 
            Button button = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
            button.onClick.AddListener(ExitFirstLevel);
            scoreLabel = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<Text>();
            GameLoaded = true;
        }
    }
}
