using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rotator))]
public class OriginButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Rotator myScript = (Rotator)target;
        // if(GUILayout.Button("Rotate Clockwise")) {
        //     myScript.Clockwise();
        // }
        
        if(GUILayout.Button("Set Origin")) {
            myScript.SetOrigin();
        }
    }
}
