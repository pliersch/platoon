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
		private Army _myArmy;
		private Army _enemyArmy;
		private Army _activeArmy;

		// TODO: move LevelChecker and all of generation/initializing to a factory. Don´t want see here
		// TODO: better Use Unity Editor scripts and check it before compile (no code in final game)
		private void Start() {
			float tileSize = new LevelChecker().CheckTileSize(_factory.tile);
			_model.GenerateFields(_rows, _columns, tileSize, transform.position);
			_view.SetController(this);
			AddUnits();
		}

		public void HandleUnitSelected(Unit unit) {
		_view.DestroyReachableFields(); // 
			// TODO re-enable if KI exists
			//		if (unit.Army == _myArmy && _myArmy == _activeArmy) {
			if (unit.Army == _activeArmy) {
				Field[] reachableFields = _model.GetReachableFields(unit.Position, unit.GetRemainingActionPoints());
				_view.ShowReachableFields(reachableFields);
			}
		}

		public void HandleTargetFieldSelected(Position position) {
			Field[] way = _model.GetWay(position);
			_model.UpdateFreeFields(way);
			_view.DestroyReachableFields();
			// good for debug
			//_view.ShowReachableFields(way);
			// TODO re-enable if KI exists... OR NOT?
//			_myArmy.MoveActiveUnit(way);
			_activeArmy.MoveActiveUnit(way);
		}

		public void HandleNextTurn() {
			_activeArmy.ResetActionPoints();
//			DisableArmy(_activeArmy);
			EnableNextArmy();
		}

		private void EnableNextArmy() {
			_activeArmy = _myArmy == _activeArmy ? _enemyArmy : _myArmy;
		}

		/*------------------------------------------------------------------------------------------*/
		/*-------------------------------------- Initilize -----------------------------------------*/
		/*------------------------------------------------------------------------------------------*/

		private void AddUnits() {
			GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
			GameObject[] enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
			InitUnits(out _myArmy, spawns);
			InitUnits(out _enemyArmy, enemySpawns);
			_activeArmy = _myArmy;
		}


		private void InitUnits(out Army army, GameObject[] spawns) {
			int count = spawns.Length;
			Unit[] units = new Unit[count];
			army = new Army(this, units);
			for (int i = 0; i < count; i++) {
				var position = _model.ConvertCoordinateToPosition(spawns[i].transform.localPosition);
				Field field = _model.GetField(position);
				GameObject go = _view.AddUnit(_factory.tank, field.RealPosition);
				Unit unit = new Tank(go, position, army);
				units[i] = unit;
				_model.UpdateAddedUnit(unit, position);
			}
		}

	}

}