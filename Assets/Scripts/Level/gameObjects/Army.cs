using System;
using level.battlefield;
using UnityEngine;

namespace level.gameObjects {

	public class Army {

		public Unit[] _units;
		private Unit _activeUnit;
		private readonly Battlefield _battlefield;

		public Army(Battlefield battlefield, Unit[] units) {
			_battlefield = battlefield;
			_units = units;
		}

		public void HandleUnitSelected(Unit unit) {
			if (unit == _activeUnit) {
				return;
			}
			_activeUnit = unit;
			_battlefield.HandleUnitSelected(unit);
		}

	}

}