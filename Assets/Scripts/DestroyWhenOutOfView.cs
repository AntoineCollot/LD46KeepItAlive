using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOutOfView : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Camera cam = Camera.main;
        if (Camera.main != null)
        {
            //If on the left of the cam
            if (transform.position.x < cam.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
    }
}
