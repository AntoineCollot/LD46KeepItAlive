using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : MonoBehaviour
{
    public FailureManager.FailureType type { get; private set; }
    Animator roverAnim;

    [SerializeField] GameObject commandPrompt = null;
    bool playerInContact;

    public void Init(FailureManager.FailureType type, Animator roverAnim)
    {
        this.type = type;
        this.roverAnim = roverAnim;

        SetAnim(true);
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
                Cleared();
            }
        }
    }
}
