using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewMiniGame : MonoBehaviour {
	/* PUBLIC VARIABLES */
	public TextMeshProUGUI instructions;

	/* PRIVATE VARS */
	private Constants.GameType _currentGameType;
	private MiniGame _currentGame;
	private MiniGameFactory _miniGameFactory;

	/* INITIALIZATION */
	void Awake() {
		_miniGameFactory = new MiniGameFactory();
		SpawnNewMiniGame();
	}

	void Start() {
		Messenger.AddListener<bool>(Messages.MINI_GAME_COMPLETE, OnMiniGameComplete);
	}

	void Update() {
		_currentGame?.Update(Time.deltaTime);
	}

	/* PRIVATE METHODS */
	private void SpawnNewMiniGame() {
		_currentGameType = (Constants.GameType)Random.Range(0, (int)Constants.GameType.AllTypes);
		_currentGameType = Constants.GameType.QuickTimeEvent;
		_currentGame = _miniGameFactory.CreateMiniGame(Constants.GameType.QuickTimeEvent);

		instructions.text = _currentGame.Instructions;
	}

	/* EVENT HANDLERS */
	private void OnMiniGameComplete(bool pDidComplete) {
		Debug.LogFormat("{0} Mini Game {1}!", System.Enum.GetName(typeof(Constants.GameType), _currentGameType),
			(pDidComplete ? "Complete" : "Failed"));

		SpawnNewMiniGame();
	}
}
