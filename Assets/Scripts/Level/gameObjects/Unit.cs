﻿using level.battlefield;
using Tanks;
using UnityEngine;

namespace level.gameObjects {

	public abstract class Unit {

		public Position Position { get; set; }
		public Vector3 RealPosition { get; set; }
		protected GameObject _go;
		protected TankActionHandler _actionHandler;
		private readonly Army _army;
		public int ActionPoints { get; set; }
		protected int _remainingActionPoints;

		public Army Army {
			get { return _army; }
		}


		protected Unit(GameObject go, Army army, Position position, Vector3 realPosition) {
			Position = position;
			RealPosition = realPosition;
			_go = go;
			_army = army;
			_actionHandler = go.GetComponent<TankActionHandler>();
			_actionHandler.SetInteractionHandler(this);
		}

		public GameObject GetGameObject() {
			return _go;
		}

		public int GetRemainingActionPoints() {
			return _remainingActionPoints;
		}

		public void HandleClick() {
			_army.HandleUnitSelected(this);
		}

		public void HandleMovementComplete() {
			_army.HandleUnitMovementComplete(this);
		}

		public void Move(Field[] way) {
			TankMovement movement = _go.GetComponent<TankMovement>();
			movement.enabled = true;
			movement.Move(way);
			Field targetField = way[way.Length - 1];
			Position = targetField.Position;
			RealPosition = targetField.RealPosition;
			_remainingActionPoints = targetField.RemainedActionPoint;
		}

		public void ResetActionPoints() {
			_remainingActionPoints = ActionPoints;
		}

		public void Fire(Vector3 target) {
			if (_remainingActionPoints == 0) {
				return;
			}
			_remainingActionPoints--;
			_go.transform.LookAt(target);
			//			TankShooting shooting = _go.GetComponent<TankShooting>();
			LineShooting shooting = _go.GetComponentInChildren<LineShooting>();
			Vector3 offset = target - _go.transform.position;
			float distance = Mathf.Sqrt(offset.x * offset.x + offset.z * offset.z);
			shooting.Shoot(distance);
		}

	}

}