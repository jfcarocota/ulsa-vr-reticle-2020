using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRReticle : MonoBehaviour
{
    [SerializeField]
    Transform recticleStartPoint;

    [SerializeField]
    Transform reticle;

    [SerializeField]
    Vector3 initSize;

    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField, Range(0.1f, 100f)]
    float rayDistance = 50f;
    [SerializeField]
    LayerMask hitLayer;

    RaycastHit hit;

    [SerializeField]
    Color colorNormal;
    [SerializeField]
    Color colorHit;
    Renderer renderTarget;

    void Start() 
    {
        reticle.localPosition = recticleStartPoint.localPosition;     
        initSize = reticle.localScale;
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(recticleStartPoint.position, transform.forward, out hit, rayDistance, hitLayer))
        {
            if(hit.collider)
            {
                reticle.parent = null;
                reticle.position = hit.point;
                reticle.localScale = initSize * hit.distance;
                reticle.rotation = Quaternion.LookRotation(hit.normal);

                renderTarget = hit.collider.GetComponent<Renderer>();
                renderTarget.material.SetColor("_Albedo", colorHit);
            }
        }
        else
        {
            if(renderTarget)
            {
                renderTarget.material.SetColor("_Albedo", colorNormal);
                renderTarget = null;
            }

            reticle.parent = recticleStartPoint;
            reticle.localRotation = Quaternion.identity;
            reticle.localScale = initSize;
            reticle.localPosition = Vector3.zero;
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(recticleStartPoint.position, transform.forward * rayDistance);
    }
}
