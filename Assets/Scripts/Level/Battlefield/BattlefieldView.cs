using System;
using UnityEngine;

namespace level.battlefield {

	public class BattlefieldView : MonoBehaviour, ITileActionHandler {

		public GameObject _fieldPrefab;
		public GameObject _pivot;
		private IBattlefieldViewController _controller;

		public GameObject AddUnit(GameObject unit, Vector3 position) {
			return Instantiate(unit, position, unit.transform.rotation);
			//			GameObject go = Instantiate(unit, position, unit.transform.rotation);
		}

		internal void ShowReachableFields(Field[] reachableFields) {
			foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Field")) {
				Destroy(tile);
			}
			foreach (var field in reachableFields) {
				GameObject go =
					Instantiate(_fieldPrefab, field.RealPosition, _fieldPrefab.transform.rotation);
				Tile tile = (Tile) go.GetComponent(typeof(Tile));
				tile.SetActionHandler(this);
				tile.SetPosition(field.Position);
				tile.SetText(field.Position.x + " | "+ field.Position.z);
			}
		}

		public void HandleRechableFieldSelected(Position position) {
			_controller.HandleRechableFieldSelected(position);
		}

		public void SetController(IBattlefieldViewController controller) {
			_controller = controller;
		}

	}

}