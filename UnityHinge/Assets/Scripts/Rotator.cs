using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rotator : MonoBehaviour
{
    public float maxangle;
    public float minangle;
    
    private float angle;
    private float newangle;
    private float origin;
    private float neworigin;

    void Start()
    {
        origin = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        angle = origin;
        neworigin = origin;
        newangle = angle;

        Debug.Log("Origin: " + origin);
    }

    void Update()
    {           
        if(Input.GetAxisRaw("Vertical") == 1 && angle < origin+maxangle) {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }

        if(Input.GetAxisRaw("Vertical") == -1 && origin+minangle < angle) {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
   
        if(Input.GetButton("Jump")){
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        newangle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        
        if(origin != neworigin) {
            origin = neworigin;
            Debug.Log("Origin: " + origin);
        }
        if(angle != newangle) {
            angle = newangle;
            Debug.Log("Angle: " + angle);
        }
    }

    public void SetOrigin() {
        neworigin = angle;
    }
}
