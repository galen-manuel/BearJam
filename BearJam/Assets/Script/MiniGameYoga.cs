using UnityEngine;
using InControl;
using System.Collections.Generic;

public class MiniGameYoga : MiniGame {
	/* CONSTANTS */
	private const float ERROR_BUFFER = 0.25f;

	/* PRIVATE VARIABLES */
	private float _timeToScale;
	private float _timer;
	private InputControlType _currentControl;
	private List<InputControlType> _controlPool;

	/* PROPERTIES */
	public float TimeToScale {
		get { return _timeToScale; }
	}

	public InputControlType CurrentControl {
		get { return _currentControl; }
	}

	/* INITIALIZATION */
	public MiniGameYoga(int pPlayerIndex, float pTimeToComplete) : base(pPlayerIndex, pTimeToComplete) {
		_instructions = "Watch your timing!";
		_timeToScale = 0f;
		_timer = 0f;
		_currentControl = InputControlType.None;
		_controlPool = new List<InputControlType>();

		InitTask();
	}

	private void InitTask() {
		// Generate a random key for sets of controls
		Constants.ActionType[] keys = new Constants.ActionType[] { Constants.ActionType.Buttons, Constants.ActionType.BumpersAndTriggers };
		Constants.ActionType randKey = keys[Random.Range(0, keys.Length)];

		// BumpersAndTriggers is a special override not used in the main dictionary.
		_controlPool.AddRange(_availableControls[randKey]);

		// Set time to scale each button
		_timeToScale = _timeToComplete / _controlPool.Count;

		for(int i = 0; i < _controlPool.Count; i++) {
			GenerateNewControl();
		}
	}

	public override void Update(float pDt) {
		_timer += pDt;
		if(_timer >= _timeToScale + ERROR_BUFFER) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
		}
	}

	private void GenerateNewControl() {
		// Generate a random control from the set of controls we defined above.
		_currentControl = _controlPool[Random.Range(0, _controlPool.Count)];
		_taskActions.Enqueue(new ButtonAction(_currentControl));
	}

	protected override void OnControllerButtonReleased(InputControlType pButton) {
		InputAction action = _taskActions.Peek();
		if(action.GetType() == typeof(ButtonAction)) {

			ButtonAction btnAction = action as ButtonAction;
			if(_timer >= _timeToScale - ERROR_BUFFER && _timer <= _timeToScale + ERROR_BUFFER) {
				Messenger.Broadcast(Messages.MINI_GAME_STEP_COMPLETE, typeof(MiniGameYoga));
				_taskActions.Dequeue();
			}

			if(_taskActions.Count == 0) {
				Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, true);
			}
		}
	}
}