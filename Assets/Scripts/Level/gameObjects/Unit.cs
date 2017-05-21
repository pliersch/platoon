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
		protected Army _army;
		public readonly int _actionPoints;

		protected Unit(GameObject go, Position position) : this(go) {
			Position = position;
		}

		protected Unit(GameObject go) {
			_go = go;
			_actionHandler = go.GetComponent<TankActionHandler>();
			_actionHandler.SetInteractionHandler(this);
		}

		public GameObject GetGameObject() {
			return _go;
		}

		public void SetArmy(Army army) {
			_army = army;
		}

		public void HandleClick() {
//			Debug.Log("pos " + Position.ToString());
			_army.HandleUnitSelected(this);
		}

		public void Move(Field[] way) {
			TankMovement movement = _go.GetComponent<TankMovement>();
			movement.enabled = true;
			movement.Move(way);
			Position = way[way.Length - 1].Position;
		}

	}

}