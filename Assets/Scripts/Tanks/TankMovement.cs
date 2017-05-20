using System;
using System.Collections.Generic;
using level.battlefield;
using UnityEngine;

namespace Tanks {

	public class TankMovement : MonoBehaviour {

		public float _turnSpeed = 180f; // How fast the tank turns in degrees per second.

		// Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
		public AudioSource _movementAudio;

		public AudioClip _engineDriving; // Audio to play when the tank is moving.
		public float _pitchRange = 0.1f; // The amount by which the pitch of the engine noises can vary.

		private Rigidbody _rigidbody; // Reference used to move the tank.
		private float _originalPitch; // The pitch of the audio source at the start of the scene.

		private int _currentFieldIndex;
		private Vector3 _checkPoint;
		private Field[] _way;
		private float _speed;
		private Vector3 _movement;
		private bool _isMoving;

		private void Awake() {
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void OnEnable() {
			_isMoving = false;
			_rigidbody.isKinematic = false;

			_speed = 0;
			_movement = Vector3.zero;
		}

		private void OnDisable() {
			_rigidbody.isKinematic = true;
		}

		private void Start() {
			_originalPitch = _movementAudio.pitch;
		}

		private void Update() {
			if (!_isMoving) {
				return;
			}
			EngineAudio();
			_rigidbody.MovePosition(_rigidbody.position + _movement);
			if (IsCheckPointReached()) {
				if (IsTargetFieldReached()) {
					StopMoving();
				} else {
					SetNextCheckPoint();
					Turn2NextCheckPoint();
				}
			}
		}

		private bool IsTargetFieldReached() {
			return _currentFieldIndex == _way.Length - 1;
		}

		private void StopMoving() {
			_isMoving = false;
			_speed = 0;
			_movement = Vector3.zero;
			_movementAudio.Stop();
		}

		private void SetNextCheckPoint() {
			_checkPoint = _way[++_currentFieldIndex].RealPosition;
		}

		private void Turn2NextCheckPoint() {
			Position nextPosition = _way[_currentFieldIndex].Position;
			Position lastPosition = _way[_currentFieldIndex - 1].Position;
			if (nextPosition.x > lastPosition.x) {
				_movement = new Vector3(_speed, 0, 0);
			} else if (nextPosition.x < lastPosition.x) {
				_movement = new Vector3(-_speed, 0, 0);
			} else if (nextPosition.z > lastPosition.z) {
				_movement = new Vector3(0, 0, _speed);
			} else if (nextPosition.z < lastPosition.z) {
				_movement = new Vector3(0, 0, -_speed);
			}
		}

		private bool IsCheckPointReached() {
			return Math.Abs(_rigidbody.position.x - _checkPoint.x) < 0.01f &&
			       Math.Abs(_rigidbody.position.z - _checkPoint.z) < 0.01f;
		}

		private void EngineAudio() {
			_movementAudio.pitch = UnityEngine.Random.Range(_originalPitch - _pitchRange, _originalPitch + _pitchRange);
			_movementAudio.Play();
		}

		public void Go(Field[] way) {
			_isMoving = true;
			_way = way;
			_speed = 0.5f;
			_currentFieldIndex = 0;
			SetNextCheckPoint();
			Turn2NextCheckPoint();

//			int diffX = way[1].Position.x - way[0].Position.x;
//			if (diffX != 0) {
//				_turnInputValue = diffX * 0.35f;
//				//Turn();
//			}
			//_checkPoint = way[_currentFieldIndex].RealPosition;
		}

		private void Turn() {
//			// Determine the number of degrees to be turned based on the input, speed and time between frames.
//			float turn = _turnInputValue * _turnSpeed * Time.deltaTime;
//
//			// Make this into a rotation in the y axis.
//			Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
//
//			// Apply this rotation to the rigidbody's rotation.
//			_rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
		}

	}

}