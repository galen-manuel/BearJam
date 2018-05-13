using UnityEngine;
using UnityEngine.UI;

public static class ColorExtensions {
	/* ENUMS */


	/* CONSTANTS */


	/* PUBLIC VARS */

	public static void SetAlpha(this Image pImage, float pAlpha) {
		Color c = pImage.color;
		pImage.color = new Color(c.r, c.g, c.b, pAlpha);
	}

	/* PRIVATE VARS */


	/* INITIALIZATION */


	/* PROPERTIES */


	/* PUBLIC FUNCTIONS */


	/* PRIVATE FUNCTIONS */


	/* EVENT HANDLERS */

}