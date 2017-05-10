using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankClickHandler : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Debug.Log("start " + gameObject.name);
	}

	// Update is called once per frame
	void Update() {

	}

	void OnMouseDown() {
		Debug.Log("mouse down " + gameObject.name);
		//Destroy(gameObject);
		// TODO usefull later
		if (Input.GetMouseButtonDown(0)) {
			// Whatever you want it to do.
		}
	}


	void OnMouseOver() {
		Debug.Log("mouse over " + gameObject.name);
	}

}
