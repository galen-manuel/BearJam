using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameFactory {

	public MiniGameFactory() {

	}

	public MiniGame CreateMiniGame(Constants.GameType pType) {
		MiniGame m = null;
		switch(pType) {
			case Constants.GameType.QuickTimeEvent:
				m = new MiniGameQTE(0);
				break;
		}

		return m;
	}
}
