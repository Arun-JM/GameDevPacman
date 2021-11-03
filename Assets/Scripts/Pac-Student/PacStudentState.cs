using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentState : MonoBehaviour
{
    private GameUIManager gameUI;
    private Animator pacAnim;
    private List<GameObject> lifeBar = new List<GameObject>();
    private int Health;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("GameManager").GetComponent<GameUIManager>();
        Health = 3;
        pacAnim = gameObject.GetComponent<Animator>();
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
        if (collider.tag == "Enemy" && hit == false)
        {
            Health -= 1;
            Destroy(lifeBar[lifeBar.Count - 1]);
            StartCoroutine(DeadState());
            hit = true;
        }
    }

    IEnumerator DeadState()
    {
        pacAnim.SetTrigger("Death");
        gameObject.GetComponent<PacStudentController>().lastInput = null;
        gameObject.GetComponent<PacStudentController>().movement = Vector3.zero;
        gameObject.GetComponent<PacStudentController>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<PacStudentController>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().WakeUp();
        pacAnim.SetInteger("Direction", 0);
        gameObject.transform.position = new Vector3(-1.528f, 1.958f, 0);
        gameObject.transform.Rotate(transform.position, 90f);
        gameObject.GetComponent<PacStudentController>().previousPosition = new Vector3(1.528f, 1.958f, 0);
        hit = false;
    }
}
