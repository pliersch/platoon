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

		// TODO: move LevelChecker and all of generation/initializing to a factory. Don´t want see here
		private void Start() {
			float tileSize = new LevelChecker().CheckTileSize(_factory.tile);
			_model.GenerateFields(_rows, _columns, tileSize, transform.position);
			AddUnits();
		}

		//		private void Update() {
		//		}

		public void HandleUnitSelected(Unit unit) {
			Field[] reachableFields = _model.GetReachableFields(unit.Position);
			_view.ShowReachableFields(reachableFields);
		}


		private void AddUnits() {
			var respawns = GameObject.FindGameObjectsWithTag("Respawn");
			Position[] positions = new Position[respawns.Length];
			for (int i = 0; i < respawns.Length; i++) {
				positions[i] = _model.ConvertCoordinateToPosition(respawns[i].transform.localPosition);
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

		public void OnCamera() {
			Debug.Log(" foo");
		}

	}

}