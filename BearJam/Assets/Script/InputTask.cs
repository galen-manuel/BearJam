using UnityEngine;
using System.Collections;

public class InputAction {
	/* PUBLIC VARS */
	public InControl.InputControlType controlType;

	/* INITIALIZATION */
	public InputAction(InControl.InputControlType pControlType) {
		controlType = pControlType;
	}
}

public class ButtonAction : InputAction {
	/* PUBLIC VARS */
	public bool isHold;
	public float holdTime;

	/* INITIALIZATION */
	public ButtonAction(InControl.InputControlType pControlType, bool pIsHold = false, float pHoldTime = 0f)
		: base(pControlType) {
		isHold = pIsHold;
		holdTime = pHoldTime;
	}
}

public class JoystickAction : InputAction {
	/* PUBLIC VARS */

	/* INITIALIZATION */
	public JoystickAction(InControl.InputControlType pControlType) : base(pControlType) {

	}
}