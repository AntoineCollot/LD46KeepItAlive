using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverScanLines : MonoBehaviour
{
    [SerializeField] LayerMask RaycastLayers;
    [SerializeField] Projector[] projectors;
    [SerializeField] float maxDistance = 10;
    [SerializeField] float projectionProgressTime = 0.5f;
    [SerializeField] float scanTime = 1;
    [SerializeField] Transform head = null;
    float defaultAspectRatio;

    private void Start()
    {
        foreach (Projector p in projectors)
        {
            p.material.SetFloat("_Reveal", 0);
        }

        defaultAspectRatio = projectors[0].aspectRatio;
    }

    public void Scan()
    {
        RoverAudio.Instance.PlayClip(1);
        foreach (Projector p in projectors)
        {
            p.aspectRatio = defaultAspectRatio;
        }
        StartCoroutine(ScanC(scanTime));
    }

    public void HideAll(float time)
    {
        StartCoroutine(HideAllC(time));
    }

    IEnumerator HideAllC(float time)
    {
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / time;
            float a = Curves.QuadEaseInOut(defaultAspectRatio, 0, Mathf.Clamp01(t));

            foreach (Projector p in projectors)
            {
                p.aspectRatio = a;
            }

            yield return null;
        }

        foreach (Projector p in projectors)
        {
            p.material.SetFloat("_Reveal", 0);
        }
    }

    IEnumerator ScanC(float maxTime)
    {
        foreach (Projector p in projectors)
        {
            Vector3 origin = transform.position;
            origin.y = 1;

            Vector3 dir = p.transform.parent.forward;
            dir.y = 0;
            dir.Normalize();

            float hit = RaycastScan(new Ray(origin, dir));

            StartCoroutine(CastProjector(p, hit, projectionProgressTime));
            yield return new WaitForSeconds(maxTime / projectors.Length);
        }
    }

    IEnumerator CastProjector(Projector p, float limit, float time)
    {
        float t = 0;

        while(t< limit)
        {
            t += Time.deltaTime / time;
            p.material.SetFloat("_Reveal", t);

            yield return null;
        }
    }

    float RaycastScan(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, RaycastLayers))
        {
            return hit.distance / maxDistance;
        }
        else
        {
            return 1;
        }
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.up * head.eulerAngles.y;
    }
}
