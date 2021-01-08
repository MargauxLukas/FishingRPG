﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class UniqueDialogTSVReader : MonoBehaviour
{
    string[] lines;  //List of all tab's lines
    string[] blocks; //List of each line's columns
    List<UniqueDialog> uDialogues = new List<UniqueDialog>();  //List of all existing Fishy Fiches

    public void ReadTab()
    {
        Debug.Log("Read Tab");
        uDialogues.Clear();

        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data UniqueDialogs");

        //Add each line of the tab in a list 'lines'
        lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length; i++)
        {
            Debug.Log("Read Lines");
            //... add each column of the line in a list 'blocks'
            blocks = lines[i].Split(new char[] { ';' });

            //Find all existing IDs
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/Dialogs/Unique"))
            {
                Debug.Log("Search files");
                //Adding all existing files in a list (excepting .meta)
                if (!file.Contains(".meta"))
                {
                    //Load asset at asset path as a FishyFiche
                    UniqueDialog currentDialog = (UniqueDialog)AssetDatabase.LoadAssetAtPath(file, typeof(UniqueDialog));

                    //Verify if this fiche doesn't already exist
                    for (int dial = 0; dial < uDialogues.Count; dial++)
                    {
                        if (!currentDialog.name.Contains(uDialogues[dial].name))
                        {
                            //Then add it to the list
                            uDialogues.Add(currentDialog);
                        }
                    }
                }
            }

            //Verify if line's ID already exist in project
            for (int j = 0; j < uDialogues.Count; j++) //Read all existing ID's in 'fishyFiches' list
            {
                Debug.Log("Read all fiches");
                //If current Fishy Fiche name contains tab's ID, update Fishy Fiche
                if (uDialogues[j].name.Contains(blocks[0]))
                {
                    Debug.Log("Rod already exist, updating values...");
                    UpdateScriptable(uDialogues[j]);
                }
                else
                {
                    Debug.Log("Rod doesn't exist, creating new asset...");
                    CreateNewScriptable();
                }
            }

            //If existing files list is empty, create directly a new scriptable
            if (uDialogues.Count == 0)
            {
                Debug.Log("FishingRod list empty, creating new asset...");
                CreateNewScriptable();
            }
        }
    }

    public void CreateNewScriptable()
    {
        //Create a local file path & a new instance of FishyFiche
        string localPath = "Assets/Scripts/Data/Scriptables/Dialogs/Unique/" + blocks[0] + ".asset";
        UniqueDialog asset = ScriptableObject.CreateInstance<UniqueDialog>();

        //Create the new asset at the created local path
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        AssetDatabase.CreateAsset(asset, localPath);
        AssetDatabase.SaveAssets();

        //Then updates data values
        UpdateScriptable(asset);

    }

    public void UpdateScriptable(UniqueDialog _dialogue)
    {
        _dialogue.ID = blocks[0];
        _dialogue.characterName = blocks[1];
        _dialogue.characterExpression = blocks[2];
        _dialogue.sentence = blocks[3];
    }
}
