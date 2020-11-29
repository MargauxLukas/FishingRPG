using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Feature { Int, Float, Curve }

[CustomEditor(typeof(FishyPattern))]
public class FishyPatternEditor : Editor
{
    FishyPattern currentPattern;

    SerializedProperty priority_s;
    SerializedProperty duration_s;
    SerializedProperty energyCost_s;
    SerializedProperty costEnergyOverTime_s;
    SerializedProperty DOTFrequency_s;
    SerializedProperty triggerRageMode_s;

    SerializedProperty featureName_s;
    Feature featureType_s;

    private void OnEnable()
    {
        currentPattern = (target as FishyPattern);

        priority_s           = serializedObject.FindProperty(nameof(FishyPattern.priority          ));
        duration_s           = serializedObject.FindProperty(nameof(FishyPattern.duration          ));
        energyCost_s         = serializedObject.FindProperty(nameof(FishyPattern.energyCost        ));
        costEnergyOverTime_s = serializedObject.FindProperty(nameof(FishyPattern.costEnergyOverTime));
        DOTFrequency_s       = serializedObject.FindProperty(nameof(FishyPattern.DOTFrequency      ));
        triggerRageMode_s    = serializedObject.FindProperty(nameof(FishyPattern.triggerRageMode   ));

        featureName_s        = serializedObject.FindProperty(nameof(FishyPattern.featureName       ));
    }

    public override void OnInspectorGUI()
    {
        #region Draw Title
        EditorGUILayout.BeginHorizontal();
        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 30, font = (Font)Resources.Load("Fonts/GLECB", typeof(Font)) };
        string patternName = ObjectNames.NicifyVariableName(currentPattern.name);
        EditorGUILayout.LabelField(patternName, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        #region Draw Priority
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(priority_s);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw Duration
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(duration_s);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw EnergyCost
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(energyCost_s);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw EnergyOverTime bool
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(costEnergyOverTime_s);
        EditorGUILayout.EndHorizontal();
        #endregion

        if (currentPattern.costEnergyOverTime)
        {
            #region Draw DOTFrequency
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(DOTFrequency_s);
            EditorGUILayout.EndHorizontal();
            #endregion
        }

        #region RageMode bool
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(triggerRageMode_s);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        #region Separator
        var rect1 = EditorGUILayout.BeginHorizontal();
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(rect1.x - 15, rect1.y), new Vector2(rect1.width + 15, rect1.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new pattern feature (Coming soon)"))
        {
            //
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnDisable()
    {
        
    }
}
