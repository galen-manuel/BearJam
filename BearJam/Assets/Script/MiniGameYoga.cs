using UnityEngine;
using UnityEngine.UI;
using InControl;
using System.Collections.Generic;

public class MiniGameYoga : MiniGame {
	/* CONSTANTS */
	private const float ERROR_BUFFER = 0.35f;

	/* PUBLIC VARIABLES */
	public RectTransform fadedRT;
	public Image fadedImg;
	public RectTransform scalingRT;
	public LayoutElement layoutElement;

	/* PRIVATE VARIABLES */
	private float _timeToScale;
	private float _minTime;
	private float _maxTime;
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
		_minTime = 0f;
		_maxTime = 0f;
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
		_minTime = _timeToScale - ERROR_BUFFER;
		_maxTime = _timeToScale + ERROR_BUFFER;

		var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);

		Debug.Log("[YOGA] - Time To Scale: " + _timeToScale);
		Debug.LogFormat("[YOGA] - Low End {0} - High End {1}", _minTime, _maxTime);

		for(int i = 0; i < _controlPool.Count; i++) {
			GenerateNewControl();
		}

		_currentControl = _taskActions.Peek().controlType;
	}

	/* PUBLIC METHODS */
	public override void Update(float pDt) {
		_timer += pDt;
		if(_timer >= _maxTime) {
			Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
		}
	}

	public bool CheckGameComplete() {
		_taskActions.Dequeue();

		if(_taskActions.Count == 0) {
			Debug.Log("[YOGA] - GAME COMPLETE!");
			return true;
		}
		else {
			Debug.Log("[YOGA] - MOVING ON!!");
			_currentControl = _taskActions.Peek().controlType;
			_timer = 0f;
			return false;
		}
	}

	/* PRIVATE METHODS */
	private void GenerateNewControl() {
		// Generate a random control from the set of controls we defined above.
		InputControlType randControl = _controlPool[Random.Range(0, _controlPool.Count)];
		_taskActions.Enqueue(new ButtonAction(randControl));
	}

	/* EVENT HANDLERS */
	protected override void OnControllerButtonReleased(InputControlType pButton) {
		InputAction action = _taskActions.Peek();
		if(action.GetType() == typeof(ButtonAction)) {
			ButtonAction btnAction = action as ButtonAction;
			Debug.Log("[YOGA] - Task Count: " + _taskActions.Count);
			Debug.LogFormat("<color=red>[YOGA] - Timer: {0}</color>", _timer);
			Debug.LogFormat("[YOGA] - Button Pressed: {0} - Looking For Button: {1}", pButton, action.controlType);
			// Debug.Break();

			if(_timer >= _minTime && _timer <= _maxTime && action.controlType == pButton) {
				Debug.Log("[YOGA] - PASS");
				Messenger.Broadcast(Messages.MINI_GAME_STEP_COMPLETE, typeof(MiniGameYoga));
			}
			else {
				if(_timer <= _minTime) {
					Debug.LogFormat("[YOGA] - TOO EARLY!!");
					// Debug.Break();
				}
				Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, false);
			}
		}
	}
}