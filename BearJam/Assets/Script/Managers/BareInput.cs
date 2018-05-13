using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class BareInput : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	private static BareInput s_instance;
	private List<InputDevice> _activeDevices;

	/* INITIALIZATION */

	private void Awake() {
		if(s_instance == null) {
			s_instance = this;

			DontDestroyOnLoad(gameObject);
			_activeDevices = new List<InputDevice>();

			InputManager.OnDeviceAttached += OnDeviceAttached;
			InputManager.OnDeviceDetached += OnDeviceDetached;

			for(int i = InputManager.Devices.Count - 1; i >= 0; --i) {
				Debug.LogFormat("Device {0} Name: {1}", i, InputManager.Devices[0].Name);
				if(InputManager.Devices[i].IsAttached) {
					_activeDevices.Add(InputManager.Devices[i]);
				}
			}

			if(_activeDevices.Count == 0) {
				Messenger.Broadcast(Messages.NO_CONTROLLER_CONNECTED);
			}
		}
		else {
			if (s_instance != this) {
				Destroy(gameObject);
			}
		}
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */

	private void Update() {
		for(int i = _activeDevices.Count - 1; i >= 0; --i) {
			var device = _activeDevices[i];

			#region Action Buttons
			if(device.Action1.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.Action1);
			}
			else if(device.Action1.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.Action1);
			}

			if(device.Action2.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.Action2);
			}
			else if(device.Action2.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.Action2);
			}

			if(device.Action3.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.Action3);
			}
			else if(device.Action3.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.Action3);
			}

			if(device.Action4.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.Action4);
			}
			else if(device.Action4.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.Action4);
			}
			#endregion

			#region D-Pad
			if(device.DPadUp.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.DPadUp);
			}
			else if(device.DPadUp.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.DPadUp);
			}

			if(device.DPadDown.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.DPadDown);
			}
			else if(device.DPadDown.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.DPadDown);
			}

			if(device.DPadLeft.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.DPadLeft);
			}
			else if(device.DPadLeft.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.DPadLeft);
			}

			if(device.DPadRight.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.DPadRight);
			}
			else if(device.DPadRight.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.DPadRight);
			}
			#endregion

			#region Bumpers
			if(device.LeftBumper.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.LeftBumper);
			}
			else if(device.LeftBumper.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.LeftBumper);
			}

			if(device.RightBumper.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.RightBumper);
			}
			else if(device.RightBumper.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.RightBumper);
			}
			#endregion

			#region Triggers
			if(device.LeftTrigger.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.LeftTrigger);
			}
			else if(device.LeftTrigger.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.LeftTrigger);
			}

			if(device.RightTrigger.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.RightTrigger);
			}
			else if(device.RightTrigger.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.RightTrigger);
			}
			#endregion

			#region Joystick Buttons
			if(device.LeftStickButton.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.LeftStickButton);
			}
			else if(device.LeftStickButton.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.LeftStickButton);
			}

			if(device.RightStickButton.WasPressed) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_PRESSED, i), InputControlType.RightStickButton);
			}
			else if(device.RightStickButton.WasReleased) {
				Messenger.Broadcast(string.Format("{0}{1}", Messages.CONTROLLER_BUTTON_RELEASED, i), InputControlType.RightStickButton);
			}
			#endregion
		}
	}

/* EVENT HANDLERS */

	private void OnDeviceAttached(InputDevice pDevice) {
		_activeDevices.Add(pDevice);
	}

	private void OnDeviceDetached(InputDevice pDevice) {
		_activeDevices.Remove(pDevice);

		if (_activeDevices.Count == 0) {
			Messenger.Broadcast(Messages.NO_CONTROLLER_CONNECTED);
		}
	}
}