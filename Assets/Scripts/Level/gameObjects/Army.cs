using level.battlefield;

namespace level.gameObjects {

	public class Army {

		public Unit[] _units;
		private Unit _activeUnit;
		private readonly Battlefield _battlefield;

		public Army(Battlefield battlefield, Unit[] units) {
			_battlefield = battlefield;
			_units = units;
		}

		public Unit GetActiveUnit() {
			return _activeUnit;
		}

		public void HandleUnitSelected(Unit unit) {
			_activeUnit = unit;
			_battlefield.HandleUnitSelected(unit);
		}

		internal void MoveActiveUnit(Field[] way) {
			_activeUnit.Move(way);
		}

		public void ResetActionPoints() {
			foreach (Unit unit in _units) {
				unit.ResetActionPoints();
			}
		}

	}

}