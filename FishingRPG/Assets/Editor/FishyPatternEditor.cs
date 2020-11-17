using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    SerializedProperty featureType_s;

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
        featureType_s        = serializedObject.FindProperty(nameof(FishyPattern.FeatureType       ));
    }

    public override void OnInspectorGUI()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
