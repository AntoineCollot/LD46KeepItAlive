using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverAudio : MonoBehaviour
{
    new AudioSource audio;

    //0 hello
    //1 Command 1
    //2 Command 2
    //3 alert slow
    //4 alert fast
    //5 happy
    public AudioClip[] audioClips;
    bool isPlayingLoop;
    int currentLoopId;

    public static RoverAudio Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void PlayClip(int id)
    {
        if (isPlayingLoop)
            return;
        audio.PlayOneShot(audioClips[id]);
    }

    public void PlayLoop(int id)
    {
        if (isPlayingLoop && currentLoopId>=id)
            return;
        isPlayingLoop = true;
        audio.clip = audioClips[id];
        audio.loop = true;
        audio.Play();
    }

    public void StopLoop()
    {
        isPlayingLoop = false;
        audio.Stop();
    }
}
