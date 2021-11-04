using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    public AudioClip Intro;
    public AudioClip NormalBackground;
    public AudioClip ScaredBackground;
    public AudioClip DeadBackground;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(Intro);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayScaredBackground()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(ScaredBackground);
    }

    public void PlayNormalBackground()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(NormalBackground);
    }

    public void PlayDeadBackground()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(DeadBackground);
    }
}
