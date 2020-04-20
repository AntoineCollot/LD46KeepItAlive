using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnteneScanLine : MonoBehaviour
{
    [SerializeField] Projector projector = null;
    [SerializeField] LayerMask raycastLayers;
    [SerializeField] float maxDistance = 10;
    [SerializeField] float startUpTime = 0.5f;
    [SerializeField] float scanTime = 3;
    [SerializeField] Transform antenaBone = null;
    float defaultAspectRatio;

    // Start is called before the first frame update
    void Start()
    {
        defaultAspectRatio = projector.aspectRatio;
        projector.material.SetFloat("_Reveal", 0);
    }

    public float Scan()
    {
        RoverAudio.Instance.PlayClip(2);
        StartCoroutine(ScanC());
        return startUpTime + scanTime + 0.3f;
    }

    IEnumerator ScanC()
    {
        projector.aspectRatio = defaultAspectRatio;
        yield return StartCoroutine(CastProjector(startUpTime));

        yield return StartCoroutine(Rotate(scanTime));

        yield return StartCoroutine(HideC(0.3f));
    }

    IEnumerator Rotate(float time)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            transform.localEulerAngles = Vector3.up * Curves.QuadEaseInOut(0, 360, Mathf.Clamp01(t));

            //Cast the ray
            Vector3 dir = transform.forward;
            dir.y = 0;
            dir.Normalize();

            antenaBone.LookAt(antenaBone.position + dir);

            Vector3 origin = transform.position;
            origin.y = 1;

            RaycastHit hit;
            if (Physics.Raycast(new Ray(origin, dir), out hit, projector.orthographicSize*2, raycastLayers))
            {
                if (hit.collider.tag == "Character")
                {
                    //Try another raycast to the center of the player to check if the player is hidden
                    Vector3 toPlayer = CharacterControls.Position - origin;
                    toPlayer.y = 0;

                    RaycastHit playerHit;
                    if (Physics.Raycast(new Ray(origin, toPlayer), out playerHit, projector.orthographicSize * 2, raycastLayers))
                    {
                        if (hit.collider.tag == "Character")
                        {
                            GameManager.Instance.GameOver("You have been seen");
                            VoicesManager.Instance.PlayGameOverClip(true);
                        }
                    }
                }
                else
                {
                    projector.material.SetFloat("_Reveal", hit.distance / (projector.orthographicSize * 2));
                }
            }            
            else
            {
                projector.material.SetFloat("_Reveal", 1);
            }
            yield return null;
        }
    }

    IEnumerator CastProjector(float time)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            projector.material.SetFloat("_Reveal", t);

            yield return null;
        }
    }

    IEnumerator HideC(float time)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            float a = Curves.QuadEaseInOut(defaultAspectRatio, 0, Mathf.Clamp01(t));

            projector.aspectRatio = a;

            yield return null;
        }

        projector.material.SetFloat("_Reveal", 0);
    }
}
