using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicesManager : MonoBehaviour
{
    [SerializeField] ClipTranscripted[] failureClips = null;
    [SerializeField] ClipTranscripted[] fixClips = null;
    [SerializeField] ClipTranscripted[] postRadarClips = null;

    int failId = -1;

    new AudioSource audio;
    Queue<ClipTranscripted> playQueue = new Queue<ClipTranscripted>();
    bool playFix =false;

    Transcript transcript;
    public static VoicesManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
        transcript = GetComponent<Transcript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            if (playQueue.Count>0)
            {
                ClipTranscripted clip = playQueue.Dequeue();
                audio.clip = clip.clip;
                audio.Play();

                transcript.Type(clip.transcript);
            }
        }
    }

    public void PlayNextFailureClip()
    {
        //Do not go to the next clip if something is playing
        if (audio.isPlaying)
            return;

        failId++;
        playFix = true;

        if (failId< failureClips.Length && failureClips[failId]!=null)
        {
            playQueue.Enqueue(failureClips[failId]);
        }
    }

    public void PlayFixClip()
    {
        if (playFix && failId < fixClips.Length && fixClips[failId] != null)
        {
            playQueue.Enqueue(fixClips[failId]);
        }
        playFix = false;
    }
}
