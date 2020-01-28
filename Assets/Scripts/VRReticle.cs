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
            }
        }
        else
        {
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
