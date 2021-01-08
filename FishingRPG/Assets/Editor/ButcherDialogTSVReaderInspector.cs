using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButcherDialogTSVReader))]
public class ButcherDialogTSVReaderInspector : Editor
{
    ButcherDialogTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as ButcherDialogTSVReader);
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
