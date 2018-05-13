using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

[System.Serializable]
public class MiniGame {
	/* ENUMS */

	/* CONSTANTS */


	/* PUBLIC VARS */

	/* PRIVATE VARS */
	protected Queue<InputAction> _taskActions;
	protected Dictionary<Constants.ActionType, InputControlType[]> _availableControls;
	protected string _instructions;
	protected float _timeToComplete;
	protected float _timer;
	private int _playerIndex;
	private float _holdTimer;
	private float _holdTime;
	private bool _isHoldTimerActive;

	/* PROPERTIES */
	public string Instructions {
		get { return _instructions; }
	}

	public float Timer {
		get { return _timer; }
	}

	/* INITIALIZATION */
	public MiniGame(int pPlayerIndex, float pTimeToComplete = 5.0f) {
		_playerIndex = pPlayerIndex;
		_timeToComplete = pTimeToComplete;
		_timer = 0f;
		_holdTimer = 0f;
		_holdTime = 0f;
		_taskActions = new Queue<InputAction>();

		_availableControls = new Dictionary<Constants.ActionType, InputControlType[]> {
			[Constants.ActionType.Buttons] = new InputControlType[] {
				InputControlType.Action1,
				InputControlType.Action3,
				InputControlType.Action2,
				InputControlType.Action4
			},
			[Constants.ActionType.DPad] = new InputControlType[] {
				InputControlType.DPadUp,
				InputControlType.DPadDown,
				InputControlType.DPadLeft,
				InputControlType.DPadRight
			},
			[Constants.ActionType.Bumpers] = new InputControlType[] {
				InputControlType.LeftBumper,
				InputControlType.RightBumper
			},
			[Constants.ActionType.Triggers] = new InputControlType[] {
				InputControlType.LeftTrigger,
				InputControlType.RightTrigger
			},
			[Constants.ActionType.BumpersAndTriggers] = new InputControlType[] {
				InputControlType.LeftBumper,
				InputControlType.RightBumper,
				InputControlType.LeftTrigger,
				InputControlType.RightTrigger
			},
			[Constants.ActionType.Joysticks] = new InputControlType[] {
				InputControlType.LeftStickButton,
				InputControlType.RightStickButton
			}
		};

		Messenger.AddListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, _playerIndex),
			OnControllerButtonPressed);
		Messenger.AddListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, _playerIndex),
			OnControllerButtonReleased);
	}

	public void Cleanup() {
		Messenger.RemoveListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, _playerIndex),
			OnControllerButtonPressed);
		Messenger.RemoveListener<InputControlType>(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, _playerIndex),
			OnControllerButtonReleased);
	}

	/* PROPERTIES */

	/* PUBLIC FUNCTIONS */
	public virtual void Update(float pDt) {
		_timer += pDt;
		if(_timer >= _timeToComplete) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
		}

		if(_isHoldTimerActive) {
			_holdTimer += _holdTime;
			if(_holdTimer >= _holdTime) {
				Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
			}
		}
	}

	public InputControlType GetRandomControl() {
		Constants.ActionType randKey = (Constants.ActionType)Random.Range(0, (int)Constants.ActionType.All);
		int randIndex = Random.Range(0, _availableControls[randKey].Length);
		return _availableControls[randKey][randIndex];
	}


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	protected virtual void OnControllerButtonPressed(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was pressed", pButton);
		InputAction action = _taskActions.Peek();
		if(pButton == action.controlType) {
			if(action.GetType() == typeof(JoystickAction)) {
				JoystickAction joystickAction = action as JoystickAction;
				Debug.Log("Joystick Task Detected - Pressed");
			}
			else if(action.GetType() == typeof(ButtonAction)) {
				ButtonAction btnAction = action as ButtonAction;
				if(btnAction.isHold) {
					_isHoldTimerActive = true;
					_holdTime = btnAction.holdTime;
				}
			}
		}

		if(_taskActions.Count == 0) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, true);
		}
	}

	protected virtual void OnControllerButtonReleased(InputControlType pButton) {
		Debug.LogFormat("Controller button {0} was released", pButton);

		InputAction action = _taskActions.Peek();
		if(pButton == action.controlType) {
			if(action.GetType() == typeof(JoystickAction)) {
				JoystickAction joystickAction = action as JoystickAction;
				Debug.Log("Joystick Task Detected - Released");
			}
			else if(action.GetType() == typeof(ButtonAction)) {
				ButtonAction btnAction = action as ButtonAction;
				if(btnAction.isHold) {
					_isHoldTimerActive = false;
					_holdTime = 0f;
					_holdTimer = 0f;

					// Add a check for holdTimer buffer to see if withing range then deque task
				}
			}
		}

		if(_taskActions.Count == 0) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, true);
		}
	}
}