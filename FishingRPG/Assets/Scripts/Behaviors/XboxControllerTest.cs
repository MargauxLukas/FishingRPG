using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxControllerTest : MonoBehaviour
{

	public bool aButton;
	public bool bButton;
	public bool xButton;
	public bool yButton;
	public bool leftBumper;
	public bool rightBumper;
	public float leftTrigger;
	public float rightTrigger;
	public bool viewButton;
	public bool menuButton;
	public bool lClickButton;
	public bool rClickButton;
	public bool xBoxGuideHomeButton;
	public float leftAnalogStickHorizontal;
	public float rightAnalogStickHorizontal;
	public float leftAnalogStickVertical;
	public float rightAnalogStickVertical;
	public float dPadHorizontal;
	public float dPadVertical;


	void Update()
	{
		aButton = Input.GetButton("A Button");
		bButton = Input.GetButton("B Button");
		xButton = Input.GetButton("X Button");
		yButton = Input.GetButton("Y Button");
		leftBumper = Input.GetButton("Left Bumper");
		rightBumper = Input.GetButton("Right Bumper");
		leftTrigger = Input.GetAxis("Left Trigger");
		rightTrigger = Input.GetAxis("Right Trigger");
		//viewButton = Input.GetButton("View Button");
		//menuButton = Input.GetButton("Menu Button");
		//lClickButton = Input.GetButton("L-Click Button");
		//rClickButton = Input.GetButton("R-Click Button");
		//xBoxGuideHomeButton = Input.GetButton("Xbox Guide/Home Button");
		leftAnalogStickHorizontal = Input.GetAxis("Horizontal");
		rightAnalogStickHorizontal = Input.GetAxis("Right Stick (Horizontal)");
		leftAnalogStickVertical = Input.GetAxis("Vertical");
		rightAnalogStickVertical = Input.GetAxis("Right Stick (Vertical)");
		dPadHorizontal = Input.GetAxis("D-Pad (Horizontal)");
		dPadVertical = Input.GetAxis("D-Pad (Vertical)");

		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}