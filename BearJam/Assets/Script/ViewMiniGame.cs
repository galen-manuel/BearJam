using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMiniGame : MonoBehaviour {
	/* PUBLIC VARIABLES */
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
		if(_currentGame != null) {
			_currentGame.Update(Time.deltaTime);
		}
	}

	/* PRIVATE METHODS */
	private void SpawnNewMiniGame() {
		_currentGameType = (Constants.GameType)Random.Range(0, (int)Constants.GameType.AllTypes);
		_currentGameType = Constants.GameType.RandomSequence;
		_currentGame = _miniGameFactory.CreateMiniGame(Constants.GameType.RandomSequence);
	}

	/* EVENT HANDLERS */
	private void OnMiniGameComplete(bool pDidComplete) {
		Debug.LogFormat("{0} Mini Game {1}!", System.Enum.GetName(typeof(Constants.GameType), _currentGameType),
			(pDidComplete ? "Complete" : "Failed"));

		SpawnNewMiniGame();
	}
}
