using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    [SerializeField] Vector2 speed=Vector2.one;
    [SerializeField] Transform[] scanOrigins = null;
    [SerializeField] LayerMask obstacleLayer = 0;
    [SerializeField] float maxZ = 20;

    Camera cam;
    Animator anim;
    new Rigidbody rigidbody;
    public static CharacterControls Instance;

    public State stopCharacterState = new State();

    public static Vector3 Position
    {
        get
        {
            return Instance.transform.position;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        cam = Camera.main;
        GameManager.Instance.onGameOver.AddListener(Stop);
    }

    void Stop()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(inputs.sqrMagnitude>1)
            inputs.Normalize();

        Vector3 movement = Vector3.zero;
        movement.x = inputs.x * speed.x;
        movement.z = inputs.y * speed.y;

        if (!stopCharacterState.IsOn)
        {
            anim.SetFloat("MoveSpeed", movement.magnitude);

            Vector3 targetPos = rigidbody.position + movement * Time.deltaTime;
            ClampToViewport(ref targetPos);

            //clamp in depth
            targetPos.z = Mathf.Min(targetPos.z, maxZ);

            rigidbody.position = targetPos;

            anim.SetBool("IsFacingBack", false);
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);

            anim.SetBool("IsFacingBack", scanOrigins[0].position.z > transform.position.z);
        }

        HiddenStateAnim();
        anim.SetBool("IsFixing", stopCharacterState.IsOn);
    }

    void HiddenStateAnim()
    {
        bool hidden = true;
        foreach(Transform scanOrigin in scanOrigins)
        {
            Vector3 toScanOrigin = scanOrigin.position - transform.position;
            toScanOrigin.y = 0;

            Vector3 origin = transform.position;
            origin.y = 1;

            if (!Physics.Raycast(new Ray(origin, toScanOrigin), toScanOrigin.magnitude, obstacleLayer))
            {
                hidden = false;
                break;
            }
        }

        anim.SetBool("IsHidden", hidden);
    }

    void ClampToViewport(ref Vector3 pos)
    {
        pos = cam.WorldToViewportPoint(pos);
        pos.x = Mathf.Clamp(pos.x,0.03f, 0.97f);
        pos.y = Mathf.Clamp01(pos.y);
        pos = Camera.main.ViewportToWorldPoint(pos);
    }
}
