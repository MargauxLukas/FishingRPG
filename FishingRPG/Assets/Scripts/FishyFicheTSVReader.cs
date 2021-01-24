using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class FishyFicheTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<FishyFiche> fishyFiches = new List<FishyFiche>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        fishyFiches.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data Fish");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            //Find all existing IDs
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/FishyFiches"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    FishyFiche currentFishyFiche = (FishyFiche)AssetDatabase.LoadAssetAtPath(file, typeof(FishyFiche));

                    //Verify if this fiche doesn't already exist
                    for (int fiche = 0; fiche < fishyFiches.Count; fiche++)
                    {
                        if (!currentFishyFiche.name.Contains(fishyFiches[fiche].name))
                        {
                            //Then add it to the list
                            fishyFiches.Add(currentFishyFiche);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < fishyFiches.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (fishyFiches[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Fiche already exist, updating values...");
                    UpdateScriptable(fishyFiches[j]);
                }
                else
                {
                    Debug.Log("Fiche doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if(fishyFiches.Count == 0)
            {
                Debug.Log("FishyFiche list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/FishyFiches/" + blocks[0] + ".asset";
        FishyFiche asset = ScriptableObject.CreateInstance<FishyFiche>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(FishyFiche _fishyFiche)
    {
        _fishyFiche.ID      = blocks[0];
        _fishyFiche.species = blocks[1];
        _fishyFiche.tier            = int.Parse(blocks[2]);

        _fishyFiche.life            = int.Parse(blocks[3]);
        _fishyFiche.stamina         = int.Parse(blocks[4]);

        _fishyFiche.strength        = int.Parse(blocks[5]);
        _fishyFiche.weight          = int.Parse(blocks[6]);
        _fishyFiche.agility         = int.Parse(blocks[7]);
        _fishyFiche.magicResistance = int.Parse(blocks[8]);
    }
}
