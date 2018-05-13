using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameFactory {

	public MiniGameFactory() {

	}

	public MiniGame CreateMiniGame(Constants.GameType pType) {
		MiniGame m = null;
		switch(pType) {
			case Constants.GameType.RandomSequence:
				m = new MiniGameRandomSequence(0);
				break;
		}

		return m;
	}
}
