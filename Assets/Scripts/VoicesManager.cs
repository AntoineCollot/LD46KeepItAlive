using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicesManager : MonoBehaviour
{
    [SerializeField] ClipTranscripted[] failureClips = null;
    [SerializeField] ClipTranscripted[] fixClips = null;
    [SerializeField] float randomClipAverageInterval = 20;
    [SerializeField] float minRandomClipInterval = 10;
    [SerializeField] ClipTranscripted[] randomClips = null;
    [SerializeField] ClipTranscripted[] gameOverSeenClips = null;
    [SerializeField] ClipTranscripted[] gameOverBrokenClips = null;
    [SerializeField] ClipTranscripted preScanClip = null;
    [SerializeField] ClipTranscripted scanningClip = null;
    [SerializeField] DayCountClip[] dayCountClips = null;
    [SerializeField] ClipTranscripted startClip = null;

    int failureId = -1;
    int randomClipId = -1;
    int dayCountId = 0;
    bool preScanningClipPlayed;
    bool playFix =false;
    float lastRandomClipTime;

    new AudioSource audio;
    Queue<ClipTranscripted> playQueue = new Queue<ClipTranscripted>();

    Transcript transcript;
    public static VoicesManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
        transcript = GetComponent<Transcript>();
    }

    private void Start()
    {
        GameManager.Instance.onGameStart.AddListener(PlayStartClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            if (playQueue.Count>0)
            {
                ClipTranscripted clip = playQueue.Dequeue();
                PlayClip(clip);
            }
        }

        //Random clips
        {
            //If nothing is waiting
            if(GameManager.isPlaying && playQueue.Count==0)
            {
                if(Random.Range(0f, 1f)<Time.deltaTime/ randomClipAverageInterval && Time.time > lastRandomClipTime + minRandomClipInterval)
                {
                    lastRandomClipTime = Time.time;
                    PlayNextRandomClip();
                }
            }
        }

        //DayCount
        if(dayCountId<dayCountClips.Length && GameProgress.DayCount == dayCountClips[dayCountId].dayCount)
        {
            playQueue.Enqueue(dayCountClips[dayCountId].clipTranscripted);
            dayCountId++;
        }
    }

    public void PlayStartClip()
    {
        playQueue.Enqueue(startClip);
    }
    
    void PlayClip(ClipTranscripted clip)
    {
        audio.clip = clip.clip;
        audio.Play();

        transcript.Type(clip.transcript);
    }

    public void PlayNextRandomClip()
    {
        randomClipId++;
        if (randomClipId < randomClips.Length)
            playQueue.Enqueue(randomClips[randomClipId]);
    }

    public void PlayNextFailureClip()
    {
        //Do not go to the next clip if something is playing
        if (audio.isPlaying)
            return;

        failureId++;
        playFix = true;

        if (failureId< failureClips.Length && failureClips[failureId]!=null)
        {
            playQueue.Enqueue(failureClips[failureId]);
        }
    }

    public void PlayFixClip()
    {
        if (playFix && failureId < fixClips.Length && fixClips[failureId] != null)
        {
            playQueue.Enqueue(fixClips[failureId]);
        }
        playFix = false;
    }

    public void PlayGameOverClip(bool hasBeenSeen)
    {
        if(hasBeenSeen)
            PlayClip(gameOverSeenClips[Random.Range(0, gameOverSeenClips.Length)]);
        else
            PlayClip(gameOverBrokenClips[Random.Range(0, gameOverBrokenClips.Length)]);
    }

    public void PlayScanningClip()
    {
        if(playQueue.Count==0 && Random.Range(0f, 1f)<0.2f)
        {
            playQueue.Enqueue(scanningClip);
        }
    }

    public void PlayPreScanClip()
    {
        if (!preScanningClipPlayed && playQueue.Count == 0 && Random.Range(0f, 1f) < 0.3f)
        {
            preScanningClipPlayed = true;
            playQueue.Enqueue(preScanClip);
        }
    }

    [System.Serializable]
    public struct DayCountClip
    {
        public int dayCount;
        public ClipTranscripted clipTranscripted;
    }
}
