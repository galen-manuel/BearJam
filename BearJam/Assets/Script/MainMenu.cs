using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	[SerializeField] private Button[] _buttons;

	private int _currentIndex;

	/* INITIALIZATION */

	private void Start() {
		_currentIndex = 0;

		_buttons[_currentIndex].Select();
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	public void OnPlaySelected() {
		
	}

	public void OnOptionsSelected() {
		
	}

	public void OnCreditsSelected() {
		
	}
}