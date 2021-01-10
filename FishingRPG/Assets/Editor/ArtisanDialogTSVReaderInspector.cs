using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArtisanDialogTSVReader))]
public class ArtisanDialogTSVReaderInspector : Editor
{
    ArtisanDialogTSVReader targetScript;

    private void OnEnable()
    {
        targetScript = (target as ArtisanDialogTSVReader);
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
