using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuPostProcess : MonoBehaviour
{
    PostProcessVolume volume;


    [SerializeField] float vignetteStartIntensity =0.4f;
    [SerializeField] float vignetteStartSmooth = 0.2f;
    [SerializeField] float chromaStartIntensity = 0.05f;
    [SerializeField] float colorStartEV = 0;
    [SerializeField] float colorStartSaturation = 0;

    [SerializeField] float vignetteTargetIntensity = 0.6f;
    [SerializeField] float vignetteTargetSmooth = 0.8f;
    [SerializeField] float chromaAberationIntensity = 0.4f;
    [SerializeField] float colorTargetEV = -0.5f;
    [SerializeField] float colorTargetSaturation = -20;

    // Start is called before the first frame update
    void Start()
    {
        volume = Camera.main.GetComponent<PostProcessVolume>();
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
        GameManager.Instance.onGameOver.AddListener(OnGameEnd);
    }

    void OnGameStart()
    {
        StartCoroutine(Fade(false));
    }

    void OnGameEnd()
    {
        StartCoroutine(Fade(true));
    }

    IEnumerator Fade(bool value)
    {
        PostProcessProfile profile = volume.profile;
        Vignette vignette = profile.GetSetting<Vignette>();
        ChromaticAberration chromaticAberration = profile.GetSetting<ChromaticAberration>();
        ColorGrading color = profile.GetSetting<ColorGrading>();

        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime;
            float fadeT = Mathf.Clamp01(t);
            if (!value)
                fadeT = 1 - fadeT;

            vignette.intensity.value = Curves.QuartEaseInOut(vignetteStartIntensity, vignetteTargetIntensity, fadeT);
            vignette.smoothness.value = Curves.QuartEaseInOut(vignetteStartSmooth, vignetteTargetSmooth, fadeT);
            chromaticAberration.intensity.value = Curves.QuartEaseInOut(chromaStartIntensity, chromaAberationIntensity, fadeT);
            color.postExposure.value = Curves.QuartEaseInOut(colorStartEV, colorTargetEV, fadeT);
            color.saturation.value = Curves.QuartEaseInOut(colorStartSaturation, colorTargetSaturation, fadeT);

            yield return null;
        }
    }
}
