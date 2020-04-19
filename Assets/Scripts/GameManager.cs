using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying { get; private set; }
    public UnityEvent onGameStart = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        isPlaying = false;
    }

    public void StartGame()
    {
        isPlaying = true;
        onGameStart.Invoke();
    }

    public void GameOver()
    {
        isPlaying = false;
        onGameOver.Invoke();
    }
}
