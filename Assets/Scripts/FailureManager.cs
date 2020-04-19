using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FailureManager : MonoBehaviour
{
    public enum FailureType { WheelFR, WheelMR, WheelBR, WingL }

    [SerializeField] Transform[] failureSpots = null;
    [SerializeField] Failure failurePrefab = null;
    [SerializeField] float averageTimeToFailure = 5;
    [SerializeField] Animator roverAnim;
    List<Failure> onGoingFailures = new List<Failure>();

    StateToken stopRoverToken;

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
        }
    }

    IEnumerator FailureLoop()
    {
        yield return new WaitForSeconds(5);

        while(GameManager.isPlaying)
        {
            if(Random.Range(0f, 1f)<Time.deltaTime/ averageTimeToFailure)
            {
                //Failure
                FailureType type = (FailureType)Random.Range(0, System.Enum.GetValues(typeof(FailureType)).Length);

                //If there are no failure of this type on going
                if(!onGoingFailures.Exists(f=>f.type == type))
                {
                    stopRoverToken.isOn = true;
                    MoveRover.Instance.Stop();

                    //Find the pos
                    Vector3 pos = failureSpots[type.GetHashCode()].position;

                    Failure newFailure = Instantiate(failurePrefab, pos, Quaternion.identity,transform);
                    onGoingFailures.Add(newFailure);
                    newFailure.Init(type, roverAnim);
                }

            }
        }
    }
}
