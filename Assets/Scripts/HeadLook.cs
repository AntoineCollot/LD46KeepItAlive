using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour
{
    public enum LookState {Character, Forward, Scan, Target, None }
    public LookState lookState = LookState.Forward;

    [SerializeField] Transform neck = null;
    [SerializeField] Transform head = null;
    [SerializeField] float lookSmooth = 0.3f;

    Vector3 lookTarget;
    Vector3 lookPosition;
    Vector3 refLookPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookPositionTarget = lookPosition;

        switch (lookState)
        {
            case LookState.Character:
                lookPositionTarget = CharacterControls.Position;
                break;
            case LookState.Forward:
                lookPositionTarget = neck.position + Vector3.right*3;
                break;
            case LookState.Scan:
                break;
            case LookState.Target:
                lookPositionTarget = lookTarget;
                break;
            case LookState.None:
                break;
            default:
                break;
        }

        lookPosition = Vector3.SmoothDamp(lookPosition, lookPositionTarget, ref refLookPosition, lookSmooth);
        Debug.DrawLine(neck.position, lookPosition,Color.red);

        neck.LookAt(Vector3.Lerp(neck.position, lookPosition, 0.5f));
        head.LookAt(lookPosition);
    }

    public void LookRandomDirection()
    {
        Vector3 targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        SetLookTarget(neck.position + targetDirection * 3);
    }

    public void SetLookTarget(Vector3 position)
    {
        lookState = LookState.Target;
        lookTarget = position;
    }
}
