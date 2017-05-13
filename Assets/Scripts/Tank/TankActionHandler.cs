using Cameras;
using UnityEngine;

namespace Tank {

	public class TankActionHandler : MonoBehaviour {

		private CameraFollow _cameraFollow;

		// Use this for initialization
		private void Start() {
			var camerRig = GameObject.FindWithTag("MainCamera");
			_cameraFollow = camerRig.GetComponent<CameraFollow>();
		}

		private void OnMouseDown() {
			_cameraFollow.AddTarget(gameObject.transform);
			//Destroy(gameObject);
		}

		private void OnMouseOver() {
			Debug.Log("mouse over " + gameObject.name);
		}

	}

}