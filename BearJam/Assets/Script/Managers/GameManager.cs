using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */


	/* INITIALIZATION */

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		Messenger.AddListener(Messages.BEGING_START_GAME_SEQUENCE, OnBeginStartGameSequence);
		Messenger.AddListener(Messages.START_GAME, OnStartGame);
		Messenger.AddListener(Messages.START_MINI_GAME, OnStartMiniGame);
		Messenger.AddListener(Messages.MINI_GAME_STEP_COMPLETE, OnMiniGameStepComplete);
		Messenger.AddListener(Messages.MINI_GAME_COMPLETE, OnMiniGameComplete);

		AudioManager.Instance.PlayMusic(AudioTracks.AMBIENT_BEACH_DAY, 0.1f);

		StartCoroutine("PlaySeagullSound");
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */

	private IEnumerator PlaySeagullSound() {
		yield return new WaitForSeconds(Random.Range(10, 21));

		if (Random.value > 0.7f) {
			AudioManager.Instance.PlaySound(AudioTracks.SFX_SEAGULLS, 0.3f);
		}

		StartCoroutine(PlaySeagullSound());
	}

	/* EVENT HANDLERS */

	private void OnBeginStartGameSequence() {
		
	}

	private void OnStartGame() {
		AudioManager.Instance.PlayMusic(AudioTracks.GAME_MUSIC, 0.5f);
	}

	private void OnStartMiniGame() {
		
	}

	private void OnMiniGameStepComplete() {
		
	}

	private void OnMiniGameComplete() {
		
	}
}