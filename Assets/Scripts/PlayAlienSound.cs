using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAlienSound : MonoBehaviour
{
    public void PlayClip0()
    {
        AlienAudio.Instance.PlayCip(0);
    }
    public void PlayClip1()
    {
        AlienAudio.Instance.PlayCip(1);
    }
    public void PlayClip2()
    {
        AlienAudio.Instance.PlayCip(2);
    }
    public void PlayClip3()
    {
        AlienAudio.Instance.PlayCip(3);
    }
}
