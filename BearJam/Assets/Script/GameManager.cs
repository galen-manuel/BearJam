using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */


	/* INITIALIZATION */

	private void Start() {
		AudioManager.Instance.PlaySound(AudioTracks.AMBIENT_BEACH_DAY, 0.3f);

		StartCoroutine("PlaySeagullSound");
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */

	private IEnumerator PlaySeagullSound() {
		yield return new WaitForSeconds(Random.Range(10, 21));

		if (Random.value > 0.7f) {
			AudioManager.Instance.PlaySound(AudioTracks.AMBIENT_BEACH_DAY, 0.3f);
		}

		StartCoroutine(PlaySeagullSound());
	}

	/* EVENT HANDLERS */

	private void OnMiniGameStepComplete() {
		
	}

	private void OnMiniGameComplete() {
		
	}
}