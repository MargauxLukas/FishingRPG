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

    Sprite fishSprite;

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

        fishSprite = (target as FishyFiche).appearance;
    }

    public override void OnInspectorGUI()
    {
        //Temporary values waiting for tsv import
        currentFish.ID = "PQS_0";
        currentFish.species = "Pequessivo";
        currentFish.tier = 0;

        #region Draw Title
        EditorGUILayout.BeginHorizontal();
        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 26, font = (Font)Resources.Load("GLECB", typeof(Font)) };
        EditorGUILayout.LabelField("#" + ID_s.stringValue + " - " + species_s.stringValue + " - Tier " + tier_s.intValue, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw Sprite & Texture
        var layout = new GUILayoutOption[] { };
        if (fishSprite != null) layout = new GUILayoutOption[] { GUILayout.Height(fishSprite.rect.height / 16) };

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(appearance_s, GUIContent.none, layout);

        if (EditorGUI.EndChangeCheck()) fishSprite = (target as FishyFiche).appearance;
        if(fishSprite != null) OnGUIDrawSprite(fishSprite.rect, fishSprite);
        EditorGUILayout.EndHorizontal();
        #endregion


        GUIStyle statsStyle = new GUIStyle();
        statsStyle.richText = true;

        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(247, 33, 33, 255);
        GUILayout.Label("Life : " + life_s.floatValue + "</size>", statsStyle);
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Stamina : " + stamina_s.floatValue);
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    void OnGUIDrawSprite(Rect _pos, Sprite _sprite)
    {
        Rect dimensions = new Rect(_sprite.rect.position, _sprite.rect.size/4);
        float sWidth = dimensions.width /4;
        float sHeight = dimensions.height /4;
        Rect pos = GUILayoutUtility.GetRect(sWidth, sHeight);
        pos.width = sWidth;
        pos.height = sHeight;

        if(Event.current.type == EventType.Repaint)
        {
            var tex = _sprite.texture;
            dimensions.xMin /= tex.width /4;
            dimensions.xMax /= tex.width /4;
            dimensions.yMin /= tex.height /4;
            dimensions.yMax /= tex.height /4;

            GUI.DrawTextureWithTexCoords(pos, tex, dimensions);
        }
    }

    private void OnDisable()
    {
        
    }
}
