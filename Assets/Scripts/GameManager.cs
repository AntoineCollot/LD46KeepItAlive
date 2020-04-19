using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying { get; private set; }
    public UnityEvent onGameStart = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    [SerializeField] TMPro.TextMeshProUGUI explanationText;

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

    public void GameOver(string failureMessage)
    {
        isPlaying = false;
        onGameOver.Invoke();

        explanationText.text = failureMessage;
    }
}
