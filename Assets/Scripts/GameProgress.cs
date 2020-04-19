using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    //Score
    public static float startTime = 0;
    public const int EndGameTime = 1000;
    public static float GameTime
    {
        get
        {
            return Time.time - startTime;
        }
    }
    public float dayLength = 10;
    public static int DayCount
    {
        get
        {
            return Mathf.FloorToInt(GameTime / Instance.dayLength);
        }
    }

    [SerializeField] float antenaStartInterval = 20;
    [SerializeField] float antenaEndInterval = 5;
    public static float AntenaScanInterval
    {
        get
        {
            return Mathf.Lerp(Instance.antenaStartInterval, Instance.antenaEndInterval, GameTime / EndGameTime);
        }
    }

    [SerializeField] float averageFailureStartTime = 20;
    [SerializeField] float averageFailureEndTime = 5;

    public static float AverageFailureTime
    {
        get
        {
            return Mathf.Lerp(Instance.averageFailureStartTime, Instance.averageFailureEndTime, GameTime / EndGameTime);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        startTime = Time.time;
    }
}
