using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : MonoBehaviour
{
    public FailureManager.FailureType type { get; private set; }
    Animator roverAnim;

    [SerializeField] GameObject commandPrompt = null;
    [SerializeField] GameObject criticalFX = null;
    [SerializeField] float fixingTime = 1.5f;
    [SerializeField] float timeBeforeCriticalState = 3;
    [SerializeField] float timeBeforeGameOver = 3;
    bool playerInContact;
    bool isBeingFixed;

    StateToken stopCharacterToken;
    StateToken shakeToken;

    public void Init(FailureManager.FailureType type, Animator roverAnim)
    {
        this.type = type;
        this.roverAnim = roverAnim;
        stopCharacterToken = new StateToken();
        shakeToken = new StateToken();
        Shake.Instance.criticalShakeState.Add(shakeToken);

        SetAnim(true);

        StartCoroutine(FailureIntensify());
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInContact = true;
        commandPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        playerInContact = false;
        commandPrompt.SetActive(false);
    }

    public void Cleared()
    {
        SetAnim(false);
        FailureManager.Instance.FailureCleared(this);
        Destroy(gameObject);
    }

    void SetAnim(bool value)
    {
        //Anim
        switch (type)
        {
            case FailureManager.FailureType.WheelFR:
                roverAnim.SetBool("WheelFR_Broken", value);
                break;
            case FailureManager.FailureType.WheelMR:
                roverAnim.SetBool("WheelMR_Broken", value);
                break;
            case FailureManager.FailureType.WheelBR:
                roverAnim.SetBool("WheelBR_Broken", value);
                break;
            case FailureManager.FailureType.WingL:
                roverAnim.SetBool("WingL_Broken", value);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if(playerInContact)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CharacterControls.Instance.stopCharacterState.Add(stopCharacterToken);
                stopCharacterToken.isOn = true;
                isBeingFixed = true;
                Invoke("FinishFixing", fixingTime);
                AlienAudio.Instance.PlayCip(2);
            }
        }
    }

    void FinishFixing()
    {
        Cleared();
        stopCharacterToken.isOn = false;
        CharacterControls.Instance.stopCharacterState.Remove(stopCharacterToken);
        shakeToken.isOn = false;
        Shake.Instance.criticalShakeState.Remove(shakeToken);
    }

    IEnumerator FailureIntensify()
    {
        RoverAudio.Instance.PlayLoop(3);

        yield return new WaitForSeconds(timeBeforeCriticalState);

        if (isBeingFixed)
            yield break;

        shakeToken.isOn = true;
        criticalFX.SetActive(true);
        RoverAudio.Instance.PlayLoop(4);

        yield return new WaitForSeconds(timeBeforeGameOver);

        if (isBeingFixed)
            yield break;
        GameManager.Instance.GameOver("You failed to fix the rover in time");
    }
}
