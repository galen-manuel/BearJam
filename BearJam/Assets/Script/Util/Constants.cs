using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	/* ENUMS */
	public enum GameType {
		PresetSequence, // press a pre-defined sequnce of input tasks
		Yoga, // press a random-defined sequence of input tasks
		ABDecision, // press a single button sequence
		Movement,
		Patience,
		AllTypes
	}

	public enum ActionType {
		Buttons,
		DPad,
		Triggers,
		Bumpers,
		BumpersAndTriggers,
		Joysticks,
		All
	}


	/* CONSTANTS */

	public const int MAX_PLAYER_COUNT = 4;

	/* PUBLIC VARS */


	/* PRIVATE VARS */


	/* INITIALIZATION */


	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

}