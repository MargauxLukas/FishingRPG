using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class FishyDropTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<FishyDrop> fishyDrops = new List<FishyDrop>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        fishyDrops.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data Drops");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/FishyDrops"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    FishyDrop currentFishyDrop = (FishyDrop)AssetDatabase.LoadAssetAtPath(file, typeof(FishyDrop));

                    //Verify if this fiche doesn't already exist
                    for (int drop = 0; drop < fishyDrops.Count; drop++)
                    {
                        if (!currentFishyDrop.name.Contains(fishyDrops[drop].name))
                        {
                            //Then add it to the list
                            fishyDrops.Add(currentFishyDrop);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < fishyDrops.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (fishyDrops[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Rod already exist, updating values...");
                    UpdateScriptable(fishyDrops[j]);
                }
                else
                {
                    Debug.Log("Rod doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if (fishyDrops.Count == 0)
            {
                Debug.Log("FishingRod list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/FishyDrops/" + blocks[0] + ".asset";
        FishyDrop asset = ScriptableObject.CreateInstance<FishyDrop>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(FishyDrop _fishyDrop)
    {
        _fishyDrop.ID     = blocks[0];
        _fishyDrop.type   = blocks[1];
        _fishyDrop.rarity = blocks[2];
        _fishyDrop.dropRate = int.Parse(blocks[3]);
        _fishyDrop.description = blocks[4];
    }
}
