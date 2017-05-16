using level.battlefield.Util;
using level.gameObjects;
using UnityEngine;

namespace level.battlefield {

	public class Battlefield : MonoBehaviour {

		public BattlefieldModel _model;
		public BattlefieldView _view;
		public PrefabFactory _factory;
		public int _rows;
		public int _columns;

		private void Start() {
			float tileSize = new LevelChecker().CheckTileSize(_factory.tile);
			_model.GenerateFields(_rows, _columns, tileSize, transform.position);
			Position[] positions = new[] {
				new Position(3, 3),
				new Position(3, 4),
				new Position(4, 3)
			};
			AddUnits(positions);

		}

//		private void Update() {
//		}



		private void AddUnits(Position[] positions) {
			for (int i = 0; i < positions.Length; i++) {
				var position = positions[i];
				Field field = _model.GetField(position);
				GameObject go = _view.AddUnit(_factory.tank, field.RealPosition);
				Unit[] units = new Unit[positions.Length];
				Unit unit = new Tank(go, position);
				units[i] = unit;
				Army army = new Army(this, units);
				unit.SetArmy(army);
				_model.UpdateAddedUnit(unit, position);
			}
		}

		public void HandleUnitSelected(Unit unit) {
			Field[] reachableFields = _model.GetReachableFields(unit.Position);
			_view.ShowReachableFields(reachableFields);
		}

	}

}