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
			Transform pivot = transform.Find("GroundPlane/Pivot");
			_model.GenerateFields(_rows, _columns, tileSize, pivot.position);
			var position = new Position(3, 3);
			GameObject go = AddUnits(_factory.tank, position);
			Unit[] units = new Unit[1];
			Unit unit = new Tank(go, position);
			units[0] = unit;
			Army army = new Army(this, units);
			unit.SetArmy(army);

			_model.UpdateAddedUnit(unit, position);
		}

//		private void Update() {
//		}



		private GameObject AddUnits(GameObject go, Position position) {
			Field field = _model.GetField(position);
			return _view.AddUnit(go, field.RealPosition);
		}

		public void HandleUnitSelected(Unit unit) {
			Field[] reachableFields = _model.GetReachableFields(unit.Position);
			_view.ShowReachableFields(reachableFields);
		}

	}

}