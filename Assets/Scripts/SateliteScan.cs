using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteScan : MonoBehaviour
{
    [Header("Scan")]
    [SerializeField] float maxDistanceFromRover = 10;
    [SerializeField] Transform centerPoint=null;

    [Header("Grid")]
    [SerializeField] Projector gridProjector=null;
    [SerializeField] float showGridDelay = 1;
    [SerializeField] float showGridProgressTime = 1f;
    [SerializeField] float showGridPauseTime = 1f;

    [Header("Satelite")]
    [SerializeField] Projector satelliteProjector = null;
    [SerializeField] Vector3 satelliteStartPos = Vector3.zero;
    [SerializeField] Vector3 satelliteTargetPos = Vector3.zero;
    [SerializeField] float satelliteTravelingTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        gridProjector.material.SetFloat("_Reveal", 1);
        gridProjector.aspectRatio = 0;
    }

    public float Scan()
    {
        //Place the satellite
        Vector3 pos = transform.position;
        pos.x = centerPoint.position.x + Random.Range(-maxDistanceFromRover, maxDistanceFromRover);
        transform.position = pos;

        StartCoroutine(SatelliteTraveling());
        StartCoroutine(ScanC());

        return showGridDelay + showGridProgressTime *2 + showGridPauseTime;
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
        float width = GameProgress.SatteliteScanWidth;
        float aspectRatio = width / (gridProjector.orthographicSize * 2);
        gridProjector.aspectRatio = 0;

        yield return new WaitForSeconds(showGridDelay);

        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / showGridProgressTime;

            gridProjector.aspectRatio = Curves.QuadEaseInOut(0, aspectRatio, Mathf.Clamp01(t));

            yield return null;
        }

        yield return new WaitForSeconds(showGridPauseTime);

        //Detect player
        Vector3 toPlayer = CharacterControls.Position - transform.position;
        if(Mathf.Abs(toPlayer.x)<width * 0.5f)
        {
            GameManager.Instance.GameOver("You have been seen");
        }

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / showGridProgressTime;

            gridProjector.aspectRatio = Curves.QuadEaseInOut(aspectRatio, 0, Mathf.Clamp01(t));

            yield return null;
        }
    }
}
