using UnityEngine;
using System.Collections;

public class InputTask {
	/* ENUMS */

	public enum Type {
		PressRelease,
		PressHold,
		JoystickMovement
	}

	/* CONSTANTS */


	/* PUBLIC VARS */

	public InControl.InputControlType controlType;
	public Type inputType;
	public float holdTime;

	/* PRIVATE VARS */


	/* INITIALIZATION */

	public InputTask(InControl.InputControlType pControlType, Type pInputType, float pHoldTime = -1) {
		controlType = pControlType;
		inputType = pInputType;
		holdTime = pHoldTime;
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

}