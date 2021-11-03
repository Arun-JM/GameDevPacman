using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentState : MonoBehaviour
{
    private GameUIManager gameUI;
    private Animator pacAnim;
    private List<GameObject> lifeBar = new List<GameObject>();
    private ParticleSystem explosion;
    private int Health;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("GameManager").GetComponent<GameUIManager>();
        Health = 3;
        pacAnim = gameObject.GetComponent<Animator>();
        explosion = GameObject.Find("ParticleDeath").GetComponent<ParticleSystem>();
        foreach (var sprite in GameObject.FindGameObjectsWithTag("Life"))
        {
            lifeBar.Add(sprite);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Health == 0)
        {
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && hit == false && (collider.GetComponent<StateChanger>().scaredState == false))
        {
            Health -= 1;
            Destroy(lifeBar[lifeBar.Count - 1]);
            lifeBar.RemoveAt(lifeBar.Count - 1);
            StartCoroutine(DeadState());
            hit = true;
        } else if (collider.tag == "Enemy" && collider.GetComponent<StateChanger>().scaredState == true)
        {
            gameUI.ScoreUpdater(300);
            collider.GetComponent<StateChanger>().SetDead();
        }
    }

    IEnumerator DeadState()
    {
        pacAnim.SetTrigger("Death");
        gameObject.GetComponent<PacStudentController>().lastInput = null;
        gameObject.GetComponent<PacStudentController>().movement = Vector3.zero;
        gameObject.GetComponent<PacStudentController>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        yield return new WaitForSeconds(0.5f);
        explosion.Play();
        yield return new WaitForSeconds(1.6f);
        gameObject.GetComponent<PacStudentController>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().WakeUp();
        pacAnim.SetInteger("Direction", 90);
        gameObject.transform.position = new Vector3(-1.528f, 1.958f, 0);
        gameObject.GetComponent<PacStudentController>().previousPosition = new Vector3(1.528f, 1.958f, 0);
        hit = false;
    }
}
