using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArmorSetTSVReader))]
public class ArmorSetTSVReaderInspector : Editor
{
    ArmorSetTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as ArmorSetTSVReader);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update ArmorSets"))
        {
            targetScript.ReadTab();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnDisable()
    {

    }
}

