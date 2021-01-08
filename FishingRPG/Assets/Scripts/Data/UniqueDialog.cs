using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewUniqueDialog", menuName = "BFF Tools/Unique Dialog", order = 10)]
public class UniqueDialog : ScriptableObject
{
    public string ID;
    public string characterName;
    public string characterExpression;
    public string sentence;
}
