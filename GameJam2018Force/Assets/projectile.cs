using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

	public float projectileSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * projectileSpeed;

	}

	void OnTriggerEnter2D(Collider2D target) {


		// Destroy Debre ship Collided With
		if ((target.gameObject.layer == 8) || (target.gameObject.layer == 10) || (target.gameObject.layer == 11)) { // Attached or Thrown Debre

			GameObject.Destroy(target.gameObject);
			// Decrease HP instead******

		}


		// Play Explosion on Destruction of Enemy Ship
		AudioManager.audioManager.playExplosion();
		GameObject.Destroy(gameObject);

	}

}
