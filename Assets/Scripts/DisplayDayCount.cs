using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDayCount : MonoBehaviour
{
    TextMeshProUGUI dayCountText;

    // Start is called before the first frame update
    void Start()
    {
        dayCountText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.isPlaying)
        {
           dayCountText.text = "Day " + GameProgress.DayCount.ToString();
        }
    }
}
