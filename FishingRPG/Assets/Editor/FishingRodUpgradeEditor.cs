using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishingRodUpgrade))]
public class FishingRodUpgradeEditor : Editor
{
    FishingRodUpgrade currentUpgrade;

    SerializedProperty ID_s;
    SerializedProperty upgradeState_s;
    SerializedProperty itemName_s;
    SerializedProperty tier_s;
    SerializedProperty gemPosition_s;

    SerializedProperty strength_s;
    SerializedProperty constitution_s;
    SerializedProperty dexterity_s;
    SerializedProperty intelligence_s;

    SerializedProperty components_s;
    SerializedProperty componentsQty_s;

    private void OnEnable()
    {
        currentUpgrade = (target as FishingRodUpgrade);

        ID_s            = serializedObject.FindProperty(nameof(FishingRodUpgrade.ID           ));
        upgradeState_s  = serializedObject.FindProperty(nameof(FishingRodUpgrade.upgradeState ));
        itemName_s      = serializedObject.FindProperty(nameof(FishingRodUpgrade.itemName     ));
        tier_s          = serializedObject.FindProperty(nameof(FishingRodUpgrade.tier         ));
        gemPosition_s   = serializedObject.FindProperty(nameof(FishingRodUpgrade.gemPosition  ));

        strength_s      = serializedObject.FindProperty(nameof(FishingRodUpgrade.strength     ));
        constitution_s  = serializedObject.FindProperty(nameof(FishingRodUpgrade.constitution ));
        dexterity_s     = serializedObject.FindProperty(nameof(FishingRodUpgrade.dexterity    ));
        intelligence_s  = serializedObject.FindProperty(nameof(FishingRodUpgrade.intelligence ));

        components_s    = serializedObject.FindProperty(nameof(FishingRodUpgrade.components   ));
        componentsQty_s = serializedObject.FindProperty(nameof(FishingRodUpgrade.componentsQty));
    }

    public override void OnInspectorGUI()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
