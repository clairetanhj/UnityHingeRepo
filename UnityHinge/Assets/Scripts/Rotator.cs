using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
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
        
        if(Application.isPlaying)
        {
            origin = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        } else {
            Debug.Log("origin" + origin);
            Debug.Log("angle" + angle);
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        }

        neworigin = angle = origin;
        maxangle = origin + maxbound;
        minangle = origin + minbound;

        Debug.Log("Origin: " + origin);
        Debug.Log("start");
    }

    void Update()
    {   
        if(origin != neworigin) {
            origin = neworigin;
            Debug.Log("Origin: " + origin);
            maxangle = origin + maxbound;
            minangle = origin + minbound;
        }        

        // EventType.KeyDown
        // Event.keyCode

        if(Input.GetKey(KeyCode.UpArrow) && angle < maxangle) {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }

        if(Input.GetKey(KeyCode.DownArrow) && angle > minangle) {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
   
        if (Input.GetKey(KeyCode.Space)) {
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        }

        newangle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        
        if(angle != newangle) {
            angle = newangle;
            Debug.Log("Angle: " + angle);
        }
    }

    // public void Clockwise() {
    //     transform.Rotate(1.0f, 0.0f, 0.0f);
    // }

    public void SetOrigin() {
        neworigin = angle;
    }
}
