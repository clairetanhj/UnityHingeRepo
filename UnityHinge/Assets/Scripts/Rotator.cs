using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rotator : MonoBehaviour
{
    public float maxbound;
    public float minbound;
    public float angleFromOrigin;
    
    private float angle;
    private float newangle;
    private float origin;
    private float neworigin;

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

        Debug.Log("Origin: " + origin);
    }

    void Update()
    {   
        if(origin != neworigin) {
            origin = neworigin;
            Debug.Log("Origin: " + origin);

            SaveAngle saveAngle = new SaveAngle { savedOrigin = origin };
            string json = JsonUtility.ToJson(saveAngle);
            System.IO.File.WriteAllText(Application.dataPath + "/save.json", json);
        }  

        angleFromOrigin = angle - origin;
        if(angleFromOrigin>180) angleFromOrigin -= 360;
        if(angleFromOrigin<-180) angleFromOrigin += 360;

        if(Input.GetAxisRaw("Vertical") == 1 && angleFromOrigin < maxbound) {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }

        if(Input.GetAxisRaw("Vertical") == -1 && angleFromOrigin > minbound) {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
   
        if(Input.GetButton("Jump")) {
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        }

        newangle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        
        if(angle != newangle) {
            angle = newangle;
            Debug.Log("Global Angle: " + angle);
        }
    }

    public void SetOrigin() {
        neworigin = angle;
    }

    private class SaveAngle {
        public float savedOrigin;
    }
}
