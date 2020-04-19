using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipTranscripted")]
public class ClipTranscripted : ScriptableObject
{
    public AudioClip clip;
    [TextArea] public string transcript;
}
