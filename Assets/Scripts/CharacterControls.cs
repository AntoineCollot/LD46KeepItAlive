using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    [SerializeField] Vector2 speed=Vector2.one;

    public static CharacterControls Instance;

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

        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}
