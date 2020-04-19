using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    public float currentSpeed { get; private set; }
    float targetSpeed;
    float refSpeed;
    [SerializeField] float speedChangesSmooth = 0.3f;

    public State stopState;

    public static MoveRover Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        stopState = new State();
    }

    private void Start()
    {
        GameManager.Instance.onGameStart.AddListener(Go);
        GameManager.Instance.onGameOver.AddListener(Stop);
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref refSpeed, speedChangesSmooth);
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.World);
    }

    public void Stop()
    {
        targetSpeed = 0;
    }

    public void Go()
    {
        if(GameManager.isPlaying && !stopState.IsOn)
            targetSpeed = moveSpeed;
    }
}
