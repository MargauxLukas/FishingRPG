using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class Rotate : MonoBehaviour
{
    Quaternion FishingRodRotaBase;
    Vector3 fishingRodPosBase;
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
    public GameObject bobberAuraCircle;
    public GameObject bobberAuraArrow;
    public Camera mainCamera;
    RaycastHit hit;
    Vector3 raycastOrigin;
    public LayerMask layer;

    public Color yellowArrow;
    public Color redArrow;
    public Color yellowCircle;
    public Color redCircle;

    bool goodZone = true;  //La flêche est dans une zone correcte ?

    [Header("Slowed View")]
    public PlayerView playerView;
    public float slowedSensitivity = 50f;
    float normalMouseSensitivity;

    public bool playWireOnce = false;

    private void Start()
    {
        FishingRodRotaBase = transform.localRotation;
        fishingRodPosBase = transform.localPosition;
        imageTarget = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
        normalMouseSensitivity = playerView.mouseSensitivity;
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
                            if (!playWireOnce)
                            {
                                //AkSoundEngine.PostEvent("OnWireLaunched", gameObject);
                                playWireOnce = true;
                            }
                            FishingRodManager.instance.bobber.GetComponent<Bobber>().SetBezierPoint(bobberAuraCircle.transform.position);
                        }
                        else
                        {
                            StartCoroutine("FailedThrow");
                        }

                        isMax = false;
                        axisRelease = false;
                        bobberAuraCircle.SetActive(false);
                    }

                    if ((Input.GetAxis("Right Trigger") > 0.1f) && !FishingRodManager.instance.bobberThrowed)                               //First Press RT
                    {
                        axisRelease = true;
                        if ((transform.localRotation.eulerAngles.x > 280f || (transform.localRotation.eulerAngles.x >= 0 && transform.localRotation.eulerAngles.x < 1)) && !isMax)
                        {
                            playerView.mouseSensitivity = slowedSensitivity;
                            transform.Rotate(new Vector3(-2f, 0f, 0f));
                            PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber += 0.6f;
                            transform.localPosition += new Vector3(0.007f, -0.007f, 0f);
                        }
                        else
                        {
                            isMax = true;
                        }

                        //Aura bobber
                        raycastOrigin = mainCamera.transform.position;

                        if (Physics.Raycast(raycastOrigin, mainCamera.transform.forward, out hit, 80, layer))
                        {
                            bobberAuraCircle.transform.position = new Vector3(hit.point.x, hit.point.y + 0.7f, hit.point.z);
                            bobberAuraCircle.SetActive(true);

                            if (hit.distance > FishingRodManager.instance.fishingLine.fMax || hit.collider.gameObject.layer == 8)
                            {
                                bobberAuraCircle.gameObject.GetComponent<SpriteRenderer>().color = redCircle;
                                bobberAuraArrow.gameObject.GetComponent<SpriteRenderer>().color = redArrow;
                                goodZone = false;
                            }
                            else
                            {
                                bobberAuraCircle.gameObject.GetComponent<SpriteRenderer>().color = yellowCircle;
                                bobberAuraArrow.gameObject.GetComponent<SpriteRenderer>().color = yellowArrow;
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
        playerView.mouseSensitivity = normalMouseSensitivity;


        for (float t = 0f; t < 1f; t += 2f * Time.fixedDeltaTime)
        {
            if(t > 0.25f)
            {
                result = true;
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FishingRodRotaBase, t);
            transform.localPosition = Vector3.Lerp(transform.localPosition, fishingRodPosBase, t);

            yield return null;
        }
        transform.localRotation = FishingRodRotaBase;
    }

    IEnumerator FailedThrow()
    {
        for (float t = 0f; t < 1f; t += 2f * Time.fixedDeltaTime)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FishingRodRotaBase, t);
            transform.localPosition = Vector3.Lerp(transform.localPosition, fishingRodPosBase, t);

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
        StartCoroutine(FellVibration());
        StartCoroutine(ResetBoolAnimator());
    }

    public void AerialRotation()
    {
        //isAerial = true;
        fishingRodAnchor.GetComponent<Animator>().SetBool("ProjectionSucceed", true);
        StartCoroutine(AerialVibration());
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
        StartCoroutine(FailAerialVibration());
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

    #region Vibrations (Aerial, Aerial Fail et Claquage)

    IEnumerator AerialVibration()
    {
        GamePad.SetVibration(0, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        GamePad.SetVibration(0, 0f, 0f);
    }

    IEnumerator FellVibration()
    {
        yield return new WaitForSeconds(0.5f);    //delay pour que la vibration arrive au bon moment
        GamePad.SetVibration(0, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        GamePad.SetVibration(0, 0f, 0f);
    }

    IEnumerator FailAerialVibration()
    {
        GamePad.SetVibration(0, 0.4f, 0.4f);
        yield return new WaitForSeconds(0.1f);
        GamePad.SetVibration(0, 0f, 0f);
    }

    #endregion
}

