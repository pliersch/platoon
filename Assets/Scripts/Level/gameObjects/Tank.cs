using level.battlefield;
using UnityEngine;

namespace level.gameObjects {

	public class Tank : Unit {

		public Tank(GameObject go, Position position, Army army) : base(go, position, army) {
			ActionPoints = 5;
			_remainingActionPoints = ActionPoints;
		}

	}

}