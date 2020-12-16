using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Values")]
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    private float x = 0f;
    private float z = 0f;

    private void Start()
    {
        speed = PlayerManager.instance.speed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!PlayerManager.instance.isOnMenu && !PlayerManager.instance.isPause && !PlayerManager.instance.isFishStock)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.fixedDeltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKey(KeyCode.P))
            {
                PlayerManager.instance.CHEAT_ShowData();
            }

            if (Input.GetButton("A Button"))
            {
                if (PlayerManager.instance.cb.isNearChest)
                {
                    Debug.Log("ff");
                    PlayerManager.instance.OpenChestMenu();
                }

                if (PlayerManager.instance.ch.isNearFishingStock)
                {
                    PlayerManager.instance.OpenFishingStockMenu();
                }
            }
        }

        if (Input.GetButtonDown("Menu Button") && !PlayerManager.instance.isPause && !PlayerManager.instance.isFishStock)
        {
            PlayerManager.instance.OpenInventoryMenu();
        }
        else if((Input.GetButtonDown("Menu Button") || Input.GetButton("B Button")) && PlayerManager.instance.isPause && !PlayerManager.instance.isQuestMenu)
        {
            PlayerManager.instance.LeaveInventoryMenu();
        }

        if(Input.GetButtonDown("X Button") && PlayerManager.instance.isPause)
        {
            PlayerManager.instance.OpenQuestMenu();
        }
        else if(Input.GetButtonUp("B Button") && PlayerManager.instance.isPause && PlayerManager.instance.isQuestMenu)
        {
            PlayerManager.instance.LeaveQuestMenu();
        }

        if (Input.GetButton("B Button") && PlayerManager.instance.isFishStock)
        {
            PlayerManager.instance.LeaveFishingStockMenu();
        }
    }
}
