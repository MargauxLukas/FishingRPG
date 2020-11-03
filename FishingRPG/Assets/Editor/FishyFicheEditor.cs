using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(FishyFiche))]
public class FishyFicheEditor : Editor
{
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnDisable()
    {
        
    }
}
