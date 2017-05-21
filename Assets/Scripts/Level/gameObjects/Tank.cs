using level.battlefield;
using UnityEngine;

namespace level.gameObjects {

	public class Tank : Unit {

		public Tank(GameObject go, Position position) : base(go, position) {
			ActionPoints = 5;
			_remainingActionPoints = ActionPoints;
		}

	}

}