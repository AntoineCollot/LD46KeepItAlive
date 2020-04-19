using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FailureManager : MonoBehaviour
{
    public enum FailureType { WheelFR, WheelMR, WheelBR, WingL }

    [SerializeField] Transform[] failureSpots = null;
    [SerializeField] Failure failurePrefab = null;
    [SerializeField] Animator roverAnim = null;
    [SerializeField] HeadLook headLook= null;
    List<Failure> onGoingFailures = new List<Failure>();
    float lastClearTime;
    [SerializeField] float minTimeSinceClear = 3;

    StateToken stopRoverToken;

    public bool HasFailure
    {
        get
        {
            return onGoingFailures.Count > 0;
        }
    }

    public static FailureManager Instance;

    private void Awake()
    {
        Instance =this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stopRoverToken = new StateToken();
        MoveRover.Instance.stopState.Add(stopRoverToken);
        GameManager.Instance.onGameStart.AddListener(StartFailureLoop);
    }

    void StartFailureLoop()
    {
        StartCoroutine(FailureLoop());
    }

    public void FailureCleared(Failure failure)
    {
        onGoingFailures.Remove(failure);

        if(onGoingFailures.Count==0)
        {
            stopRoverToken.isOn = false;
            MoveRover.Instance.Go();
            roverAnim.SetTrigger("Happy");
            RoverAudio.Instance.StopLoop();
            RoverAudio.Instance.PlayClip(5);

            if (headLook.lookState != HeadLook.LookState.ScanFixe && headLook.lookState != HeadLook.LookState.ScanCharacter)
                headLook.lookState = HeadLook.LookState.Forward;

            VoicesManager.Instance.PlayFixClip();

            lastClearTime = Time.time;
        }
    }

    IEnumerator FailureLoop()
    {
        yield return new WaitForSeconds(5);

        while(GameManager.isPlaying)
        {
            if(Time.time> lastClearTime+minTimeSinceClear && Random.Range(0f, 1f)<Time.deltaTime/ GameProgress.AverageFailureTime)
            {
                //Failure
                FailureType type = (FailureType)Random.Range(0, System.Enum.GetValues(typeof(FailureType)).Length);

                //If there are no failure of this type on going
                if(!onGoingFailures.Exists(f=>f.type == type))
                {
                    print("Failure " + type);

                    stopRoverToken.isOn = true;
                    MoveRover.Instance.Stop();

                    //Find the pos
                    Vector3 pos = failureSpots[type.GetHashCode()].position;

                    Failure newFailure = Instantiate(failurePrefab, pos, Quaternion.identity,transform);
                    onGoingFailures.Add(newFailure);
                    newFailure.Init(type, roverAnim);

                    if(headLook.lookState != HeadLook.LookState.ScanFixe && headLook.lookState != HeadLook.LookState.ScanCharacter)
                        headLook.SetLookWorldTarget(pos);

                    //If the failure we added is the only one
                    if(onGoingFailures.Count==1)
                    {
                        VoicesManager.Instance.PlayNextFailureClip();
                    }
                }
            }

            yield return null;
        }
    }
}
