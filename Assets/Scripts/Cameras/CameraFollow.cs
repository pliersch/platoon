﻿using System;
using UnityEngine;

namespace cameras {

	public class CameraFollow : MonoBehaviour {

		[HideInInspector]
		public Transform _target; // The position that that camera will be following.

		public float _smoothing = 5f; // The speed with which the camera will be following.
//		private Vector3 _offset; // The initial offset from the target.
		private bool _isZooming;
		private readonly float _zoomFactor = 5f;
		private float _zoom;
		private Camera _camera;
		private Vector3 _resetCamera; // original camera position
		private Vector3 _origin; // place where mouse is first pressed
		private Vector3 _diference; // change in position of mouse relative to origin

		public void AddTarget(Transform target) {
			_target = target;
		}

		private void Start() {
			_resetCamera = Camera.main.transform.position;
			_camera = Camera.main;
			_isZooming = false;
			// Calculate the initial offset.
//			_offset = transform.position - _target.position;
		}

		private void Update() {
			var d = Input.GetAxis("Mouse ScrollWheel");
			if (Math.Abs(d - 0.1) > 0.05f) {
				_zoom = _zoomFactor * d;
				_isZooming = true;
			} else {
				_isZooming = false;
			}
		}

		private void FixedUpdate() {
//			// Create a postion the camera is aiming for based on the offset from the target.
//			Vector3 targetCamPos = _target.position + _offset;
//			// Smoothly interpolate between the camera's current position and it's target position.
//			transform.position = Vector3.Lerp(transform.position, targetCamPos, _smoothing * Time.deltaTime);
		}

		private void LateUpdate() {
			if (Input.GetMouseButtonDown(0)) {
				_origin = MousePos();
			}
			if (Input.GetMouseButton(0)) {
				_diference = MousePos() - transform.position;
				transform.position = _origin - _diference;
			}
			// reset camera to original position
			if (Input.GetMouseButton(1)) {
				transform.position = _resetCamera;
			}
			if (_isZooming) {
				_camera.orthographicSize += _zoom;
			}
		}

		// return the position of the mouse in world coordinates (helper method)
		private Vector3 MousePos() {
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

}