using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingHead : MonoBehaviour
{
    public float walkingBobbingSpeed = 5f;
    public float bobbingAmount = 0.05f;
    public PlayerMovement controller;
    float normeVector;
    float defaultPosY = 0;
    float defaultPosX = 0;
    float timer = 0;
    public Transform target;


    [Header("For Sound Purposes")]
    //For Sound Purposes
    float oldTimer = 0f;
    public bool playSound = true;
    public LUD_TerrainTextureDetector terrainTextureDetector;


    

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        defaultPosX = transform.localPosition.x;

        
    }

    // Update is called once per frame
    void Update() 
    {
        
        if (Input.GetAxisRaw("Horizontal")>0.1f || Input.GetAxisRaw("Vertical")>0.1f || Input.GetAxisRaw("Horizontal") < -0.1f || Input.GetAxisRaw("Vertical")<-0.1f)
        {
            normeVector = Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw("Horizontal"),2) + Mathf.Pow(Input.GetAxisRaw("Vertical"),2));
            //Player is moving
            timer += Time.fixedDeltaTime * walkingBobbingSpeed * normeVector;


            //--Le bruit de pas va-t-il se jouer ?---//
            if (oldTimer != 0 && playSound)
            {
                if ((Mathf.Cos(timer) > 0 && Mathf.Cos(oldTimer) < 0) || ((Mathf.Cos(timer) < 0 && Mathf.Cos(oldTimer) > 0)))   //si cos(oldTimer) et cos(timer) sont de signes différents
                {
                    /*
                    Debug.Log("Pas");
                    //Play Sound
                    AkSoundEngine.PostEvent("OnCharacterWalk", gameObject);         //à enlever si ça te sert plus à rien ADRIEN
                    */

                    string textureName = terrainTextureDetector.GetTerrainTextureAtPlayerPosition();

                    if (textureName == "New Layer")
                    {
                        Debug.Log("Marche sur Gravier");
                    }
                    else if (textureName == "New Layer 2")
                    {
                        Debug.Log("Marche sur Rocher");
                    }
                    else if (textureName == "New Layer 3")
                    {
                        Debug.Log("Marche sur sable");
                    }
                    else
                    {
                        Debug.Log("Nom de layer non reconnu");  //ducoup son par défaut ou pas de son comme vous voulez
                    }





                }
            }
            oldTimer = timer;
            //---//

            transform.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer) * bobbingAmount * normeVector, defaultPosY + Mathf.Abs(Mathf.Sin(timer)) *1 * bobbingAmount * normeVector, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            oldTimer = 0;

            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, defaultPosX, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.y , transform.localPosition.z);
        }
        
    }
}
