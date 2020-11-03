using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(FishyFiche))]
public class FishyFicheEditor : Editor
{
    FishyFiche currentFish;

    SerializedProperty ID_s;
    SerializedProperty appearance_s;
    SerializedProperty species_s;
    SerializedProperty tier_s;

    SerializedProperty life_s;
    SerializedProperty stamina_s;

    SerializedProperty strength_s;
    SerializedProperty weight_s;
    SerializedProperty speed_s;
    SerializedProperty magicResistance_s;

    SerializedProperty drops_s;

    SerializedProperty patterns_s;

    private void OnEnable()
    {
        currentFish = (target as FishyFiche);

        ID_s              = serializedObject.FindProperty(nameof(FishyFiche.ID             ));
        appearance_s      = serializedObject.FindProperty(nameof(FishyFiche.appearance     ));
        species_s         = serializedObject.FindProperty(nameof(FishyFiche.species        ));
        tier_s            = serializedObject.FindProperty(nameof(FishyFiche.tier           ));

        life_s            = serializedObject.FindProperty(nameof(FishyFiche.life           ));
        stamina_s         = serializedObject.FindProperty(nameof(FishyFiche.stamina        ));

        strength_s        = serializedObject.FindProperty(nameof(FishyFiche.strength       ));
        weight_s          = serializedObject.FindProperty(nameof(FishyFiche.weight         ));
        speed_s           = serializedObject.FindProperty(nameof(FishyFiche.speed          ));
        magicResistance_s = serializedObject.FindProperty(nameof(FishyFiche.magicResistance));

        drops_s           = serializedObject.FindProperty(nameof(FishyFiche.drops          ));

        patterns_s        = serializedObject.FindProperty(nameof(FishyFiche.patterns       ));
    }

    public override void OnInspectorGUI()
    {
        currentFish.ID = "PQS_0";
        currentFish.species = "Pequessivo";

        EditorGUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 26, font = (Font)Resources.Load("GLECB", typeof(Font))};
        EditorGUILayout.LabelField("#" + ID_s.stringValue + " - " + species_s.stringValue, style, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(appearance_s, GUIContent.none);
        if(appearance_s != null)
        {
            Debug.Log("Image");
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnDisable()
    {
        
    }
}
