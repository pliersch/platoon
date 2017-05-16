using level.battlefield;
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

		public GameObject GetGameObject {
			get { return _go; }
		}

		public void SetArmy(Army army) {
			_army = army;
		}

		public void HandleClick() {
			Debug.Log("pos " + Position.ToString());
			_army.HandleUnitSelected(this);
		}

	}

}