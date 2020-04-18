using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanManager : MonoBehaviour
{
    public enum ScanType { RoverScan, SatelliteScan}

    [Header("Rover Scan")]
    [SerializeField] Transform roverScanOrigin = null;
    [SerializeField] HeadLook headLook = null;
    [SerializeField] Projector roverScanProjector;

    [SerializeField] float roverScanMinInterval = 2;
    [SerializeField] float roverAverageScanInterval = 10;
    [SerializeField] float roverScanProgressTime = 3;
    [SerializeField] float roverScanPauseTime = 2;
    [SerializeField] float roverPostScanPauseTime = 1;
    [SerializeField] float roverHideScanAnimTime = 0.3f;
    [SerializeField] float roverScanAngle = 45;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoverScanLoop());
    }

    IEnumerator RoverScanLoop()
    {
        float lastScanTimer = 0;
        while(true)
        {
            lastScanTimer += Time.deltaTime / roverScanMinInterval;
            if(lastScanTimer>1)
            {
                //Check if we should scan
                bool scan = Random.Range(0f, 1f) < Time.deltaTime / roverAverageScanInterval;
                print(scan + " ; " + Time.deltaTime / roverAverageScanInterval);
                if (scan)
                {
                    //yield return StartCoroutine(PerformRoverScan());
                    lastScanTimer = 0;
                }
            }
        }
    }

    IEnumerator PerformRoverScan()
    {
        print("Rover Scan");
        switch (headLook.lookState)
        {
            case HeadLook.LookState.Character:
                break;
            case HeadLook.LookState.Forward:
            case HeadLook.LookState.Target:
                //Ask to look in a random direction
                headLook.LookRandomDirection();
                yield return new WaitForSeconds(1);
                break;
            default:
                break;
            case HeadLook.LookState.Scan:
            case HeadLook.LookState.None:
                yield break;
        }

        headLook.lookState = HeadLook.LookState.Scan;

        //The projector aspect ratio
        float aspectRatio = 45 / roverScanAngle;
        roverScanProjector.aspectRatio = aspectRatio;

        float scanProgress = 0;
        while(scanProgress<1)
        {
            scanProgress += Time.deltaTime / roverScanProgressTime;

            roverScanProjector.material.SetFloat("_Reveal", scanProgress);

            yield return null;
        }

        yield return new WaitForSeconds(roverScanPauseTime);

        float hideScanAnimProgress = 0;
        while(hideScanAnimProgress<1)
        {
            hideScanAnimProgress += Time.deltaTime / roverHideScanAnimTime;
            roverScanProjector.aspectRatio = Curves.QuadEaseInOut(aspectRatio, 0, Mathf.Clamp01(hideScanAnimProgress));
            yield return null;
        }

        roverScanProjector.material.SetFloat("_Reveal", 0);

        yield return new WaitForSeconds(roverPostScanPauseTime);

        headLook.lookState = HeadLook.LookState.Forward;
        print("Rover Scan Finished");
    }
}
