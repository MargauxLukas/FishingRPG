using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class FishyFicheTSVReader : MonoBehaviour
{
    public static FishyFicheTSVReader instance;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    void Start()
    {
        //Load Text asset
        TextAsset TSVText = Resources.Load<TextAsset>("TextAssets/[TOOL] TSV Data Fish");

        //Add each line of the tab in a list 'lines'
        string[] lines = TSVText.text.Split(new char[] { '\n' });

        //For each line...
        for (int i = 1; i < lines.Length - 1; i++)
        {
            //... add each column of the line in a list 'blocks'
            string[] blocks = lines[i].Split(new char[] { ';' });

            //Verify if ID already exist
            foreach (string file in Directory.GetFiles("Assets/Scripts/Data/Scriptables/FishyFiches"))
            {
                if (file.Contains(".meta"))
                {
                    return;
                }
                else
                {
                    List<FishyFiche> fishyFiches = new List<FishyFiche>();
                    FishyFiche currentFishyFiche = (FishyFiche)AssetDatabase.LoadAssetAtPath(file, typeof(FishyFiche));

                    fishyFiches.Add(currentFishyFiche);
                    Debug.Log("Debug : " + fishyFiches[0]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() 
    {
        
    }
}
