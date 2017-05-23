using System.Linq;
using level.battlefield;
using level.battlefield.actions;
using Tanks;
using UnityEngine;

namespace level.gameObjects {

	public abstract class Unit {

		public Position Position { get; set; }
		protected GameObject _go;
		protected TankActionHandler _actionHandler;
		private readonly Army _army;
		public int ActionPoints { get; set; }
		protected int _remainingActionPoints;

		public Army Army {
			get { return _army; }
		}


		protected Unit(GameObject go, Position position, Army army) {
			Position = position;
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

		public void Move(Field[] way) {
			TankMovement movement = _go.GetComponent<TankMovement>();
			movement.enabled = true;
			movement.Move(way);
			Field targetField = way[way.Length - 1];
			Position = targetField.Position;
			_remainingActionPoints = targetField.RemainedActionPoint;
		}

		public void ResetActionPoints() {
			_remainingActionPoints = ActionPoints;
		}

	}

}