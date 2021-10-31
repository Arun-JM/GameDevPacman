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
    private AudioSource pacAudio;
    public AudioClip[] movementAudio = new AudioClip[2];
    private Tilemap tilemap;
    private const float duration = 1f;
    private Vector3 movement;
    private Vector3 previousPosition;
    private Vector3Int nextTile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<Tilemap>();
        pacAnimator = GetComponent<Animator>();
        pacAudio = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            lastInput = "w";
        } else if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = "s";
        } else if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = "a";
        } else if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = "d";
        }
        if (timeElapsed >= duration || movement == Vector3.zero)
        {
            switch (lastInput)
            {
                case "a":
                    movement = new Vector3(-0.2f, 0, 0);
                    pacAnimator.SetInteger("Direction", 0);
                    break;
                case "d":
                    movement = new Vector3(0.2f, 0, 0);
                    pacAnimator.SetInteger("Direction", 180);
                    break;
                case "w":
                    movement = new Vector3(0, 0.2f, 0);
                    pacAnimator.SetInteger("Direction", 90);
                    break;
                case "s":
                    movement = new Vector3(0, -0.2f, 0);
                    pacAnimator.SetInteger("Direction", 270);
                    break;
                default:
                    break;
            }
            timeElapsed = 0;
            previousPosition = transform.position;
            nextTile = tilemap.WorldToCell(previousPosition + movement);
            if (tilemap.GetSprite(nextTile) != null && !tilemap.GetSprite(nextTile).name.Equals("RegPellet"))
            {
                movement = Vector3.zero;
                pacAudio.Stop();
                pacAudio.clip = null;
            } else if (tilemap.GetSprite(nextTile) == null) {
                if (pacAudio.clip != movementAudio[0]) { pacAudio.clip = movementAudio[0]; pacAudio.Play(); }
            } else if (tilemap.GetSprite(nextTile).name.Equals("RegPellet"))
            {
               pacAudio.clip = movementAudio[1];
               pacAudio.PlayOneShot(movementAudio[1]);
            }
            
        } else
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(previousPosition, previousPosition + movement, t);
            timeElapsed += Time.deltaTime;
        }
    }
}
