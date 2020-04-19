using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour
{
    public enum LookState {Character, Forward, ScanFixe, ScanCharacter, Target, None }
    public LookState lookState = LookState.Forward;

    [SerializeField] Transform neck = null;
    [SerializeField] Transform head = null;
    [SerializeField] float lookSmooth = 0.3f;

    Vector3 lookTarget;
    Vector3 lookPosition;
    Vector3 refLookPosition;

    public Vector3 FlatDirection
    {
        get
        {
            Vector3 dir = head.forward;
            dir.y = 0;
            return dir;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        switch (lookState)
        {
            case LookState.Forward:
                //Follow the character if it goes in front
                Vector3 toCharacter = CharacterControls.Position - neck.position;
                toCharacter.y = 0;
                if(Vector3.Angle(toCharacter,Vector3.right)<10)
                {
                    lookState = LookState.Character;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Rotate the head toward destination
    /// </summary>
    void LateUpdate()
    {
        Vector3 lookPositionTarget = lookPosition;

        switch (lookState)
        {
            case LookState.Character:
                lookPositionTarget = CharacterControls.Position;
                lookTarget = lookPositionTarget - neck.position;
                break;
            case LookState.Forward:
                lookPositionTarget = neck.position + Vector3.right*5;
                break;
            case LookState.ScanFixe:
            case LookState.Target:
            case LookState.ScanCharacter:
                lookPositionTarget = neck.position + lookTarget;
                break;
            case LookState.None:
                break;
            default:
                break;
        }

        lookPosition = Vector3.SmoothDamp(lookPosition, lookPositionTarget, ref refLookPosition, lookSmooth);

        Vector3 currentLookPos = lookPosition;
        Vector3 neckToPos = currentLookPos - neck.position;
        neckToPos.y = 0;
        if (neckToPos.sqrMagnitude < 1)
        {
            currentLookPos = neck.position + neckToPos.normalized;
            currentLookPos.y= lookPosition.y;
        }

        neck.LookAt(Vector3.Lerp(neck.position, currentLookPos, 0.5f));
        head.LookAt(currentLookPos);
    }

    public void LookRandomDirection()
    {
        Vector3 targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        SetLookRelativeTarget(targetDirection * 5);
    }

    public void SetLookRelativeTarget(Vector3 position)
    {
        lookState = LookState.Target;
        lookTarget = position;
    }

    public void SetLookWorldTarget(Vector3 position)
    {
        lookState = LookState.Target;
        lookTarget = position - neck.position;
    }
}
