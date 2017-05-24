using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks {

	public class LineShooting : MonoBehaviour {

		public int damagePerShot = 20; // The damage inflicted by each bullet.
		public float timeBetweenBullets = 0.15f; // The time between each shot.
		public float range = 100f; // The distance the gun can fire.

		float timer; // A timer to determine when to fire.
		Ray shootRay = new Ray(); // A ray from the gun end forwards.
		RaycastHit shootHit; // A raycast hit to get information about what was hit.
		int shootableMask; // A layer mask so the raycast only hits things on the shootable layer.
		ParticleSystem gunParticles; // Reference to the particle system.
		LineRenderer gunLine; // Reference to the line renderer.
		AudioSource gunAudio; // Reference to the audio source.

		Light gunLight; // Reference to the light component.

		//public Light faceLight; // do we need this light?
		float effectsDisplayTime = 0.2f; // The proportion of the timeBetweenBullets that the effects will display for.


		void Awake() {
			// Create a layer mask for the Shootable layer.
			shootableMask = LayerMask.GetMask("Shootable");

			// Set up the references.
			gunParticles = GetComponent<ParticleSystem>();
			gunLine = GetComponent<LineRenderer>();
			gunAudio = GetComponent<AudioSource>();
			gunLight = GetComponent<Light>();
			//faceLight = GetComponentInChildren<Light> ();
		}


		void Update() {
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
			if (timer >= timeBetweenBullets * effectsDisplayTime) {
				// ... disable the effects.
				DisableEffects();
			}
		}


		public void DisableEffects() {
			// Disable the line renderer and the light.
			gunLine.enabled = false;
			//faceLight.enabled = false; // do we need this light?
			gunLight.enabled = false;
		}


		public void Shoot(Vector3 target) {
			// Reset the timer.
			timer = 0f;

			// Play the gun shot audioclip.
			gunAudio.Play();

			// Enable the lights.
			gunLight.enabled = true;
//			faceLight.enabled = true; // do we need this light?

			// Stop the particles from playing if they were, then start the particles.
			gunParticles.Stop();
			gunParticles.Play();

			// Enable the line renderer and set it's first position to be the end of the gun.
			gunLine.enabled = true;
			//gunLine.SetPosition(0, transform.localPosition);

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			gunLine.SetPosition(1, target);
		}

	}

}