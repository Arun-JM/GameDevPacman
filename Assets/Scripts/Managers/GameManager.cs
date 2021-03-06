using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Tilemap map;
    private GameObject pacStudent;
    private GameUIManager manager;
    private AudioSource backgroundaudio;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<Tilemap>();
        pacStudent = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameUIManager>();
        backgroundaudio = GameObject.Find("BackgroundSource").GetComponent<AudioSource>();
        StartCoroutine("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        pacStudent.GetComponent<PacStudentController>().enabled = false;
        Text display = GameObject.Find("GameMessage").GetComponent<Text>();
        display.enabled = true;
        display.text = "3";
        yield return new WaitForSeconds(1);
        display.text = "2";
        yield return new WaitForSeconds(1);
        display.text = "1";
        yield return new WaitForSeconds(1);
        display.text = "GO!";
        yield return new WaitForSeconds(1);
        display.enabled = false;
        pacStudent.GetComponent<PacStudentController>().enabled = true;
        manager.gameStart = true;
        backgroundaudio.GetComponent<MusicPlayer>().PlayNormalBackground();
    }

    public void GameOver()
    {
        manager.gameStart = false;
        int PreviousScore = PlayerPrefs.GetInt("HighScore", 0);
        float PreviousTime = PlayerPrefs.GetFloat("GameTime", 0);
        if (manager.Score > PreviousScore || (manager.Score == PreviousScore && manager.gameTime < PreviousTime))
        {
            PlayerPrefs.SetInt("HighScore", manager.Score);
            PlayerPrefs.SetFloat("GameTime", manager.gameTime);
            PlayerPrefs.Save();
        }
        backgroundaudio.enabled = false;
        pacStudent.SetActive(false);
        backgroundaudio.GetComponent<MusicPlayer>().enabled = false;
        StartCoroutine("ShowGameOverScreen");
    }

    IEnumerator ShowGameOverScreen()
    {
        GameObject.Find("GameMessage").GetComponent<Text>().enabled = true;
        GameObject.Find("GameMessage").GetComponent<Text>().text = "Game Over!";
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(0);
    }
}
