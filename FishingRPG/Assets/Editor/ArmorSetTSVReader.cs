using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ArmorSetTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<ArmorSet> armorSets = new List<ArmorSet>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        armorSets.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data Armor");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            //Find all existing IDs
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/ArmorSets"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    ArmorSet currentArmorSet = (ArmorSet)AssetDatabase.LoadAssetAtPath(file, typeof(ArmorSet));

                    //Verify if this fiche doesn't already exist
                    for (int armor = 0; armor < armorSets.Count; armor++)
                    {
                        if (!currentArmorSet.name.Contains(armorSets[armor].name))
                        {
                            //Then add it to the list
                            armorSets.Add(currentArmorSet);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < armorSets.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (armorSets[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Armor already exist, updating values...");
                    UpdateScriptable(armorSets[j]);
                }
                else
                {
                    Debug.Log("Armor doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if (armorSets.Count == 0)
            {
                Debug.Log("ArmorSet list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/ArmorSets/" + blocks[0] + ".asset";
        ArmorSet asset = ScriptableObject.CreateInstance<ArmorSet>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(ArmorSet _armorSet)
    {
        _armorSet.ID       = blocks[0];
        _armorSet.itemType = blocks[1];
        _armorSet.upgradeState = int.Parse(blocks[2]);
        _armorSet.itemName = blocks[3];
        _armorSet.tier         = int.Parse(blocks[4]);
        _armorSet.description = blocks[5];

        _armorSet.strength     = int.Parse(blocks[6]);
        _armorSet.constitution = int.Parse(blocks[7]);
        _armorSet.dexterity    = int.Parse(blocks[8]);
        _armorSet.intelligence = int.Parse(blocks[9]);
    }
}
