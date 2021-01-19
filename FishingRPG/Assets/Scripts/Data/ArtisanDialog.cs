using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewArtisanDialog", menuName = "BFF Tools/Artisan Dialog", order = 10)]
public class ArtisanDialog : ScriptableObject
{
    public string ID;
    public string characterName;
    public string sentence;
}
