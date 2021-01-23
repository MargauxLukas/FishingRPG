using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    Quaternion FishingRodRotaBase;
    [HideInInspector] public bool isMax = false;

    //Rotation Aerial and Fell
    public Transform fishingRodAnchor;           //Ancre canne à pêche
    /*
    bool isFell = false;
    bool isFell2 = false;
    bool isAerial = false;
    bool isAerial2 = false;

    bool isFellFail = false;
    bool isFellFail2 = false;*/
    //bool isAerialFail = false;
    //bool isAerialFail2 = false;


    public bool result = false;

    private float holdingButtonTimer;
    public float holdingButtonMaxTimer;
    public Image holdButtonImage;

    private bool isCancelFishing = false;

    private bool isReleaseButton = true;

    private Vector3 imageTarget;

    private bool axisRelease = false;

    //New Bobber launch
    public GameObject bobberAura;
    public Camera mainCamera;
    RaycastHit hit;
    Vector3 raycastOrigin;
    public LayerMask layer;

    public Color yellowArrow;
    public Color redArrow;

    bool goodZone = true;  //La flêche est dans une zone correcte ?

    [Header("Test Anumat Rotate")]
    public float maxRotProjection = 0f;
    public float timeProjecAscending = 0f;
    public float timeProjecDescending = 0f;
    public float maxRotAbat = 0f;
    public float speedAbbatAscending = 0f;
    public float speedAbbatDescending = 0f;

    private void Start()
    {
        FishingRodRotaBase = transform.localRotation;
        imageTarget = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
    }

    void Update()
    {
        if (Input.GetButtonUp("B Button"))
        {
            isReleaseButton = true;
            holdButtonImage.gameObject.SetActive(false);
            holdingButtonTimer = 0;
            holdButtonImage.fillAmount = 0f;

            if(isCancelFishing)
            {
                isCancelFishing = false;
            }
        }
     
        /*
        if (Input.GetButtonUp("B Button") && isCancelFishing)
        {
            holdButtonImage.gameObject.SetActive(false);
            holdingButtonTimer = 0;
            holdButtonImage.fillAmount = 0f;
            isCancelFishing = false;
            isReleaseButton = true;
        }*/

        if (Input.GetButton("B Button") && FishingRodManager.instance.bobberThrowed)
        {
            holdButtonImage.gameObject.SetActive(true);

            isCancelFishing = true;
            isReleaseButton = false;
            holdingButtonTimer += Time.deltaTime;

            if (holdingButtonTimer < holdingButtonMaxTimer)
            {
                holdButtonImage.fillAmount = holdingButtonTimer / holdingButtonMaxTimer;
            }

            if (holdingButtonTimer >= holdingButtonMaxTimer)
            {
                FishingManager.instance.CancelFishing();
                FishingRodManager.instance.bobberThrowed = false;

                holdingButtonTimer = 0;
                holdButtonImage.fillAmount = 0f;
                isCancelFishing = false;
                holdButtonImage.gameObject.SetActive(false);
            }
        }

        if (isReleaseButton && FishingManager.instance.isInFishingRod)
        {
            if (!PlayerManager.instance.isPressingRT)
            {
                if (PlayerManager.instance.playerInventory.inventory.currentFishOnMe < 3)
                {
                    if (Input.GetAxis("Right Trigger") == 0 && axisRelease && !FishingRodManager.instance.bobberThrowed)
                    {
                        if (goodZone)
                        {
                            StartCoroutine("Throw");
                            //Play Sound
                            AkSoundEngine.PostEvent("OnWireLaunched", gameObject);
                            FishingRodManager.instance.bobber.GetComponent<Bobber>().SetBezierPoint(bobberAura.transform.position);
                        }
                        else
                        {
                            StartCoroutine("FailedThrow");
                        }

                        isMax = false;
                        axisRelease = false;
                        bobberAura.SetActive(false);
                    }

                    if ((Input.GetAxis("Right Trigger") > 0.1f) && !FishingRodManager.instance.bobberThrowed)                               //First Press RT
                    {
                        axisRelease = true;
                        if ((transform.localRotation.eulerAngles.x > 280f || (transform.localRotation.eulerAngles.x >= 0 && transform.localRotation.eulerAngles.x < 1)) && !isMax)
                        {
                            transform.Rotate(new Vector3(-2f, 0f, 0f));
                            PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber += 0.6f;
                        }
                        else
                        {
                            isMax = true;
                        }

                        //Aura bobber
                        raycastOrigin = mainCamera.transform.position;

                        if (Physics.Raycast(raycastOrigin, mainCamera.transform.forward, out hit, 80, layer))
                        {
                            bobberAura.transform.position = new Vector3(hit.point.x, hit.point.y + 0.7f, hit.point.z);
                            bobberAura.SetActive(true);

                            if (hit.distance > FishingRodManager.instance.fishingLine.fMax || hit.collider.gameObject.layer == 8)
                            {
                                bobberAura.gameObject.GetComponent<SpriteRenderer>().color = redArrow;
                                goodZone = false;
                            }
                            else
                            {
                                bobberAura.gameObject.GetComponent<SpriteRenderer>().color = yellowArrow;
                                goodZone = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Trop de poisson");
                }
            }
            else
            {
                if (Input.GetAxis("Right Trigger") == 0)
                {
                    PlayerManager.instance.isPressingRT = false;
                }
            }
        }


        #region Movement Canne à pêche Aerial et Claquage
        /*
        if (isFell)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x < 50f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(6f, 0f, 0f));
            }
            else
            {
                isFell = false;
                isFell2 = true;
            }
        }

        if (isFell2)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x > 10f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(-8f, 0f, 0f));
            }
            else
            {
                isFell2 = false;
            }
        }

        if (isAerial)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x < 20f || fishingRodAnchor.transform.localRotation.eulerAngles.x > 320f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(-6f, 0f, 0f));
            }
            else
            {
                isAerial = false;
                isAerial2 = true;
            }
        }

        if (isAerial2)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x > 10f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(8f, 0f, 0f));
            }
            else
            {
                isAerial2 = false;
            }
        }*/
        #endregion

        #region Movement Fail Canne à pêche Aerial et Claquage
        /*
        if (isFellFail)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x < 15f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(6f, 0f, 0f));
            }
            else
            {
                isFellFail = false;
                isFellFail2 = true;
            }
        }

        if (isFellFail2)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x > 10f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(-8f, 0f, 0f));
            }
            else
            {
                isFellFail2 = false;
            }
        }
        
        if (isAerialFail)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x < 20f || fishingRodAnchor.transform.localRotation.eulerAngles.x > 360f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(-6f*Time.deltaTime, 0f, 0f));
            }
            else
            {
                isAerialFail = false;
                isAerialFail2 = true;
            }
        }

        if (isAerialFail2)
        {
            if (fishingRodAnchor.transform.localRotation.eulerAngles.x > 10f)
            {
                fishingRodAnchor.transform.Rotate(new Vector3(8f*Time.deltaTime, 0f, 0f));
            }
            else
            {
                isAerialFail2 = false;
            }
        }
        */
        #endregion

    }

    IEnumerator Throw()
    {
        for (float t = 0f; t < 1f; t += 2f * Time.fixedDeltaTime)
        {
            if(t > 0.25f)
            {
                result = true;
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FishingRodRotaBase, t);
            yield return null;
        }
        transform.localRotation = FishingRodRotaBase;
    }

    IEnumerator FailedThrow()
    {
        for (float t = 0f; t < 1f; t += 2f * Time.fixedDeltaTime)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FishingRodRotaBase, t);
            yield return null;
        }
        transform.localRotation = FishingRodRotaBase;
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.Euler(360, 0, 0);
        transform.parent.transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.parent.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    #region Rotation canne à pêche lorsque Aerial et Claquage
    public void FellRotation()
    {
        //isFell = true;
        fishingRodAnchor.GetComponent<Animator>().SetBool("FellSucceed", true);
        StartCoroutine(ResetBoolAnimator());
    }

    public void AerialRotation()
    {
        //isAerial = true;
        fishingRodAnchor.GetComponent<Animator>().SetBool("ProjectionSucceed", true);
        StartCoroutine(ResetBoolAnimator());
    }
    #endregion

    #region Rotation Fail canne à pêche lorsque Aerial et Claquage
    public void FellFailRotation()
    {
        //isFellFail = true;
        fishingRodAnchor.GetComponent<Animator>().SetBool("FellFail", true);
        StartCoroutine(ResetBoolAnimator());
    }

    public void AerialFailRotation()
    {
        //isAerialFail = true;
        fishingRodAnchor.GetComponent<Animator>().SetBool("ProjectionFail", true);
        StartCoroutine(ResetBoolAnimator());
    }
    #endregion

    IEnumerator ResetBoolAnimator()
    {
        yield return new WaitForSeconds(0.1f);
        fishingRodAnchor.GetComponent<Animator>().SetBool("ProjectionFail", false);
        fishingRodAnchor.GetComponent<Animator>().SetBool("ProjectionSucceed", false);
        fishingRodAnchor.GetComponent<Animator>().SetBool("FellFail", false);
        fishingRodAnchor.GetComponent<Animator>().SetBool("FellSucceed", false);
    }
}

