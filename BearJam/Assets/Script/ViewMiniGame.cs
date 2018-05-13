using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ViewMiniGame : MonoBehaviour {
	/* PUBLIC VARIABLES */
	[SerializeField] private TextMeshProUGUI _instructions;
	[SerializeField] private TextMeshProUGUI _debugText;
	[SerializeField] private RectTransform _inputActionPanel;

	/* PRIVATE VARS */
	private Constants.GameType _currentGameType;
	private MiniGame _currentGame;
	private MiniGameFactory _miniGameFactory;
	private bool _updateMinigame;

	/* INITIALIZATION */
	void Awake() {
		_miniGameFactory = new MiniGameFactory();
		SpawnNewMiniGame();
	}

	void Start() {
		Messenger.AddListener<bool>(Messages.MINI_GAME_COMPLETE, OnMiniGameComplete);
		Messenger.AddListener<System.Type>(Messages.MINI_GAME_STEP_COMPLETE, OnMiniGameStepComplete);
	}

	void Update() {
		if(_updateMinigame) {
			_instructions.text = string.Format("{0} Time: {1}", _currentGame?.Instructions, _currentGame?.Timer.ToString("00.00"));
			_currentGame?.Update(Time.deltaTime);
		}
	}

	/* PRIVATE METHODS */
	private void SpawnNewMiniGame() {
		float delay = 0f;

		// Clear game data.
		_currentGame?.Cleanup();
		_currentGame = null;

		// Clear input panel first.
		for(int i = 0; i < _inputActionPanel.childCount; i++) {
			RectTransform rt = _inputActionPanel.GetChild(i) as RectTransform;
			rt.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack).OnComplete(() => Destroy(rt.gameObject));
		}
		delay += (_inputActionPanel.childCount == 0) ? 0f : 0.4f;

		DOTween.Sequence().InsertCallback(delay, () => {
			_currentGameType = (Constants.GameType)Random.Range(0, (int)Constants.GameType.AllTypes);
			_currentGameType = Constants.GameType.Yoga;
			_currentGame = _miniGameFactory.CreateMiniGame(Constants.GameType.Yoga);
			_updateMinigame = true;

			_instructions.text = _currentGame.Instructions;

			ShowNextInput(_currentGame.GetType());
		});
	}

	private IEnumerator DeepCopyLayout(RectTransform scalingRT, RectTransform fadedRt) {
		yield return new WaitForEndOfFrame();

		scalingRT.localScale = Vector3.zero;
		scalingRT.anchorMin = new Vector2(fadedRt.anchorMin.x, fadedRt.anchorMin.y);
		scalingRT.anchorMax = new Vector2(fadedRt.anchorMax.x, fadedRt.anchorMax.y);
		scalingRT.anchoredPosition = new Vector2(fadedRt.anchoredPosition.x, fadedRt.anchoredPosition.y);
		scalingRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fadedRt.rect.width);
		scalingRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fadedRt.rect.height);

		scalingRT.gameObject.SetActive(true);
	}

	private void ShowNextInput(System.Type pType) {
		if(pType == typeof(MiniGameYoga)) {
			MiniGameYoga game = _currentGame as MiniGameYoga;
			game.fadedRT = Instantiate(Resources.Load<GameObject>("Prefabs/Controller/" + game.CurrentControl.ToString()), Vector3.zero,
						 Quaternion.identity, _inputActionPanel).GetComponent<RectTransform>();
			game.scalingRT = Instantiate(Resources.Load<GameObject>("Prefabs/Controller/" + game.CurrentControl.ToString()), Vector3.zero,
				 Quaternion.identity, _inputActionPanel).GetComponent<RectTransform>();

			game.layoutElement = game.scalingRT.GetComponent<LayoutElement>();
			game.layoutElement.ignoreLayout = true;

			game.fadedRT.localScale = Vector3.zero;

			game.scalingRT.gameObject.SetActive(false);
			StartCoroutine(DeepCopyLayout(game.scalingRT, game.fadedRT));

			game.fadedImg = game.fadedRT.GetComponent<Image>();
			game.fadedImg.SetAlpha(0.5f);

			game.fadedRT.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
			DOTween.Sequence().InsertCallback(0.5f, () => {
				_updateMinigame = true;
				game.scalingRT.DOScale(Vector3.one, game.TimeToScale);
			});
		}
	}

	/* EVENT HANDLERS */
	private void OnMiniGameStepComplete(System.Type pType) {
		_updateMinigame = false;

		if(pType == typeof(MiniGameYoga)) {
			MiniGameYoga game = _currentGame as MiniGameYoga;
			bool isComplete = game.CheckGameComplete();

			_debugText.text = string.Format("Step Complete: PASS");

			if(isComplete) {
				Messenger.Broadcast(Messages.MINI_GAME_COMPLETE, true);
			}
			else {
				game.fadedRT.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack);
				game.scalingRT.DOScale(Vector3.zero, 0.35f * game.scalingRT.localScale.x / 1.0f).SetEase(Ease.InBack);
				DOTween.Sequence().InsertCallback(0.45f, () => {
					Destroy(game.scalingRT.gameObject);
					Destroy(game.fadedRT.gameObject);
					ShowNextInput(game.GetType());
				});
			}
		}
	}

	private void OnMiniGameComplete(bool pDidComplete) {
		// Debug.LogFormat("{0} Mini Game {1}!", System.Enum.GetName(typeof(Constants.GameType), _currentGameType),
		// 	(pDidComplete ? "Complete" : "Failed"));


		if(pDidComplete == false) {
			_debugText.text = string.Format("Step Complete: FAIL");
		}
		SpawnNewMiniGame();
	}
}
