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

    StateToken roverScanningToken;

    [Header("Antena Scan")]
    [SerializeField] AnteneScanLine antenaScan = null;
    [SerializeField] float firstAntenaScanTime = 20;
    StateToken antenaScanningToken;

    [Header("Satellite Scan")]
    [SerializeField] SateliteScan satelliteScan = null;
    [SerializeField] float firstSatelliteScanTime = 45;

    // Start is called before the first frame update
    void Start()
    {
        roverScanningToken = new StateToken();
        antenaScanningToken = new StateToken();
        MoveRover.Instance.stopState.Add(roverScanningToken);
        //MoveRover.Instance.stopState.Add(antenaScanningToken);
        GameManager.Instance.onGameStart.AddListener(StartScanning);
    }

    void StartScanning()
    {
        StartCoroutine(RoverScanLoop());
        StartCoroutine(AntenaScanLoop());
    }

    IEnumerator AntenaScanLoop()
    {
        yield return new WaitForSeconds(firstAntenaScanTime);

        while(GameManager.isPlaying)
        {
            print("Start Antena Scan");
            antenaScanningToken.isOn = true;
            yield return new WaitForSeconds(antenaScan.Scan());
            antenaScanningToken.isOn = false;

            yield return new WaitForSeconds(GameProgress.AntenaScanInterval);
        }
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

        roverScanningToken.isOn = true;
        MoveRover.Instance.Stop();

        scanLines.Scan();
        yield return new WaitForSeconds(roverScanProgressTime);

        if(IsPlayerInView(45))
        {
            GameManager.Instance.GameOver("You have been seen");
        }
        scanLines.HideAll(roverHideScanAnimTime);

        yield return new WaitForSeconds(roverHideScanAnimTime);

        roverScanningToken.isOn = false;
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

        if (Vector3.Angle(toPlayer, lookDir) < angle * 0.65f)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(raycastOrigin, toPlayer), out hit,18, playerScanLayers))
            {
                if (hit.collider.gameObject.tag == "Character")
                {
                    return true;
                }
            }
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.green;

        if (IsPlayerInView(45))
            Gizmos.color = Color.red;

        Gizmos.DrawLine(roverScanOrigin.position, CharacterControls.Position);
    }
#endif
}
