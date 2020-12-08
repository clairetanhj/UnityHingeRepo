using System.Collections;
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
        if (System.IO.File.Exists(Application.dataPath + "/save.json")) {
            string saveString = System.IO.File.ReadAllText(Application.dataPath + "/save.json");
            SaveAngle loadedData = JsonUtility.FromJson<SaveAngle>(saveString);
            origin = loadedData.savedOrigin;
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        } else {
            origin = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        }
        
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

            SaveAngle saveAngle = new SaveAngle { savedOrigin = origin };
            string json = JsonUtility.ToJson(saveAngle);
            System.IO.File.WriteAllText(Application.dataPath + "/save.json", json);
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

    private class SaveAngle {
        public float savedOrigin;
    }
}
