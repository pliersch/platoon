using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankClickHandler : MonoBehaviour {

	void OnMouseDown() {
		Destroy(gameObject);
		// TODO usefull later
		if (Input.GetMouseButtonDown(0)) {
			// Whatever you want it to do.
		}
	}


	void OnMouseOver() {
		Debug.Log(gameObject.name);
	}

}
