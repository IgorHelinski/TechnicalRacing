using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public Transform camHolder;
    public float smoothness;
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    public enum RelativePosition { InitalPosition, gunnerPos }
    public RelativePosition relativePosition;
    public Vector3 gunnerPos;
    public Transform gunnerTransform;

    void Start()
    {
        relativePosition = RelativePosition.InitalPosition;
        initalOffset = camHolder.position - targetObject.position;
    }

    void FixedUpdate()
    {
        gunnerPos = gunnerTransform.position;

        Vector3 camOffset = CameraOffset(relativePosition);
        cameraPosition = targetObject.position + camOffset;

        camHolder.position = Vector3.Lerp(camHolder.position, cameraPosition, smoothness * Time.fixedDeltaTime);
        camHolder.rotation = Quaternion.Slerp(camHolder.rotation, targetObject.rotation, smoothness * Time.fixedDeltaTime);

        if (relativePosition == RelativePosition.gunnerPos)
        {
            camHolder.position = gunnerTransform.position;
        }
    }

    Vector3 CameraOffset(RelativePosition ralativePos)
    {
        Vector3 currentOffset;

        switch (ralativePos)
        {
            case RelativePosition.gunnerPos:
                currentOffset = gunnerPos;
                break;

            default:
                currentOffset = initalOffset;
                break;
        }
        return currentOffset;
    }
}
