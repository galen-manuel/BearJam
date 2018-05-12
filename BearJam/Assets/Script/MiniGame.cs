using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class MiniGame : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */

	public string instruction;
	private Queue<InputControlType> _performedActions;
	private InputDevice _activeDevice;

	/* PRIVATE VARS */


	/* INITIALIZATION */

	private void Start() {
		
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	private void OnControllerButtonPressed(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was pressed", pButton);
	}

	private void OnControllerButtonReleased(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was released", pButton);
	}
}