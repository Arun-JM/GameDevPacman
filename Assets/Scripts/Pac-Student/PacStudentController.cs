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
    public AudioClip[] movementAudio = new AudioClip[3];
    private Tilemap tilemap;
    private const float duration = 0.5f;
    private Vector3 movement;
    private Vector3 previousPosition;
    private Vector3Int nextTile;
    private bool WallFlag = false;
    private ParticleSystem particleImpact;
    private List<Vector3Int> listTeleport = new List<Vector3Int>();
    private bool teleportFlag = false;
    private GameUIManager GameUIManager;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<Tilemap>();
        pacAnimator = GetComponent<Animator>();
        pacAudio = GetComponentInChildren<AudioSource>();
        pacAudio.Stop();
        pacAudio.clip = null;
        particleImpact = GameObject.FindGameObjectWithTag("Impact").GetComponent<ParticleSystem>();
        particleImpact.Stop();
        GameUIManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameUIManager>();
        for (int y = tilemap.origin.y; y < (tilemap.origin.y + tilemap.size.y); y++) // Gets the position of all Teleporter Tiles in Tilemap
        {
            for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
            {
                Vector3Int temp = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(temp) != null && tilemap.GetSprite(temp).name.Equals("Teleporter"))
                {
                    listTeleport.Add(new Vector3Int(x, y, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { // Gets the last input
            lastInput = "w";
        } else if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = "s";
        } else if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = "a";
        } else if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = "d";
        }
        if (timeElapsed >= duration || movement == Vector3.zero) // Sets the direction of movement
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
            if (tilemap.GetSprite(nextTile) != null && !tilemap.GetSprite(nextTile).name.Equals("RegPellet") && !tilemap.GetSprite(nextTile).name.Equals("Teleporter")) // If Wall if Hit
            {
                if (WallFlag == false) { pacAudio.clip = movementAudio[2]; pacAudio.PlayOneShot(movementAudio[2]); WallFlag = true; particleImpact.Play(); }
                movement = Vector3.zero;
            } else if (tilemap.GetSprite(nextTile) == null) { // If Next Tile is Blank
                if (pacAudio.clip != movementAudio[0]) { pacAudio.clip = movementAudio[0]; pacAudio.Play(); }
                teleportFlag = false;
            } else if (tilemap.GetSprite(nextTile).name.Equals("RegPellet")) // If Next Tile is Pellet
            {
               pacAudio.clip = movementAudio[1];
               teleportFlag = false;
               if (!pacAudio.isPlaying && (movement != null && movement != Vector3.zero)) { pacAudio.PlayOneShot(movementAudio[1]); }
                tilemap.SetTile(nextTile, null);
                GameUIManager.Score += 10;
            } else if (tilemap.GetSprite(nextTile).name.Equals("Teleporter")) // If next tile is teleporter
            {
                if (nextTile == listTeleport[0] && teleportFlag == false) { transform.position = tilemap.GetCellCenterWorld(listTeleport[1]); teleportFlag = true; previousPosition = tilemap.GetCellCenterWorld(listTeleport[1]); }
                else if (nextTile == listTeleport[1] && teleportFlag == false) { transform.position = tilemap.GetCellCenterWorld(listTeleport[0]); teleportFlag = true; previousPosition = tilemap.GetCellCenterWorld(listTeleport[0]); }
            }
            
        } else
        { // If movement found then lerp
            WallFlag = false;
            currentInput = lastInput;
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(previousPosition, previousPosition + movement, t);
            timeElapsed += Time.deltaTime;
        }
    }
}
