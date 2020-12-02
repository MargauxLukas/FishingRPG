using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishingRodTSVReader))]
public class FishingRodTSVReaderInspector : Editor
{
    FishingRodTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as FishingRodTSVReader);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update FishingRods"))
        {
            targetScript.ReadTab();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnDisable()
    {

    }
}
