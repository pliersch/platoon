using System.Collections.Generic;
using level.battlefield.util;
using level.gameObjects;
using UnityEngine;

namespace level.battlefield {

	public class Battlefield : MonoBehaviour, IBattlefieldViewController {

		public BattlefieldModel _model;
		public BattlefieldView _view;
		public PrefabFactory _factory;
		public LevelChecker _levelChecker;
		public int _rows;
		public int _columns;
		private Army _myArmy;
		private Army _enemyArmy;
		private Army _offenerArmy;
		private Army _defenderArmy;
		private List<Unit> _possibleTargets;

		// TODO: move LevelChecker and all of generation/initializing to a factory. Don´t want see here
		// TODO: better Use Unity Editor scripts and check it before compile (no code in final game)
		private void Start() {
			float tileSize = _levelChecker.CheckTileSize(_factory.tile);

			Field[,] fields = _model.GenerateFields(_rows, _columns, tileSize, transform.position);
			_levelChecker.FindOccupiedFields(fields, _factory);
			_view.SetController(this);
			AddUnits();
		}

		public void HandleUnitSelected(Unit unit) {
			// TODO re-enable if KI exists
			//		if (unit.Army == _myArmy && _myArmy == _offenerArmy) {
			if (unit.Army == _offenerArmy) {
				ShowReachableFields(unit);
				_possibleTargets = FindPossibleTargets(unit);
				//	_defenderArmy.UnHighlightUnits(_possibleTargets);
				//	_defenderArmy.HighlightUnits(_possibleTargets);
			} else if (_offenerArmy.GetActiveUnit() != null && _possibleTargets.Contains(unit)) {
				Attack(unit);
			}
		}

		public void HandleUnitMovementComplete(Unit unit) {
			_view.ShowReachableFields(_model.GetReachableFields(unit.Position, unit.GetRemainingActionPoints()));
			_possibleTargets = FindPossibleTargets(unit);
		}

		private void Attack(Unit defender) {
			//_offenerArmy.Attack(defender);
			Unit offener = _offenerArmy.GetActiveUnit();
			offener.Fire(defender.RealPosition);
			ShowReachableFields(offener);
		}

		private void ShowReachableFields(Unit unit) {
			_view.DestroyReachableFields();
			_view.ShowReachableFields(_model.GetReachableFields(unit.Position, unit.GetRemainingActionPoints()));
		}

		private List<Unit> FindPossibleTargets(Unit unit) {
			return new Raycaster().FindPossibleTargets(unit, _enemyArmy.GetUnits());
		}

		public void HandleTargetFieldSelected(Position position) {
			Field[] way = _model.GetWay(position);
			_model.UpdateFreeFields(way);
			_view.DestroyReachableFields();
			// good for debug
			//_view.ShowReachableFields(way);
			// TODO re-enable if KI exists... OR NOT?
//			_myArmy.MoveActiveUnit(way);
			_offenerArmy.MoveActiveUnit(way);
		}

		public void HandleNextTurn() {
			_offenerArmy.ResetActionPoints();
//			DisableArmy(_offenerArmy);
			EnableNextArmy();
		}

		private void EnableNextArmy() {
			if (_offenerArmy == _myArmy) {
				_offenerArmy = _enemyArmy;
				_defenderArmy = _myArmy;
			} else {
				_offenerArmy = _myArmy;
				_defenderArmy = _enemyArmy;
			}
		}


		/*------------------------------------------------------------------------------------------*/
		/*-------------------------------------- Initilize -----------------------------------------*/
		/*------------------------------------------------------------------------------------------*/

		private void AddUnits() {
			GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
			GameObject[] enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
			InitUnits(out _myArmy, spawns);
			InitUnits(out _enemyArmy, enemySpawns);
			_offenerArmy = _myArmy;
			_defenderArmy = _enemyArmy;
		}


		private void InitUnits(out Army army, GameObject[] spawns) {
			int count = spawns.Length;
			Unit[] units = new Unit[count];
			army = new Army(this, units);
			for (int i = 0; i < count; i++) {
				Vector3 localPosition = spawns[i].transform.localPosition;
				var position = _model.ConvertCoordinateToPosition(localPosition);
				Field field = _model.GetField(position);
				GameObject go = _view.AddUnit(_factory.tank, field.RealPosition);
				Unit unit = new Tank(go, army, position, localPosition);
				units[i] = unit;
				_model.UpdateAddedUnit(unit, position);
			}
		}

	}

}