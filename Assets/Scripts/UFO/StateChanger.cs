using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateChanger : MonoBehaviour
{
    private Animator ufoAnim;
    private Text textScaredTime;
    private MusicPlayer musicPlayer;
    private float timeRemaining = 0f;
    public bool scaredState = false;
    private bool deadState = false;
    public bool recoverState = false;
    // Start is called before the first frame update
    void Start()
    {
        ufoAnim = GetComponent<Animator>();
        textScaredTime = GameObject.Find("txtScaredTimer").GetComponent<Text>();
        musicPlayer = GameObject.Find("BackgroundSource").GetComponent<MusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scaredState == true)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                SetNormal();
            } else if (timeRemaining <= 3)
            {
                SetRecover();
            } 
            textScaredTime.text = "Scared Time Remaining: " + (int)timeRemaining;
        } else if (deadState == true)
        {
            timeRemaining -= Time.deltaTime;
           
            if (timeRemaining <= 0)
            {
                
                SetNormal();
            }
        }
    }

    public void SetScared()
    {
        ufoAnim.SetBool("Scared", true);
        ufoAnim.SetBool("Normal", false);
        ufoAnim.SetBool("Dead", false);
        ufoAnim.SetBool("Recover", false);
        timeRemaining = 10f;
        scaredState = true;
        textScaredTime.enabled = true;
        musicPlayer.PlayScaredBackground();
    }
    public void SetDead()
    {
        ufoAnim.SetBool("Dead", true);
        ufoAnim.SetBool("Scared", false);
        ufoAnim.SetBool("Normal", false);
        ufoAnim.SetBool("Recover", false);
        timeRemaining = 5f;
        deadState = true;
        scaredState = false;
        musicPlayer.PlayDeadBackground();
    }
    public void SetRecover()
    {
        ufoAnim.SetBool("Recover", true);
        ufoAnim.SetBool("Dead", false);
        ufoAnim.SetBool("Scared", false);
        ufoAnim.SetBool("Normal", false);

    }
    public void SetNormal()
    {
        ufoAnim.SetBool("Normal", true);
        ufoAnim.SetBool("Recover", false);
        ufoAnim.SetBool("Scared", false);
        ufoAnim.SetBool("Dead", false);
        scaredState = false;
        deadState = false;
        timeRemaining = 0f;
        textScaredTime.enabled = false;
        musicPlayer.PlayNormalBackground();
    }

  
   
    
}
