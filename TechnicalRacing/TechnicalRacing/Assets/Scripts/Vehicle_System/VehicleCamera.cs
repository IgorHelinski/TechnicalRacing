using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public Transform TargetObject;
    public float smoothness;

    private void FixedUpdate()
    {
        Vector3 offset = TargetObject.position - transform.position;

        Vector3 newPos = transform.position + offset;
        Quaternion newRot = TargetObject.rotation;

        transform.position = Vector3.Lerp(transform.position, newPos, smoothness * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, smoothness * Time.deltaTime);
    }
}
