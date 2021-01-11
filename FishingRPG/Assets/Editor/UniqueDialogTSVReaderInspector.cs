using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UniqueDialogTSVReader))]
public class UniqueDialogTSVReaderInspector : Editor
{
    UniqueDialogTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as UniqueDialogTSVReader);
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
