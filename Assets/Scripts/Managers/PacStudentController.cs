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
    private const float duration = 0.1f;
    private Vector3Int movement;
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
            TileBase nextTile = tilemap.GetTile(Vector3Int.FloorToInt(transform.position) + movement);
            if (tilemap)
            {

            }
        }
    }
}
