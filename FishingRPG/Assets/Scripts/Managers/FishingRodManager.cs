using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodManager : MonoBehaviour
{
    public static FishingRodManager instance;

    public GameObject fishingRodPivot;
    public GameObject bobber;
    public GameObject bobberPosition;
    public GameObject fishingRodGameObject;

    public Material catchMaterial;
    public Material dontCatchMaterial;

    //Bobber
    private Vector3 bobberScale = new Vector3(5f, 0.25f, 5f);
    private Quaternion bobberRotation;

    public bool bobberThrowed = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Start()
    {
        bobberRotation = bobber.transform.localRotation;
    }

    private void Update()
    {
        if(fishingRodPivot.GetComponent<Rotate>().result && !bobberThrowed)
        {
            bobberThrowed = true;
            LaunchBobber();
        }
    }

    public void LaunchBobber()
    {
        bobber.GetComponent<Rigidbody>().useGravity = true;
        bobber.GetComponent<Bobber>().Throw();
        CameraManager.instance.CameraLookAtGameObject(bobber);
        PlayerManager.instance.DisablePlayerMovement();
        PlayerManager.instance.EnableFishMovement();
    }

    public void BobberBack()
    {
        //A METTRE DANS UN BEHAVIOUR BobberBACK 
        bobberThrowed = false;
        bobber.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bobber.transform.position = bobberPosition.transform.position;
        bobber.transform.parent   = fishingRodGameObject.transform;
        fishingRodPivot.GetComponent<Rotate>().result = false;
        bobber.transform.localScale    = bobberScale;
        bobber.transform.localRotation = bobberRotation;
        bobber.GetComponent<Rigidbody>().useGravity = false;
        CameraManager.instance.FreeCameraEnable();
        PlayerManager.instance.EnablePlayerMovement();
        PlayerManager.instance.DisableFishMovement();

        //Fish Poisson
    }

    public void SetBobberMaterialToSucces()
    {
        bobber.GetComponent<MeshRenderer>().material = catchMaterial;
    }

    public void SetBobberMaterialToFail()
    {
        bobber.GetComponent<MeshRenderer>().material = dontCatchMaterial;
    }
}
