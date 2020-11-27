using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEditor.Experimental.GraphView;

[CustomEditor(typeof(FishyDrop))]
public class FishyDropEditor : Editor
{
    FishyDrop currentDrop;

    SerializedProperty ID_s;
    SerializedProperty appearance_s;
    SerializedProperty type_s;
    SerializedProperty rarity_s;
    SerializedProperty dropRate_s;

    Sprite dropSprite;

    private void OnEnable()
    {
        currentDrop = (target as FishyDrop);

        ID_s         = serializedObject.FindProperty(nameof(FishyDrop.ID        ));
        appearance_s = serializedObject.FindProperty(nameof(FishyDrop.appearance));
        type_s       = serializedObject.FindProperty(nameof(FishyDrop.type      ));
        rarity_s     = serializedObject.FindProperty(nameof(FishyDrop.rarity    ));
        dropRate_s   = serializedObject.FindProperty(nameof(FishyDrop.dropRate  ));

        dropSprite = (target as FishyDrop).appearance;
    }

    public override void OnInspectorGUI()
    {
        #region Draw Title
        EditorGUILayout.BeginHorizontal();
        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 30, font = (Font)Resources.Load("Fonts/GLECB", typeof(Font)) };
        EditorGUILayout.LabelField("#" + ID_s.stringValue + " - " + type_s.stringValue, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw Sprite & Texture
        var layout = new GUILayoutOption[] { };
        if (dropSprite != null) layout = new GUILayoutOption[] { GUILayout.Height(dropSprite.rect.height / 8) };

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(appearance_s, GUIContent.none, layout);

        if (EditorGUI.EndChangeCheck()) dropSprite = (target as FishyDrop).appearance;
        if (dropSprite != null) OnGUIDrawSprite(dropSprite);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        //Initiation for colored texts
        GUIStyle whiteText = new GUIStyle(EditorStyles.label);
        whiteText.normal.textColor = Color.white;

        //Label style for all stats
        float baseLabel = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth /= 2;

        #region Draw Rarity and Drop rate
        Color32 rarityColor = new Color32();

        EditorGUILayout.BeginHorizontal();
        switch (currentDrop.rarity)
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

        GUI.contentColor = rarityColor;
        EditorGUILayout.LabelField(rarity_s.stringValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32((byte)(255 - dropRate_s.floatValue * 2.55f), (byte) (0 + dropRate_s.floatValue * 2.55f), 0, 255);
        EditorGUILayout.LabelField("Drop rate : " + dropRate_s.floatValue + "%", whiteText);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    void OnGUIDrawSprite(Sprite _sprite)
    {
        Rect dimensions = new Rect(_sprite.rect.position, _sprite.rect.size / 3);
        float sWidth = dimensions.width / 3;
        float sHeight = dimensions.height / 3;
        Rect pos = GUILayoutUtility.GetRect(sWidth, sHeight);
        pos.width = sWidth;
        pos.height = sHeight;

        if (Event.current.type == EventType.Repaint)
        {
            var tex = _sprite.texture;
            dimensions.xMin /= tex.width / 3;
            dimensions.xMax /= tex.width / 3;
            dimensions.yMin /= tex.height / 3;
            dimensions.yMax /= tex.height / 3;

            GUI.DrawTextureWithTexCoords(pos, tex, dimensions);
        }
    }

    private void OnDisable()
    {
        
    }
}
