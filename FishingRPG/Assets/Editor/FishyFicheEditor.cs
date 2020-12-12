﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEditor.Experimental.GraphView;

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
    SerializedProperty agility_s;
    SerializedProperty magicResistance_s;

    SerializedProperty drops_s;

    SerializedProperty calmPatterns_s;
    SerializedProperty ragePatterns_s;


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
        agility_s         = serializedObject.FindProperty(nameof(FishyFiche.agility        ));
        magicResistance_s = serializedObject.FindProperty(nameof(FishyFiche.magicResistance));

        drops_s           = serializedObject.FindProperty(nameof(FishyFiche.drops          ));

        calmPatterns_s    = serializedObject.FindProperty(nameof(FishyFiche.calmPatterns   ));
        ragePatterns_s    = serializedObject.FindProperty(nameof(FishyFiche.ragePatterns   ));

        fishSprite = (target as FishyFiche).appearance;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        #region Draw Title
        EditorGUILayout.BeginHorizontal();
        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 30, font = (Font)Resources.Load("Fonts/GLECB", typeof(Font)) };
        EditorGUILayout.LabelField("#" + ID_s.stringValue + " - " + species_s.stringValue + " - Tier " + tier_s.intValue, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw Sprite & Texture
        var layout = new GUILayoutOption[] { };
        if (fishSprite != null) layout = new GUILayoutOption[] { GUILayout.Height(fishSprite.rect.height / 5) };

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(appearance_s, GUIContent.none, layout);

        if (EditorGUI.EndChangeCheck()) fishSprite = (target as FishyFiche).appearance;
        if(fishSprite != null) OnGUIDrawSprite(fishSprite.rect, fishSprite);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        //Style for all subtitles
        var subtitleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 25, font = (Font)Resources.Load("Fonts/GLECB", typeof(Font)) };

        //Initiation for colored texts
        GUIStyle whiteText = new GUIStyle(EditorStyles.label);
        whiteText.normal.textColor = Color.white;
        float baseLabel = EditorGUIUtility.labelWidth;

        //Label style for all stats
        EditorGUIUtility.labelWidth /= 2;

        #region Draw Life and Stamina
        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(200, 55, 55, 255);
        EditorGUILayout.LabelField("Life : " + life_s.floatValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(255, 139, 80, 255);
        EditorGUILayout.LabelField("Stamina : " + stamina_s.floatValue, whiteText);
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

        #region Stats
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stats", subtitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(224, 0, 59, 255);
        EditorGUILayout.LabelField("Strength : " + strength_s.floatValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(255, 147, 49, 255);
        EditorGUILayout.LabelField("Weight : " + weight_s.floatValue, whiteText);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(0, 180, 85, 255);
        EditorGUILayout.LabelField("Agility : " + agility_s.floatValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(36, 141, 206, 255);
        EditorGUILayout.LabelField("Magic Res. : " + magicResistance_s.floatValue, whiteText);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        #region Separator
        var rect2 = EditorGUILayout.BeginHorizontal();
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(rect2.x - 15, rect2.y), new Vector2(rect2.width + 15, rect2.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        #region Draw Drops list
        /*EditorGUIUtility.labelWidth = baseLabel;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Butcher's Drops" , subtitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //Temporary waiting for tsv import
        /*EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(drops_s);
        EditorGUILayout.EndHorizontal();
        Repaint();*/

        /*EditorGUIUtility.labelWidth /= 2.5f;
        Color32 rarityColor = new Color32();
        for (int i = 0; i < currentFish.drops.Length; i++)
        {
            switch (currentFish.drops[i].rarity)
            {
                case "Common":
                    rarityColor = new Color32(0, 0, 0, 255);
                    break;

                case "Rare":
                    rarityColor = new Color32(0, 112, 221, 255);
                    break;
                
                case "Epic":
                    rarityColor = new Color32(163, 53, 238, 255);
                    break;
                
                case "Legendary":
                    rarityColor = new Color32(255, 128, 0, 255);
                    break;
            }

            EditorGUILayout.BeginHorizontal();
            GUI.contentColor = rarityColor;
            EditorGUILayout.LabelField(currentFish.drops[i].type, whiteText);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(currentFish.drops[i].rarity, whiteText);
            GUI.contentColor = new Color32((byte)(255 - currentFish.drops[i].dropRate * 2.55f), (byte)(0 + currentFish.drops[i].dropRate * 2.55f), 0, 255);
            EditorGUILayout.LabelField(currentFish.drops[i].dropRate + "%", whiteText);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        EditorGUILayout.Space();*/
        #endregion

        #region Separator
        var rect3 = EditorGUILayout.BeginHorizontal();
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(rect3.x - 15, rect3.y), new Vector2(rect3.width + 15, rect3.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        #region Draw Patterns lists
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Patterns", subtitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUIUtility.labelWidth = baseLabel;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(calmPatterns_s);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(ragePatterns_s);
        EditorGUILayout.EndHorizontal();
        Repaint();
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    void OnGUIDrawSprite(Rect _pos, Sprite _sprite)
    {
        Rect dimensions = new Rect(_sprite.rect.position, _sprite.rect.size/2);
        float sWidth = dimensions.width /2;
        float sHeight = dimensions.height /2;
        Rect pos = GUILayoutUtility.GetRect(sWidth, sHeight);
        pos.width = sWidth;
        pos.height = sHeight;

        if(Event.current.type == EventType.Repaint)
        {
            var tex = _sprite.texture;
            dimensions.xMin /= tex.width /2;
            dimensions.xMax /= tex.width /2;
            dimensions.yMin /= tex.height /2;
            dimensions.yMax /= tex.height /2;

            GUI.DrawTextureWithTexCoords(pos, tex, dimensions);
        }
    }

    private void OnDisable()
    {
        
    }
}
