using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcript : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI transcriptText = null;
    [SerializeField] float timePerLetter = 0.05f;
    [SerializeField] float textDisplayTime = 3;

    public void Type(string text)
    {
        StopAllCoroutines();
        StartCoroutine(TypeIn(text));
    }

    IEnumerator TypeIn(string text)
    {
        float t = 0;
        transcriptText.maxVisibleCharacters = 0;
        float totalTime = timePerLetter * text.Length;
        transcriptText.text = text;
        while (t<1)
        {
            t += Time.deltaTime / totalTime;

            transcriptText.maxVisibleCharacters = Mathf.CeilToInt(Mathf.Lerp(0, text.Length, t));

            yield return null;
        }

        yield return new WaitForSeconds(textDisplayTime);

        transcriptText.text = "";
    }
}
