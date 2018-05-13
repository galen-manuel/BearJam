using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameFactory {

	public MiniGameFactory() {

	}

	public MiniGame CreateMiniGame(Constants.GameType pType) {
		MiniGame m = null;
		switch(pType) {
			case Constants.GameType.Yoga:
				m = new MiniGameYoga(0, 10f);
				break;
		}

		return m;
	}
}
