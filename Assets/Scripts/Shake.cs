using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [Header("Moving State")]
    [SerializeField] float frequency = 1;
    [SerializeField] float rotationAmplitude = 1;

    [Header("Critical failure state")]
    [SerializeField] float criticalFrequency = 1;
    [SerializeField] float criticalRotationAmplitude = 1;
    [SerializeField] float criticalPositionAmplitude = 1;

    public State criticalShakeState = new State();
    public static Shake Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationShake = new Vector3((Mathf.PerlinNoise(Time.time * frequency,0) - 0.5f) * rotationAmplitude * MoveRover.Instance.currentSpeed,
                                    90,
                                    (Mathf.PerlinNoise(Time.realtimeSinceStartup * frequency, 0) - 0.5f) * rotationAmplitude * MoveRover.Instance.currentSpeed);
        Vector3 posShake = Vector3.zero;


        if(criticalShakeState.IsOn)
        {
            rotationShake = new Vector3((Mathf.PerlinNoise(Time.time * criticalFrequency, 0) - 0.5f) * criticalRotationAmplitude,
                                   90,
                                   (Mathf.PerlinNoise(Time.realtimeSinceStartup * criticalFrequency, 0) - 0.5f) * criticalRotationAmplitude);

            posShake = new Vector3((Mathf.PerlinNoise(Time.time * criticalFrequency, 0) - 0.5f) * criticalPositionAmplitude,
                                   (Mathf.PerlinNoise(Time.fixedTime * criticalFrequency, 0) - 0.5f) * criticalPositionAmplitude,
                                   (Mathf.PerlinNoise(Time.realtimeSinceStartup * criticalFrequency, 0) - 0.5f) * criticalPositionAmplitude);
        }


        transform.localEulerAngles = rotationShake;
        transform.localPosition = posShake;
    }
}
