using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class World : MonoBehaviour {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */


	/* PRIVATE VARS */

	[SerializeField] [Tooltip("Length of the day in seconds")] private float _dayLength;

	[Header("Linkages")]
	[SerializeField] private RectTransform _skyLayerRT;
	[SerializeField] private RectTransform _cloudLayerRT;
	[SerializeField] private RectTransform _landLayerRT;
	[SerializeField] private RectTransform _wetSandLayerRT;
	[SerializeField] private Image _wetSandImage;
	[SerializeField] private RectTransform _waterLayerRT;

	private Sequence _waterSequence;

	/* INITIALIZATION */

	private void Start() {
		//_skyLayerRT.DORotate(new Vector3(0, 0, 360), _dayLength, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
		_cloudLayerRT.DORotate(new Vector3(0, 0, 360), _dayLength, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

		_waterSequence = DOTween.Sequence();
		// Scale up the water FAST - SLOW easing
		_waterSequence.Insert(0.0f, _waterLayerRT.DOScale(1.05f, 1.0f).SetEase(Ease.OutCirc));
		// At end set alpha of wet sand to full
		_waterSequence.InsertCallback(1.0f, () => _wetSandImage.SetAlpha(1.0f));
		// Scale down the water SLOW - FAST
		_waterSequence.Insert(1.0f, _waterLayerRT.DOScale(0.95f, 1.0f).SetEase(Ease.InCirc));
		// Fade out wet sand
		_waterSequence.Insert(1.0f, _wetSandImage.DOFade(0.0f, 2.0f).SetEase(Ease.Linear));
		_waterSequence.AppendInterval(1.0f);

		_waterSequence.SetLoops(-1);
	}

	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

}