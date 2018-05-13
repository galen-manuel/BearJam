using UnityEngine;
using System.Collections;

public class DebugMenu : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	private CanvasGroup _canvasGroup;

	/* INITIALIZATION */

	private void Awake() {
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */

	private void Update() {
		if (Input.GetKeyDown(KeyCode.D)) {
			_canvasGroup.interactable = !_canvasGroup.interactable;
			_canvasGroup.alpha = _canvasGroup.interactable ? 1.0f : 0.0f;
		}
	}

	/* EVENT HANDLERS */

}