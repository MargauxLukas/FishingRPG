using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GemTSVReader))]
public class GemTSVReaderInspector : Editor
{
    GemTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as GemTSVReader);
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
