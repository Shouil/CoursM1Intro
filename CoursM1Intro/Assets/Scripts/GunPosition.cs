using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPosition : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera;
    void Update()
    {
        distanceFromCamera = 1;
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = resultingPosition;
    }
}
