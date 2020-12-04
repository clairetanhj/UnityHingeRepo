using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float maxangle;
    public float minangle;
    
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0.0f;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetAxisRaw("Vertical") == 1 && angle < maxangle) {
            transform.Rotate(0.8f, 0.0f, 0.0f);
            angle += 0.8f;
        }

        if(Input.GetAxisRaw("Vertical") == -1 && minangle < angle) {
            transform.Rotate(-0.8f, 0.0f, 0.0f);
            angle -= 0.8f;
        }
   
        if(Input.GetButton("Jump")){
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            angle = 0.0f;
        }
        
        if(angle > 180.0f) {angle -= 360.0f;}
        if(angle < -180.0f) {angle += 360.0f;}
        print(angle);
    }
}
