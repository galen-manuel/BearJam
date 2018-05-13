using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class BearManager : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	private Image _bearImage;
	private RectTransform _rt;

	/* INITIALIZATION */

	private void Start() {
		Messenger.AddListener(Messages.BEGING_START_GAME_SEQUENCE, OnBeginStartGameSequence);
		Messenger.AddListener<WorldManager.TimeOfDay>(Messages.CHANGE_TIME_OF_DAY, OnGoToTimeOfDay);

		_rt = transform as RectTransform;
		_bearImage = GetComponentInChildren<Image>();
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	private void OnBeginStartGameSequence() {
		float tweenTime = 0.0f;
		Sequence entrance = DOTween.Sequence();

		entrance.Insert(0.0f, _rt.DOAnchorPos(new Vector2(500, 800), 1.0f).SetEase(Ease.Linear));
		tweenTime += 1.0f;
		entrance.Insert(0.0f, _bearImage.rectTransform.DOJumpAnchorPos(new Vector2(0, 0), 20, 1, 0.25f).SetLoops(8, LoopType.Yoyo).SetEase(Ease.Linear));
		entrance.Insert(1.0f, _rt.DOAnchorPos(new Vector2(0, 975), 1.0f).SetEase(Ease.Linear));
		tweenTime += 1.0f;

		entrance.InsertCallback(tweenTime + 0.1f, () => {
			Messenger.Broadcast(Messages.START_GAME);
		});
	}

	private void OnGoToTimeOfDay(WorldManager.TimeOfDay pTimeOfDay) {
		_rt.DOJumpAnchorPos(_rt.anchoredPosition, 20, 50, Constants.TIME_OF_DAY_TRANSITION_TIME);
		_rt.rotation = Quaternion.Euler(new Vector3(0, 0, 5));
		_rt.DORotate(new Vector3(0, 0, -5), 0.05f).SetLoops(40, LoopType.Yoyo).OnComplete(() => _rt.rotation = Quaternion.identity);
	}
}
