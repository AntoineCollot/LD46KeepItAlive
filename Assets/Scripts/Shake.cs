using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float frequency = 1;
    [SerializeField] float rotationAmplitude = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shake = new Vector3((Mathf.PerlinNoise(Time.time * frequency,0) - 0.5f) * rotationAmplitude * MoveRover.Instance.currentSpeed,
                                    90,
                                    (Mathf.PerlinNoise(Time.realtimeSinceStartup * frequency, 0) - 0.5f) * rotationAmplitude * MoveRover.Instance.currentSpeed);
        transform.localEulerAngles = shake;
    }
}
