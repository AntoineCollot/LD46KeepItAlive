using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanManager : MonoBehaviour
{
    public enum ScanType { RoverScan, SatelliteScan}

    [Header("Rover Scan")]
    [SerializeField] Transform roverScanOrigin = null;
    [SerializeField] HeadLook headLook = null;
    [SerializeField] RoverScanLines scanLines;

    [SerializeField] float roverScanMinInterval = 2;
    [SerializeField] float roverAverageScanInterval = 10;
    [SerializeField] float roverScanProgressTime = 3;
    [SerializeField] float roverPostScanPauseTime = 1;
    [SerializeField] float roverHideScanAnimTime = 0.3f;

    [SerializeField] LayerMask playerScanLayers=0;

    StateToken stopRoverToken;

    // Start is called before the first frame update
    void Start()
    {
        stopRoverToken = new StateToken();
        MoveRover.Instance.stopState.Add(stopRoverToken);
        GameManager.Instance.onGameStart.AddListener(StartScanning);
    }

    void StartScanning()
    {
        StartCoroutine(RoverScanLoop());
    }

    IEnumerator RoverScanLoop()
    {
        float lastScanTimer = 0;
        while(GameManager.isPlaying)
        {
            lastScanTimer += Time.deltaTime / roverScanMinInterval;
            if(lastScanTimer>1)
            {
                //Check if we should scan
                bool scan = Random.Range(0f, 1f) < Time.deltaTime / roverAverageScanInterval;
                if (scan)
                {
                    yield return StartCoroutine(PerformRoverScan());
                    lastScanTimer = 0;
                }
            }

            yield return null;
        }
    }

    IEnumerator PerformRoverScan()
    {
        print("Rover Scan");
        switch (headLook.lookState)
        {
            case HeadLook.LookState.Character:
                headLook.lookState = HeadLook.LookState.ScanCharacter;
                break;
            case HeadLook.LookState.Forward:
            case HeadLook.LookState.Target:
                //Ask to look in a random direction
                headLook.LookRandomDirection();
                yield return new WaitForSeconds(0.5f);
                headLook.lookState = HeadLook.LookState.ScanFixe;
                break;
            default:
            case HeadLook.LookState.ScanFixe:
            case HeadLook.LookState.None:
                yield break;
        }

        stopRoverToken.isOn = true;
        MoveRover.Instance.Stop();

        scanLines.Scan();
        yield return new WaitForSeconds(roverScanProgressTime);

        if(IsPlayerInView(45))
        {
            GameManager.Instance.GameOver();
        }
        scanLines.HideAll(roverHideScanAnimTime);

        yield return new WaitForSeconds(roverHideScanAnimTime);

        stopRoverToken.isOn = false;
        MoveRover.Instance.Go();

        yield return new WaitForSeconds(roverPostScanPauseTime);

        headLook.lookState = HeadLook.LookState.Forward;
        print("Rover Scan Finished");
    }

    public bool IsPlayerInView(float angle)
    {
        Vector3 toPlayer = CharacterControls.Position - roverScanOrigin.position;
        toPlayer.y = 0;
        Vector3 lookDir = headLook.FlatDirection;

        Vector3 raycastOrigin = roverScanOrigin.position;
        raycastOrigin.y = 1;

        if (Vector3.Angle(toPlayer, lookDir) < angle)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(raycastOrigin, toPlayer), out hit, playerScanLayers))
            {
                if (hit.collider.gameObject.tag == "Character")
                {
                    return true;
                }
            }
        }
        return false;
    }
}
