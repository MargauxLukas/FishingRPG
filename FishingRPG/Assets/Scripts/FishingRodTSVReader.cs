using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class FishingRodTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<FishingRod> fishingRods = new List<FishingRod>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        fishingRods.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data FishingRod");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            //Find all existing IDs
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/FishingRods"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    FishingRod currentFishingRod = (FishingRod)AssetDatabase.LoadAssetAtPath(file, typeof(FishingRod));

                    //Verify if this fiche doesn't already exist
                    for (int rod = 0; rod < fishingRods.Count; rod++)
                    {
                        if (!currentFishingRod.name.Contains(fishingRods[rod].name))
                        {
                            //Then add it to the list
                            fishingRods.Add(currentFishingRod);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < fishingRods.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (fishingRods[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Rod already exist, updating values...");
                    UpdateScriptable(fishingRods[j]);
                }
                else
                {
                    Debug.Log("Rod doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if (fishingRods.Count == 0)
            {
                Debug.Log("FishingRod list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/FishingRods/" + blocks[0] + ".asset";
        FishingRod asset = ScriptableObject.CreateInstance<FishingRod>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(FishingRod _fishingRod)
    {
        _fishingRod.ID       = blocks[0];
        _fishingRod.upgradeState = int.Parse(blocks[1]);
        _fishingRod.itemName = blocks[2];
        _fishingRod.description = blocks[3];
        _fishingRod.tier         = int.Parse(blocks[4]);

        _fishingRod.rodTension   = int.Parse(blocks[5]);
        _fishingRod.gemSlots     = int.Parse(blocks[6]);

        _fishingRod.strength     = int.Parse(blocks[7]);
        _fishingRod.constitution = int.Parse(blocks[8]);
        _fishingRod.dexterity    = int.Parse(blocks[9]);
        _fishingRod.intelligence = int.Parse(blocks[10]);
    }
}
