﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishyFicheTSVReader))]
public class FishyFicheTSVReaderInspector : Editor
{
    FishyFicheTSVReader targetScript;
    
    private void OnEnable()
    {
        targetScript = (target as FishyFicheTSVReader);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update FishyFiches"))
        {
            targetScript.ReadTab();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnDisable()
    {
        
    }
}
