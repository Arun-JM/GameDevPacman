using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletCollection : MonoBehaviour
{
    private GameObject ufo1;
    private GameObject ufo2;
    private GameObject ufo3;
    private GameObject ufo4;
    // Start is called before the first frame update
    void Start()
    {
        ufo1 = GameObject.Find("UFOGreen");
        ufo2 = GameObject.Find("UFOOrange");
        ufo3 = GameObject.Find("UFOPurple");
        ufo4 = GameObject.Find("UFORed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ufo1.GetComponent<StateChanger>().SetScared();
            ufo2.GetComponent<StateChanger>().SetScared();
            ufo3.GetComponent<StateChanger>().SetScared();
            ufo4.GetComponent<StateChanger>().SetScared();
            Destroy(this.gameObject);
        }
    }
}
