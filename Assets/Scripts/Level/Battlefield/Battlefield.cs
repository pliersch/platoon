using level.battlefield.util;
using level.gameObjects;
using UnityEngine;

namespace level.battlefield {

	public class Battlefield : MonoBehaviour, IBattlefieldViewController {

		public BattlefieldModel _model;
		public BattlefieldView _view;
		public PrefabFactory _factory;
		public int _rows;
		public int _columns;
		private Army _army;

		// TODO: move LevelChecker and all of generation/initializing to a factory. Don´t want see here
		private void Start() {
			float tileSize = new LevelChecker().CheckTileSize(_factory.tile);
			_model.GenerateFields(_rows, _columns, tileSize, transform.position);
			_view.SetController(this);
			AddUnits();
		}

		public void HandleUnitSelected(Unit unit) {
			Field[] reachableFields = _model.GetReachableFields(unit.Position);
			_view.ShowReachableFields(reachableFields);
		}


		private void AddUnits() {
			var respawns = GameObject.FindGameObjectsWithTag("Respawn");
			Position[] positions = new Position[respawns.Length];
			Unit[] units = new Unit[positions.Length];
			_army = new Army(this, units);
			for (int i = 0; i < respawns.Length; i++) {
				positions[i] = _model.ConvertCoordinateToPosition(respawns[i].transform.localPosition);
				var position = positions[i];
				Field field = _model.GetField(position);
				GameObject go = _view.AddUnit(_factory.tank, field.RealPosition);
				Unit unit = new Tank(go, position);
				units[i] = unit;
				unit.SetArmy(_army);
				_model.UpdateAddedUnit(unit, position);
			}
		}

		public void HandleTargetFieldSelected(Position position) {
			Field[] way = _model.GetWay(position);
			_model.UpdateFreeFields(way);
			_view.ShowReachableFields(way);
			_army.MoveActiveUnit(way);

		}

	}

}