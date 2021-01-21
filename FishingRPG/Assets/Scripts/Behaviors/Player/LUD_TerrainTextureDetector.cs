using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_TerrainTextureDetector : MonoBehaviour
{
    public Transform playerTransform;

    [Header("Auto Assign")]

    public Terrain mTerrain;
    public TerrainData mTerrainData;
    public int alphamapWidth;
    public int alphamapHeight;

    public float[,,] mSplatmapData;
    public int mNumTextures;

    public Vector3 vecRet;
    public Vector3 TerrainCord;
    public string result_TerrainTexture;
    public string result_TerrainName;


    private void Update()
    {
        //GetTerrainTextureAtPosition(playerTransform.position);   //à transférer
    }

    private void GetTerrainProps()
    {

        mTerrainData = mTerrain.terrainData;
        alphamapWidth = mTerrainData.alphamapWidth;
        alphamapHeight = mTerrainData.alphamapHeight;

        mSplatmapData = mTerrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
        mNumTextures = mSplatmapData.Length / (alphamapWidth * alphamapHeight);
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {

        Vector3 terrainPosition = playerPos - mTerrain.transform.position;


        Vector3 mapPosition = new Vector3(terrainPosition.x / mTerrain.terrainData.size.x, 0f, terrainPosition.z / mTerrain.terrainData.size.z);  //pos du joueur dans un référentiel d'unité "terrain intégral"

        float XCoord = mapPosition.x * mTerrain.terrainData.alphamapWidth;   //on détermine sur quel slot de la map on se trouve en X et en Y
        float ZCoord = mapPosition.z * mTerrain.terrainData.alphamapHeight;

        vecRet.x = (int)XCoord;
        vecRet.z = (int)ZCoord;

        return vecRet;
    }

    private int GetActiveTerrainTextureIdx(Vector3 pos)
    {
        TerrainCord = ConvertToSplatMapCoordinate(pos);
        int ret = 0;
        float comp = 0f;

        if ((TerrainCord.x >= 0 && TerrainCord.x < 512) && (TerrainCord.z >= 0 && TerrainCord.z < 512))
        {
            for (int i = 0; i < mNumTextures; i++)
            {
                if (comp < mSplatmapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                    ret = i;

            }

            result_TerrainTexture = mTerrainData.terrainLayers[ret].name;
        }
        else result_TerrainTexture = "None";


        return ret;
    }

    public string GetTerrainTextureAtPlayerPosition()
    {
        Vector3 pos = playerTransform.position;


        GetTerrainAtPosition(pos); //on définit quel terrain est celui sous le joueur
        GetTerrainProps();  //on met à jour les data du terrain actuel
        GetActiveTerrainTextureIdx(pos);    //on détermine la texture actuelle

        return result_TerrainTexture;
    }

    private void GetTerrainAtPosition(Vector3 pos)
    {

        Terrain[] terrains = Terrain.activeTerrains; //Charge tous les terrains

        if (terrains.Length == 0)   // s'il n'y a aucun terrain
            Debug.LogError("Aucun Terrain detecté");

        if (terrains.Length == 1)   // s'il n'y a qu'1 terrain
            mTerrain = terrains[0];


        Vector3 terrainPos = terrains[0].GetPosition();
        terrainPos = new Vector3(terrainPos.x + 25, 0, terrainPos.z + 25);    // position du terrain 0 (en excluant la hauteur) //+25 car le pivot du Terrain n'est pas en son centre

        Vector3 playerPos = playerTransform.position;
        playerPos = new Vector3(playerPos.x, 0, playerPos.z);       // position du joueur (en excluant la hauteur)

        float lowDist = (terrainPos - playerPos).magnitude;
        var terrainIndex = 0;



        for (int i = 1; i < terrains.Length; i++)
        {

            terrainPos = terrains[i].GetPosition();
            terrainPos = new Vector3(terrainPos.x + 25, 0, terrainPos.z + 25);    // position du terrain i (en excluant la hauteur)

            playerPos = playerTransform.position;
            playerPos = new Vector3(playerPos.x, 0, playerPos.z);       // position du joueur (en excluant la hauteur)

            var dist = (terrainPos - playerPos).magnitude;

            if (dist < lowDist)
            {
                lowDist = dist;
                terrainIndex = i;
            }
        }

        mTerrain = terrains[terrainIndex];

        result_TerrainName = mTerrain.gameObject.name;
    }

}
