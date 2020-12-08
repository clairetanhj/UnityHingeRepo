using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rotator : MonoBehaviour
{
    public float maxAngle;
    public float minAngle;
    public float upperBound;
    public float lowerBound;

    [SerializeField] private float angleFromOrigin;
    private float angle;
    private float origin;
    private float neworigin;
    private float position;
    private float newposition;

    void Start()
    {        
        // load origin from save.json
        if (System.IO.File.Exists(Application.dataPath + "/save.json")) {
            string saveString = System.IO.File.ReadAllText(Application.dataPath + "/save.json");
            SaveAngle loadedData = JsonUtility.FromJson<SaveAngle>(saveString);
            origin = loadedData.savedOrigin;
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        } else {
            origin = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        }
        
        neworigin = angle = origin;
        Debug.Log("Origin: " + origin);

        newposition = position = (angleFromOrigin-minAngle) / (maxAngle-minAngle) * (upperBound-lowerBound) + lowerBound;
        Debug.Log("Position in Scale: " + position);
    }

    void Update()
    {   
        // set value of new origin and save it to save.json
        if(origin != neworigin) {
            origin = neworigin;
            Debug.Log("Origin: " + origin);

            SaveAngle saveAngle = new SaveAngle { savedOrigin = origin };
            string json = JsonUtility.ToJson(saveAngle);
            System.IO.File.WriteAllText(Application.dataPath + "/save.json", json);
        }  

        // calculate angleFromOrigin from global angle
        angleFromOrigin = angle - origin;
        if(angleFromOrigin>180) angleFromOrigin -= 360;
        if(angleFromOrigin<-180) angleFromOrigin += 360;

        // calculate and print position in scale
        newposition = (angleFromOrigin-minAngle) / (maxAngle-minAngle) * (upperBound-lowerBound) + lowerBound;
        if(position != newposition) {
            position = newposition;
            Debug.Log("Position in Scale: " + position);
        }

        // rotate arm according to input
        if(Input.GetAxisRaw("Vertical") == 1 && angleFromOrigin < maxAngle) {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }

        if(Input.GetAxisRaw("Vertical") == -1 && angleFromOrigin > minAngle) {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
   
        if(Input.GetButton("Jump")) {
            transform.localRotation = Quaternion.Euler(origin, 0.0f, 0.0f);
        }

        //set value of new global angle
        angle = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
    }

    public void SetOrigin() {
        neworigin = angle;
    }

    private class SaveAngle {
        public float savedOrigin;
    }
}
