using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishyDropTSVReader))]
public class FishyDropTSVReaderInspector : Editor
{
    FishyDropTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as FishyDropTSVReader);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update FishyDrops"))
        {
            targetScript.ReadTab();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnDisable()
    {
        
    }
}
