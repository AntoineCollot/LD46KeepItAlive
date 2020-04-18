using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [HideInInspector] public float currentSpeed;

    public static MoveRover Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = moveSpeed;
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.World);
    }
}
