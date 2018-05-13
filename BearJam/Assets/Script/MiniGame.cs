using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

[System.Serializable]
public class MiniGame {
	/* ENUMS */

	/* CONSTANTS */
	private const float TIME_TO_COMPLETE = 5.0f;


	/* PUBLIC VARS */

	/* PRIVATE VARS */
	protected Queue<InputAction> _task;
	protected InputControlType[] _availableControls;
	protected string _instructions;
	private int _playerIndex;
	private float _timer;

	/* PROPERTIES */
	public string Instructions {
		get { return _instructions; }
	}

	/* INITIALIZATION */
	public MiniGame(int pPlayerIndex) {
		_playerIndex = pPlayerIndex;
		_timer = 0f;

		_availableControls = new InputControlType[] {
			InputControlType.Action1,
			InputControlType.Action2,
			InputControlType.Action3,
			InputControlType.Action4,
			InputControlType.DPadUp,
			InputControlType.DPadDown,
			InputControlType.DPadLeft,
			InputControlType.DPadRight,
			InputControlType.LeftBumper,
			InputControlType.RightBumper,
			InputControlType.LeftTrigger,
			InputControlType.RightTrigger,
			InputControlType.LeftStickButton,
			InputControlType.RightStickButton,
		};

		Messenger.AddListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, _playerIndex),
			OnControllerButtonPressed);
		Messenger.AddListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, _playerIndex),
			OnControllerButtonReleased);
	}

	/* PROPERTIES */

	/* PUBLIC FUNCTIONS */
	public void Update(float pDt) {
		_timer += pDt;
		if(_timer >= TIME_TO_COMPLETE) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
		}
	}

	public InputControlType GetRandomControl() {
		int randIndex = Random.Range(0, _availableControls.Length);
		return _availableControls[randIndex];
	}


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	protected void OnControllerButtonPressed(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was pressed", pButton);
		if(pButton == _task.Peek().controlType) {
			if(_task.Peek().GetType() == typeof(JoystickAction)) {
				Debug.Log("Joystick Task Detected");
			}
			else {
				_task.Dequeue();
			}
		}

		if(_task.Count == 0) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, true);
		}
	}

	protected void OnControllerButtonReleased(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was released", pButton);
	}
}