using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset; 
    public float followSpeed = 2.0f;
    public float rotationSpeed = 5.0f; 

    void Update()
    {
        Vector3 newPosition = target.TransformPoint(offset);  
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        Quaternion toRotation = Quaternion.LookRotation(target.forward, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}