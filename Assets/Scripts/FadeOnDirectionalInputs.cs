using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnDirectionalInputs : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LookForInputs());
    }

    IEnumerator LookForInputs()
    {
        while(true)
        {
            if(Mathf.Abs(Input.GetAxis("Horizontal"))>0.3f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.3f)
            {
                StartCoroutine(Fade());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Fade()
    {
        float t = 0;
        Color c = Color.white;
        while(t<1)
        {
            t += Time.deltaTime * 3;

            c.a = 1 - Mathf.Clamp01(t);
            text.color = c;
            image.color = c;

            yield return null;
        }
    }
}
