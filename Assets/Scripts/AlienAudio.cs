using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAudio : MonoBehaviour
{
    //0 walk
    //1 Fix
    //2 Start Fix
    public AudioClip[] clips;

    public static AlienAudio Instance;
    new AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void PlayCip(int id)
    {
        audio.PlayOneShot(clips[id]);
    }
}
