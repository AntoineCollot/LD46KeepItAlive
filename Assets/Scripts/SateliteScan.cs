using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteScan : MonoBehaviour
{
    [Header("Scan")]
    [SerializeField] float maxDistanceFromRover = 10;
    [SerializeField] Transform centerPoint=null;

    [Header("Grid")]
    [SerializeField] float width = 4;
    [SerializeField] Projector gridProjector=null;
    [SerializeField] float showGridDelay = 1;
    [SerializeField] float showGridProgressTime = 1f;

    [Header("Satelite")]
    [SerializeField] Projector satelliteProjector = null;
    [SerializeField] Vector3 satelliteStartPos = Vector3.zero;
    [SerializeField] Vector3 satelliteTargetPos = Vector3.zero;
    [SerializeField] float satelliteTravelingTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        gridProjector.material.SetFloat("_Reveal", 0);
    }

    public void Scan()
    {
        //Place the satellite
        Vector3 pos = transform.position;
        pos.x = centerPoint.position.x + Random.Range(-maxDistanceFromRover, maxDistanceFromRover);

        StartCoroutine(SatelliteTraveling());
        StartCoroutine(ScanC());
    }

    IEnumerator SatelliteTraveling()
    {
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / satelliteTravelingTime;

            satelliteProjector.transform.localPosition = Vector3.Lerp(satelliteStartPos, satelliteTargetPos, t);

            yield return null;
        }
    }

    IEnumerator ScanC()
    {
        gridProjector.material.SetFloat("_Reveal", 0);
        float aspectRatio = width * 2 / gridProjector.orthographicSize;
        gridProjector.aspectRatio = aspectRatio;

        yield return new WaitForSeconds(showGridDelay);

        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / showGridProgressTime;

            gridProjector.material.SetFloat("_Reveal", t);

            yield return null;
        }

        //Detect player
        Vector3 toPlayer = CharacterControls.Position - transform.position;
        if(Mathf.Abs(toPlayer.x)<width * 0.5f)
        {
            GameManager.Instance.GameOver("You have been seen");
        }

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.3f;

            gridProjector.aspectRatio = Curves.QuadEaseInOut(aspectRatio, 0, Mathf.Clamp01(t));

            yield return null;
        }

        gridProjector.material.SetFloat("_Reveal", 0);
    }
}
