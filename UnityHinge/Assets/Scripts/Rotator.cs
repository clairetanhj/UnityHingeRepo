﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rotator : MonoBehaviour
{
    public float maxbound;
    public float minbound;
    
    private float angle;
    private float newangle;
    private float origin;
    private float neworigin;
    private float maxangle;
    private float minangle;

    void Start()
    {
        origin = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        neworigin = angle = newangle = origin;
        maxangle = origin + maxbound;
        minangle = origin + minbound;

        Debug.Log("Origin: " + origin);
    }

    void Update()
    {   
        if(origin != neworigin) {
            origin = neworigin;
            Debug.Log("Origin: " + origin);
            maxangle = origin + maxbound;
            minangle = origin + minbound;
        }        

        if(Input.GetAxisRaw("Vertical") == 1 && angle < maxangle) {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }

        if(Input.GetAxisRaw("Vertical") == -1 && angle > minangle) {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
   
        if(Input.GetButton("Jump")) {
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        }

        newangle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        
        if(angle != newangle) {
            angle = newangle;
            Debug.Log("Angle: " + angle);
        }

    }

    public void SetOrigin() {
        neworigin = angle;
    }
}
