using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GemTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<Gem> gems = new List<Gem>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        gems.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data Gems");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            //Find all existing IDs
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/Gems"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    Gem currentGem = (Gem)AssetDatabase.LoadAssetAtPath(file, typeof(Gem));

                    //Verify if this fiche doesn't already exist
                    for (int gem = 0; gem < gems.Count; gem++)
                    {
                        if (!currentGem.name.Contains(gems[gem].name))
                        {
                            //Then add it to the list
                            gems.Add(currentGem);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < gems.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (gems[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Rod already exist, updating values...");
                    UpdateScriptable(gems[j]);
                }
                else
                {
                    Debug.Log("Rod doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if (gems.Count == 0)
            {
                Debug.Log("FishingRod list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/Gems/" + blocks[0] + ".asset";
        Gem asset = ScriptableObject.CreateInstance<Gem>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(Gem _gem)
    {
        _gem.ID = blocks[0];
        _gem.upgradeState = int.Parse(blocks[1]);
        _gem.gemName = blocks[2];
        _gem.stats = blocks[3];
        _gem.description = blocks[4];
        _gem.tier = int.Parse(blocks[5]);
    }
}
