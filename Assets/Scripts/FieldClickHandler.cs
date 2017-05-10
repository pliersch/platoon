using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldClickHandler : MonoBehaviour
{

	public Color colorOver;
	public Color colorOut;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void OnMouseOver() {
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
		renderer.material.color = colorOver;
	}

	void OnMouseDown() {
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
		Debug.Log("mouse down");
	}

	void OnMouseExit() {
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
		renderer.material.color = colorOut;
	}


}
