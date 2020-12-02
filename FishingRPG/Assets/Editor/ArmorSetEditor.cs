using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArmorSet))]
public class ArmorSetEditor : Editor
{
    ArmorSet currentArmor;

    SerializedProperty ID_s;
    SerializedProperty appearance_s;
    SerializedProperty itemType_s;
    SerializedProperty upgradeState_s;
    SerializedProperty itemName_s;
    SerializedProperty tier_s;

    SerializedProperty strength_s;
    SerializedProperty constitution_s;
    SerializedProperty dexterity_s;
    SerializedProperty intelligence_s;

    SerializedProperty components_s;
    SerializedProperty componentsQty_s;

    Sprite armorSprite;

    private void OnEnable()
    {
        currentArmor = (target as ArmorSet);

        ID_s            = serializedObject.FindProperty(nameof(ArmorSet.ID           ));
        appearance_s    = serializedObject.FindProperty(nameof(ArmorSet.appearance   ));
        itemType_s      = serializedObject.FindProperty(nameof(ArmorSet.itemType     ));
        upgradeState_s  = serializedObject.FindProperty(nameof(ArmorSet.upgradeState ));
        itemName_s      = serializedObject.FindProperty(nameof(ArmorSet.itemName     ));
        tier_s          = serializedObject.FindProperty(nameof(ArmorSet.tier         ));

        strength_s      = serializedObject.FindProperty(nameof(ArmorSet.strength     ));
        constitution_s  = serializedObject.FindProperty(nameof(ArmorSet.constitution ));
        dexterity_s     = serializedObject.FindProperty(nameof(ArmorSet.dexterity    ));
        intelligence_s  = serializedObject.FindProperty(nameof(ArmorSet.intelligence ));

        components_s    = serializedObject.FindProperty(nameof(ArmorSet.components   ));
        componentsQty_s = serializedObject.FindProperty(nameof(ArmorSet.componentsQty));

        armorSprite = (target as ArmorSet).appearance;
    }

    public override void OnInspectorGUI()
    {
        #region Draw Title
        EditorGUILayout.BeginHorizontal();
        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 30, font = (Font)Resources.Load("Fonts/GLECB", typeof(Font)) };
        EditorGUILayout.LabelField("#" + ID_s.stringValue + " - " + itemName_s.stringValue + " - Level " + upgradeState_s.intValue, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw Sprite & Texture
        var layout = new GUILayoutOption[] { };
        if (armorSprite != null) layout = new GUILayoutOption[] { GUILayout.Height(armorSprite.rect.height / 4) };

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(appearance_s, GUIContent.none, layout);

        if (EditorGUI.EndChangeCheck()) armorSprite = (target as ArmorSet).appearance;
        if (armorSprite != null) OnGUIDrawSprite(armorSprite.rect, armorSprite);
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

        #region Draw Type and Tier
        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(0, 0, 0, 255);
        EditorGUILayout.LabelField("Type : " + itemType_s.stringValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(0, 0, 0, 255);
        EditorGUILayout.LabelField("Tier : " + tier_s.intValue, whiteText);
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
        EditorGUILayout.LabelField("Strength : " + strength_s.intValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(255, 147, 49, 255);
        EditorGUILayout.LabelField("Constitution : " + constitution_s.intValue, whiteText);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUI.contentColor = new Color32(0, 180, 85, 255);
        EditorGUILayout.LabelField("Dexterity : " + dexterity_s.intValue, whiteText);
        GUILayout.FlexibleSpace();
        GUI.contentColor = new Color32(36, 141, 206, 255);
        EditorGUILayout.LabelField("Intelligence : " + intelligence_s.intValue, whiteText);
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

        #region Draw Components list
        /*EditorGUIUtility.labelWidth = baseLabel;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Crafting Components" , subtitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //Temporary waiting for tsv import
        /*EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(components_s);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(componentsQty_s);
        EditorGUILayout.EndHorizontal();
        Repaint();*/

        /*EditorGUIUtility.labelWidth /= 2.5f;
        Color32 rarityColor = new Color32();
        for (int i = 0; i < currentArmor.components.Length; i++)
        {
            switch (currentArmor.components[i].rarity)
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
            EditorGUILayout.LabelField(currentArmor.components[i].type + " (" + currentArmor.componentsQty[i].ToString() + ")", whiteText);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        EditorGUILayout.Space();*/
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    void OnGUIDrawSprite(Rect _pos, Sprite _sprite)
    {
        Rect dimensions = new Rect(_sprite.rect.position, _sprite.rect.size / 2);
        float sWidth = dimensions.width / 2;
        float sHeight = dimensions.height / 2;
        Rect pos = GUILayoutUtility.GetRect(sWidth, sHeight);
        pos.width = sWidth;
        pos.height = sHeight;

        if (Event.current.type == EventType.Repaint)
        {
            var tex = _sprite.texture;
            dimensions.xMin /= tex.width / 2;
            dimensions.xMax /= tex.width / 2;
            dimensions.yMin /= tex.height / 2;
            dimensions.yMax /= tex.height / 2;

            GUI.DrawTextureWithTexCoords(pos, tex, dimensions);
        }
    }

    private void OnDisable()
    {
        
    }
}

