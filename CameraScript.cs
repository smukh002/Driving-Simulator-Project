using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{
    // movement amount trying to capture
    [SerializeField] private Vector3 move;
    // target we are trying to follow
    [SerializeField] private Transform car;
    [SerializeField] private float xspeed;
    [SerializeField] private float rspeed;

    private void FixedUpdate(){
        Translation();
        Rotation();
    }
   
    private void Translation(){
        var target = car.TransformPoint(move);
        //Vector3 Lerp(Vector3 a, Vector3 b, float t);
        float t = xspeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, t);
    }
    private void Rotation(){
        var direction = car.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        float z = rspeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, z);
    }
}
