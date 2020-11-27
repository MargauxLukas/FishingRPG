using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArmorSet))]
public class ArmorSetEditor : Editor
{
    ArmorSet currentArmor;

    SerializedProperty ID_s;
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
    private void OnEnable()
    {
        currentArmor = (target as ArmorSet);

        ID_s            = serializedObject.FindProperty(nameof(ArmorSet.ID           ));
        itemType_s      = serializedObject.FindProperty(nameof(ArmorSet.ItemType     ));
        upgradeState_s  = serializedObject.FindProperty(nameof(ArmorSet.upgradeState ));
        itemName_s      = serializedObject.FindProperty(nameof(ArmorSet.itemName     ));
        tier_s          = serializedObject.FindProperty(nameof(ArmorSet.tier         ));

        strength_s      = serializedObject.FindProperty(nameof(ArmorSet.strength     ));
        constitution_s  = serializedObject.FindProperty(nameof(ArmorSet.constitution ));
        dexterity_s     = serializedObject.FindProperty(nameof(ArmorSet.dexterity    ));
        intelligence_s  = serializedObject.FindProperty(nameof(ArmorSet.intelligence ));

        components_s    = serializedObject.FindProperty(nameof(ArmorSet.components   ));
        componentsQty_s = serializedObject.FindProperty(nameof(ArmorSet.componentsQty));
    }

    public override void OnInspectorGUI()
    {
        
    }

    private void OnDisable()
    {
        
    }
}

