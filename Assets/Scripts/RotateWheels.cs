using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    [SerializeField] Transform[] wheels = null;
    [SerializeField] float moveToRotationMult = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform w in wheels)
        {
            w.Rotate(Vector3.up * moveToRotationMult * MoveRover.Instance.currentSpeed * Time.deltaTime);
        }
    }
}
