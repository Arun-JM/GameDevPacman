using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private int sideSelect = 1;
    private float timeElapsed;
    public GameObject Cherry;
    private GameObject newCherry;
    private Camera mainCamera;
    private Vector3 newPosition;
    private float duration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateCherry", 5f, 10f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed >= duration)
        {
            Destroy(newCherry);
        }
        if (newCherry != null)
        {
            newCherry.transform.position = Vector3.Lerp(newPosition, Vector3.Scale(newPosition, new Vector3(-1, -1, 0)), timeElapsed / duration);
            timeElapsed += Time.deltaTime;
           
        }
    }

    void GenerateCherry()
    {
        //sideSelect = Random.Range(1, 5);
        if (sideSelect == 1)
        {
            float verborderPos = Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize);
            float horborderPos = -mainCamera.orthographicSize * mainCamera.aspect + 1;
            newPosition = new Vector3(horborderPos, verborderPos, 0f);
            newCherry = Instantiate(Cherry, newPosition, Quaternion.identity);
        }
    }
}
