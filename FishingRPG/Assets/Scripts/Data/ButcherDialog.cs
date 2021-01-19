using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewButcherDialog", menuName = "BFF Tools/Butcher Dialog", order = 10)]
public class ButcherDialog : ScriptableObject
{
    public string ID;
    public string characterName;
    public string sentence;
}
