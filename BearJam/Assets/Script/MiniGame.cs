using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class MiniGame : MonoBehaviour {
	/* ENUMS */

	public enum Type{
		Defined_Sequence, // press a pre-defined sequnce of input tasks
		Random_Sequence, // press a random-defined sequence of input tasks
		ABDecision, // press a single button sequence
		Movement,
		Patience
	}

	/* CONSTANTS */


	/* PUBLIC VARS */

	public string instruction;

	protected Queue<InputTask> _actionsToPerform;
	// Input task - Data class holding a control type and action?
	// this is a series of input

	/* PRIVATE VARS */

	protected bool _isComplete;
	protected float _timeToComplete;

	/* INITIALIZATION */

	protected void Start() {
		
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */
	protected void Update() {
		_timeToComplete -= Time.deltaTime;
		if (_timeToComplete <= 0f) {
			_isComplete = false;
		}
	}


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	protected void OnControllerButtonPressed(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was pressed", pButton);
		if (pButton == _actionsToPerform.Peek().controlType) {
			if(_actionsToPerform.Peek().inputType == InputTask.Type.PressHold) {

			}
			else {
				_actionsToPerform.Dequeue();
			}
		}

		if (_actionsToPerform.Count == 0) {
			// Action complete.
		}
	}

	protected void OnControllerButtonReleased(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was released", pButton);
	}
}