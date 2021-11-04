using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text textHighScore;
    private Text textTime;
    private static UIManager instance;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
        if (instance == null)
        {
            instance = this;
        }  else if (instance != this)
        {
            Destroy(gameObject);
        }
        LoadScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) { 
            Button button = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
            button.onClick.AddListener(ExitFirstLevel);
           
        } else if (scene.buildIndex == 0)
        {
            LoadScoreText();
            Button button = GameObject.FindGameObjectWithTag("btnLevel1").GetComponent<Button>();
            button.onClick.AddListener(LoadFirstLevel);
        }
    }

    private void LoadScoreText()
    {
        textHighScore = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        textTime = GameObject.FindGameObjectWithTag("ScoreTimer").GetComponent<Text>();
        string HighScore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        string time = PlayerPrefs.GetString("GameTime", "00:00:00").ToString();
        textHighScore.text = "Previous Text High Score: " + HighScore;
        textTime.text = "Previous High Score Time: " + time;
    }
}
