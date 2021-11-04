using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text textHighScore;
    private Text textTime;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  
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
            Debug.Log("Reached");
            textHighScore = GameObject.Find("txtScore").GetComponent<Text>();
            textTime = GameObject.Find("txtTime").GetComponent<Text>();
            textHighScore.text = "Previous High Score: " + PlayerPrefs.GetInt("HighScore");
            textTime.text = "Previous High Score Time: " + PlayerPrefs.GetString("GameTime");          
        }
    }
}
