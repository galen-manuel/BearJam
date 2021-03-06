﻿using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {
	/* ENUMS */

	public enum TimeOfDay {
		Day = 0,
		Dusk = 90,
		Night = 180,
		Dawn = 270,
	}

	/* CONSTANTS */


	/* PUBLIC VARS */

	public bool completelyHideWater;

	/* PRIVATE VARS */

	[SerializeField] [Tooltip("Length of the day in seconds")] private float _dayLength;

	[Header("Linkages")]
	[SerializeField] private RectTransform _skyLayerRT;
	[SerializeField] private RectTransform _celestialBodiesLayerRT;
	[SerializeField] private RectTransform _cloudLayerRT;
	[SerializeField] private RectTransform _buildingLayerRT;
	[SerializeField] private RectTransform _treeLayerRT;
	[SerializeField] private RectTransform _landLayerRT;
	[SerializeField] private RectTransform _wetSandLayerRT;
	[SerializeField] private Image _wetSandImage;
	[SerializeField] private RectTransform _waterLayerRT;

	private Sequence _waterSequence;
	private Tween _cloudTween;
	private Tween _treeBounceTween;
	private Tween _buildingBounceTween;
	private TimeOfDay _currentTimeOfDay;

	/* INITIALIZATION */

	private void Start() {
		Messenger.AddListener(Messages.START_GAME, OnStartGame);
		Messenger.AddListener<TimeOfDay>(Messages.PREPARE_CHANGE_TIME_OF_DAY, OnPrepareChangeTimeOfDay);

		_waterLayerRT.localScale = completelyHideWater ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(0.93f, 0.93f, 0.93f);
		_waterLayerRT.DORotate(new Vector3(0, 0, -360), _dayLength * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

		_cloudTween = _cloudLayerRT.DORotate(new Vector3(0, 0, 360), _dayLength, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
		_buildingBounceTween = _buildingLayerRT.DOScale(1.01f, 0.5f).SetEase(Ease.InCirc).SetLoops(-1, LoopType.Yoyo);
		_buildingBounceTween.Pause();
		_treeBounceTween = _treeLayerRT.DOScale(1.02f, 0.25f).SetEase(Ease.InCirc).SetLoops(-1, LoopType.Yoyo);
		_treeBounceTween.Pause();

		_waterSequence = DOTween.Sequence();
		_waterSequence.InsertCallback(0.0f, () => _wetSandImage.SetAlpha(0.0f));
		// Scale up the water FAST - SLOW easing
		_waterSequence.Insert(1.0f, _waterLayerRT.DOScale(1.05f, 2.0f).SetEase(Ease.OutCirc)); // ends at 2.0f
		// At end set alpha of wet sand to full
		_waterSequence.InsertCallback(3.0f, () => {
			_wetSandLayerRT.rotation = _waterLayerRT.rotation;
			_wetSandImage.SetAlpha(1.0f);
		});
		// Scale down the water SLOW - FAST
		_waterSequence.Insert(3.0f, _waterLayerRT.DOScale(completelyHideWater ? 0.80f : 0.93f, 1.5f).SetEase(Ease.InOutCirc)); // ends at 3.0f
		// Fade out wet sand
		_waterSequence.Insert(3.0f, _wetSandImage.DOFade(0.0f, 2.0f).SetEase(Ease.Linear)); // ends at 4.0f

		_waterSequence.SetLoops(-1);
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */

	public void DEBBUG_GoToTimeOfDay(int pTime) {
		OnPrepareChangeTimeOfDay((TimeOfDay)pTime);
	}

	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

	private void OnStartGame() {
		_treeBounceTween.Play();
		_buildingBounceTween.Play();
	}

	private void OnPrepareChangeTimeOfDay(TimeOfDay pTimeOfDay) {
		if(_currentTimeOfDay != pTimeOfDay) {
			float time = Constants.TIME_OF_DAY_TRANSITION_TIME;
			float offset = (int)_currentTimeOfDay - (int)pTimeOfDay;
			_currentTimeOfDay = pTimeOfDay;
			// start spinning sequence
			_cloudTween.timeScale = 5;
			_waterSequence.timeScale = 10;
			_skyLayerRT.DORotate(new Vector3(0, 0, (360 * 5) + offset), time, RotateMode.LocalAxisAdd);
			_celestialBodiesLayerRT.DORotate(new Vector3(0, 0, (360 * 5) + offset), 2.0f, RotateMode.LocalAxisAdd);
			_buildingLayerRT.DORotate(new Vector3(0, 0, (360 * 5) + offset), time, RotateMode.LocalAxisAdd);
			_treeLayerRT.DORotate(new Vector3(0, 0, (360 * 5) + offset), time, RotateMode.LocalAxisAdd);
			_landLayerRT.DORotate(new Vector3(0, 0, (360 * 5) + offset), time, RotateMode.LocalAxisAdd).OnComplete(OnTimeOfDaySettled);

			Messenger.Broadcast(Messages.CHANGE_TIME_OF_DAY, pTimeOfDay);
		}
	}

	private void OnTimeOfDaySettled() {
		_cloudTween.timeScale = 1;
		_waterSequence.timeScale = 1;
		// Send start mini game?
	}
}