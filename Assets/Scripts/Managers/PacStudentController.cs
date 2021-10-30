using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private string currentInput;
    private string lastInput;
    private float timeElapsed;
    private Animator pacAnimator;
    private Tilemap tilemap;
    private const float duration = 1f;
    private Vector3Int movement;
    private Vector3 previousPosition;
    private Vector3Int nextTile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        pacAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed >= duration)
        {
            switch (lastInput)
            {
                case "a":
                    movement = new Vector3Int(-1, 0, 0);
                    break;
                case "d":
                    movement = new Vector3Int(1, 0, 0);
                    break;
                case "w":
                    movement = new Vector3Int(0, 1, 0);
                    break;
                case "s":
                    movement = new Vector3Int(0, -1, 0);
                    break;
                default:
                    break;
            }
            nextTile = Vector3Int.FloorToInt(transform.position) + movement;
            if (tilemap.GetSprite(nextTile) != null && (!tilemap.GetSprite(nextTile).name.Equals("RegPellet") || !tilemap.GetSprite(nextTile).name.Equals("PowerPellet")))
            {
                movement = new Vector3Int(0, 0, 0);
            }
            timeElapsed = 0;
            previousPosition = transform.position;
        }
        transform.position = Vector3.Lerp(transform.position, nextTile, timeElapsed / duration);
        timeElapsed += Time.deltaTime;
    }
}
