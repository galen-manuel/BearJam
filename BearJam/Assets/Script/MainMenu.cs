using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	[SerializeField] private Button[] _buttons;

	private CanvasGroup _cg;
	private int _currentIndex;

	/* INITIALIZATION */

	private void Awake() {
		_cg = GetComponent<CanvasGroup>();
	}

	private void Start() {
		_currentIndex = 0;

		_buttons[_currentIndex].Select();
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */



	public void OnPlaySelected() {
		_cg.interactable = false;
		_cg.DOFade(0, 0.5f).OnComplete(() => Messenger.Broadcast(Messages.BEGING_START_GAME_SEQUENCE));
	}

	public void OnOptionsSelected() {
		
	}

	public void OnCreditsSelected() {
		
	}
}