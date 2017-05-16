using cameras;
using level.gameObjects;
using UnityEngine;

namespace Tanks {

	public class TankActionHandler : MonoBehaviour {

		private CameraFollow _cameraFollow;
		private Unit _handler;

		private void Start() {
			var camerRig = GameObject.FindWithTag("MainCamera");
			_cameraFollow = camerRig.GetComponent<CameraFollow>();
		}

		public void SetInteractionHandler(Unit handler) {
			_handler = handler;
		}

		private void OnMouseDown() {
			_cameraFollow.AddTarget(gameObject.transform);
			_handler.HandleClick();
		}

		private void OnMouseOver() {
			Debug.Log("mouse over " + gameObject.name);
		}

	}

}