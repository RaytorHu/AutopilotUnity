using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     public Transform target;
 
     public Vector3 offsetPosition;
 
     [Range(0,20)]public float smothTime = 5;
 
 
     private void FixedUpdate()
     {
         transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offsetPosition), smothTime * Time.deltaTime);
 
        transform.LookAt(target);
     }
}
