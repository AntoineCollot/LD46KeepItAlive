using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInGame : MonoBehaviour
{
    public static int rockCount = 0;

    void OnEnable()
    {
        rockCount++;
    }

    private void OnDisable()
    {
        rockCount--;
    }
}
