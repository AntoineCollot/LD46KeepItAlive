using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    [SerializeField] float delay = 4;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Reload", delay);
    }

    void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
