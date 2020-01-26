using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CopyProperties))]
public class CopyPropertiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CopyProperties myTarget = (CopyProperties) target;

        // Show default inspector property editor
        DrawDefaultInspector ();

         if(GUILayout.Button("Copy")) {
             Debug.Log(myTarget.GetProperties());
         }
    }
}
