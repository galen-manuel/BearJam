using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ViewMiniGame : MonoBehaviour {
	/* PUBLIC VARIABLES */
	[SerializeField] private TextMeshProUGUI _instructions;
	[SerializeField] private RectTransform _inputActionPanel;

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
		Messenger.AddListener<System.Type>(Messages.MINI_GAME_STEP_COMPLETE, OnMiniGameStepComplete);
	}

	void Update() {
		_currentGame?.Update(Time.deltaTime);
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

			_instructions.text = _currentGame.Instructions;

			if(_currentGame.GetType() == typeof(MiniGameYoga)) {
				MiniGameYoga game = _currentGame as MiniGameYoga;
				RectTransform fadedRt = Instantiate(Resources.Load<GameObject>("Prefabs/Controller/" + game.CurrentControl.ToString()), Vector3.zero,
					 Quaternion.identity, _inputActionPanel).GetComponent<RectTransform>();
				RectTransform scalingRT = Instantiate(Resources.Load<GameObject>("Prefabs/Controller/" + game.CurrentControl.ToString()), Vector3.zero,
					 Quaternion.identity, _inputActionPanel).GetComponent<RectTransform>();

				LayoutElement layoutElement = scalingRT.GetComponent<LayoutElement>();
				layoutElement.ignoreLayout = true;

				fadedRt.localScale = Vector3.zero;

				// Set Properties
				// scalingRT = fadedRt;
				scalingRT.gameObject.SetActive(false);
				StartCoroutine(DeepCopyTransform(scalingRT, fadedRt));

				Image img = fadedRt.GetComponent<Image>();
				img.SetAlpha(0.5f);

				fadedRt.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
				scalingRT.DOScale(Vector3.one, game.TimeToScale).SetDelay(0.5f);
			}
		});

	}
	private IEnumerator DeepCopyTransform(RectTransform scalingRT, RectTransform fadedRt) {
		yield return new WaitForEndOfFrame();

		scalingRT.localScale = Vector3.zero;
		scalingRT.anchorMin = new Vector2(fadedRt.anchorMin.x, fadedRt.anchorMin.y);
		scalingRT.anchorMax = new Vector2(fadedRt.anchorMax.x, fadedRt.anchorMax.y);
		scalingRT.anchoredPosition = new Vector2(fadedRt.anchoredPosition.x, fadedRt.anchoredPosition.y);
		scalingRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fadedRt.rect.width);
		scalingRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fadedRt.rect.height);

		scalingRT.gameObject.SetActive(true);
	}
	/* EVENT HANDLERS */
	private void OnMiniGameStepComplete(System.Type pType) {
		if(pType == typeof(MiniGameYoga)) {

		}
	}

	private void OnMiniGameComplete(bool pDidComplete) {
		Debug.LogFormat("{0} Mini Game {1}!", System.Enum.GetName(typeof(Constants.GameType), _currentGameType),
			(pDidComplete ? "Complete" : "Failed"));

		SpawnNewMiniGame();
	}
}
