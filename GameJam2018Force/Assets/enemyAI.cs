﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public int enemyShipHP = 10;

	float distanceToAggroPlayer = 35;
	float distanceToShootPlayer = 7;
	float maxWanderingVelocity = 1.5f;
	float wanderingAIRotationStrength = 0.3f;
	float turningSpeed = 3.0f; // Aggroed Turning Speed
	float distanceToTurnAwayFromPlayer = 3.5f;
	float distanceToOrbitPlayer = 4.5f;
	public bool orbitingDirection = false;

	float maxAggroedVelocity = 2.5f; // 1.5f

	public string shipType = "Fighter";
	public bool aggroed = false;

	float distanceToPlayer = 1000;

	int wanderingAIState = 0;
	int wanderingAIStateCounter = 100;
	int wanderingAIStateCounterLength = 50;

	int aggroedAIState = 0;
	int aggroedAIStateCounter = 100;
	int aggroedAIStateCounterLength = 50;

	float xRandomMoveTo = 0;
	float yRandomMoveTo = 0;
	float xRandomMoveTo2 = 0;
	float yRandomMoveTo2 = 0;

	int firingCooldownCounter = 0;
	int firingCooldownCounterLength = 50;

	Vector3 playerPosition;
	Vector3 dir;

	public GameObject enemyProjectile;
	public GameObject bigEnemyProjectile;

	public int ramDamage = 10;

	// Use this for initialization
	void Start () {

		if (shipType == "Dreadnaught") {
			enemyShipHP = 30;
		}
		
	}

	// Update is called once per frame
	void Update () {

		// Enemy Ship Disengaged Wandering AI
		if (aggroed == false) {
			updateWanderingAI();
			updateCheckForAggro();
		}
			
		// Enemy Ship Aggroed Player AI
		if (aggroed) {

			// Enemy Ship Aggroed Movement AI
			if (shipType == "Fighter") {
				updateFighterAggroAI ();
			} else if (shipType == "Orbiter") {
				distanceToPlayer = 12;
				updateOrbiterAggroAI ();
			} else if (shipType == "Dreadnaught") {
				maxAggroedVelocity = 0.5f;
				//distanceToAggroPlayer = 15;
				//distanceToShootPlayer = 10;
				distanceToShootPlayer = 10;
				distanceToTurnAwayFromPlayer = 5.5f;
				turningSpeed = 0.8f;
				updateFighterAggroAI ();
			} else if (shipType == "AlienK") {
				turningSpeed = 7f;
				maxAggroedVelocity = 3.0f;
				updateAlienKAggroAI();
			}

		}

		// Destroy Enemy Ship
		if (enemyShipHP <= 0) {
			Object explosionEffect = Resources.Load ("Explosion");
			Object.Instantiate (explosionEffect, transform.position, transform.rotation);
			AudioManager.audioManager.playExplosion();
			GameObject.Destroy(gameObject);
		}
			
	}


	void updateWanderingAI() {

		wanderingAIStateCounter++;
		if (wanderingAIStateCounter > wanderingAIStateCounterLength) {
			wanderingAIState = Random.Range (0, 2);
			wanderingAIStateCounterLength = Random.Range (10, 20);
			wanderingAIStateCounter = 0;
		}

		// Enemy Ship Moves Forward
		gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * 0.4f);
		if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxWanderingVelocity) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxWanderingVelocity;
		}

		// Enemy Ship AI Steering Behavior
		if (wanderingAIState == 1) {
			gameObject.GetComponent<Rigidbody2D> ().rotation += wanderingAIRotationStrength;
		} else if (wanderingAIState == 2) {
			gameObject.GetComponent<Rigidbody2D> ().rotation -= wanderingAIRotationStrength;
		}

	}

	void updateCheckForAggro() {

		distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);

		if (distanceToPlayer < distanceToAggroPlayer) {
			aggroed = true;
		}

	}

	void updateAlienKAggroAI() {

		playerPosition = GameManagerScript.gameManager.player.transform.position;
		dir = playerPosition - transform.position;


		dir = playerPosition - transform.position;

		// Move toward the Player
		gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
		if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
		}

		// Face the Player
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);



	}

	void updateFighterAggroAI() {

		playerPosition = GameManagerScript.gameManager.player.transform.position;
		dir = playerPosition - transform.position;

		aggroedAIStateCounter++;
		if (aggroedAIStateCounter > aggroedAIStateCounterLength) {
			
			aggroedAIStateCounter = 0;

			// Firing Random Rate
			firingCooldownCounterLength = Random.Range(45,65);
			if (shipType == "Dreadnaught") {
				maxAggroedVelocity = 1.0f;
				firingCooldownCounterLength = Random.Range (25, 45);
				distanceToTurnAwayFromPlayer = Random.Range (1.0f, 3.0f);
				aggroedAIStateCounterLength = Random.Range (200, 250);

				aggroedAIState = 100;
			} else if (shipType == "Fighter") {
				aggroedAIState = Random.Range(0, 5);
				aggroedAIStateCounterLength = Random.Range(70, 150);
			}

			xRandomMoveTo = Random.Range(-10,10);
			yRandomMoveTo = Random.Range(-10,10);

			// State 4 Target Direction
			if (Random.Range (0, 1) == 0) {
				xRandomMoveTo2 = Random.Range (5, 15);
			} else {
				xRandomMoveTo2 = Random.Range (-15, -5);
			}

			if (Random.Range (0, 1) == 0) {
				yRandomMoveTo2 = Random.Range (5, 15);
			} else {
				yRandomMoveTo2 = Random.Range (-15, -5);
			}

		}
			

		// Dreadnaught Steer Directly To the Player
		if (aggroedAIState == 100) {

			dir = playerPosition - transform.position;

			// Move toward the Player
			gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

			// Face the Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);

			checkToShootProjectileDreadnaught();

		}

		// Steer Directly To the Player
		if (aggroedAIState <= 1) {

			dir = playerPosition - transform.position;

			// Move toward the Player
			gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

			// Face the Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);

			// If Enemy Ship too Close to Player Turn Away
			distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);
			if (distanceToPlayer < distanceToTurnAwayFromPlayer) {
				aggroedAIStateCounter = 0;
				aggroedAIState = Random.Range (2, 2);

				AudioManager.audioManager.playFighterShot();
				GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);

				firingCooldownCounter = 0;

			}

			checkToShootProjectile();
				
		}

		// Steer Away from the Player
		if (aggroedAIState == 2) {

			Vector3 dir = playerPosition - transform.position;

			// Move away from the Player
			gameObject.GetComponent<Rigidbody2D> ().AddForce (-dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

			// Face the Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle - 90, Vector3.forward), turningSpeed * Time.deltaTime);

		}


		// Steer Directly Offset To the Player
		if (aggroedAIState == 3) {

			// Move toward the Player
			playerPosition.x += xRandomMoveTo;
			playerPosition.y += yRandomMoveTo;
			dir = playerPosition - transform.position;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

			// Face the Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);

		}

			
		// Steer at an offset away from Player
		if (aggroedAIState == 4) {

			// Move Away from Player
			playerPosition.x += xRandomMoveTo2;
			playerPosition.y += yRandomMoveTo2;
			dir = playerPosition - transform.position;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

			// Face the Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);

		}

		if (aggroedAIState == 5) {

			// Face sidewide to Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle + 90, Vector3.forward), turningSpeed * Time.deltaTime);

			dir = playerPosition - transform.position;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > maxAggroedVelocity) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * maxAggroedVelocity;
			}

		}
	
	}

	void updateOrbiterAggroAI() {

		distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);

		aggroedAIStateCounter++;
		if (aggroedAIStateCounter > aggroedAIStateCounterLength) {
			aggroedAIState = 2;// Random.Range (1, 3);
			aggroedAIStateCounterLength = Random.Range (100, 250);
			aggroedAIStateCounter = 0;

			// Firing Random Rate
			firingCooldownCounterLength = Random.Range(55,125);

			// Within Orbit Range - Orbit Player
			if (distanceToPlayer < distanceToOrbitPlayer) {
				aggroedAIState = 1;
			}
				
		}

		// If Enemy Ship too Close to Player Turn Away
		if (distanceToPlayer < distanceToTurnAwayFromPlayer) {
			aggroedAIStateCounter = 0;
			aggroedAIState = 3;
		}

		// Orbit Player
		if (aggroedAIState == 1) {
			playerPosition = GameManagerScript.gameManager.player.transform.position;
			dir = playerPosition - transform.position;

			// Face sidewide to Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			if (orbitingDirection) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle + 0), Vector3.forward), turningSpeed * Time.deltaTime);
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle + 180), Vector3.forward), turningSpeed * Time.deltaTime);
			}

			dir = playerPosition - transform.position;

			// Orbiting Velocity
			gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * 2;



			// Fire Missle
			firingCooldownCounter++;
			if (firingCooldownCounter > firingCooldownCounterLength) {

				if (orbitingDirection) {
					GameObject bullet = GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
					bullet.transform.Rotate (0, 0, 90);
				} else {
					GameObject bullet = GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
					bullet.transform.Rotate (0, 0, -90);
				}

				AudioManager.audioManager.playOrbiterShot();
				firingCooldownCounter = 0;
			}
				
		}

		// Moving In toward player
		if (aggroedAIState == 2) {
			playerPosition = GameManagerScript.gameManager.player.transform.position;
			dir = playerPosition - transform.position;

			// Face sidewide to Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			if (orbitingDirection) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle + 25), Vector3.forward), turningSpeed * Time.deltaTime);
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle - 25 + 180), Vector3.forward), turningSpeed * Time.deltaTime);
			}

			dir = playerPosition - transform.position;

			// Orbiting Velocity
			gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * 2;
		}

		// Moving Out toward player
		if (aggroedAIState == 3) {
			playerPosition = GameManagerScript.gameManager.player.transform.position;
			dir = playerPosition - transform.position;

			// Face sidewide to Player
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			if (orbitingDirection) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle - 25), Vector3.forward), turningSpeed * Time.deltaTime);
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle + 25 + 180), Vector3.forward), turningSpeed * Time.deltaTime);
			}

			dir = playerPosition - transform.position;

			// Orbiting Velocity
			gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * 2;
		}
			
	}

	bool checkRaycastingForPotentialMissile() {

		Vector2 start = transform.position ;
		Vector2 direction = -transform.up;
		RaycastHit2D hit = Physics2D.Raycast (start,direction);

		if (hit) {
			if ((hit.collider.gameObject.layer == 8) || (hit.collider.gameObject.layer == 10) || (hit.collider.gameObject.layer == 11)) {
				//Debug.Log ("DETECTED");
				return true;
			}
		}

		return false;

	}

	void OnCollisionEnter2D(Collision2D target) {

		// Destroy Debre ship Collided With
		if ((target.gameObject.layer == 8) || (target.gameObject.layer == 10) || (target.gameObject.layer == 11)) { // Attached or Thrown Debre

			if (target.gameObject.layer == 8) { // Player Layer

				GameManagerScript.gameManager.currentHealth -= ramDamage;

				if (this.gameObject.GetComponent<enemyAI> ().shipType == "Dreadnaught") {
					GameManagerScript.gameManager.currentHealth -= ramDamage * 2;
				}
				if (this.gameObject.GetComponent<enemyAI> ().shipType == "AlienK") {
					GameManagerScript.gameManager.currentHealth -= ramDamage * 2;
				}

			}

			if ((target.gameObject.layer == 10) || (target.gameObject.layer == 11)) { // Debre Layer

				target.gameObject.GetComponent<Debris>().currentHP -= ramDamage;

				if (this.gameObject.GetComponent<enemyAI> ().shipType == "Dreadnaught") {
					target.gameObject.GetComponent<Debris>().currentHP -= ramDamage * 2;
				}
				if (this.gameObject.GetComponent<enemyAI> ().shipType == "AlienK") {
					target.gameObject.GetComponent<Debris>().currentHP -= ramDamage * 2;
				}

			}

		}
			
		// Play Explosion on Destruction of Enemy Ship
		AudioManager.audioManager.playExplosion();
		//GameObject.Destroy(gameObject);

		// Enemy Ship Takes Damage from Collision
		this.gameObject.GetComponent<enemyAI>().enemyShipHP -= ramDamage;

	}

	void checkToShootProjectile() {
		// Enemy Ship Aggroed Projectile Shooting AI
		distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);
		if (distanceToPlayer < distanceToShootPlayer) {
			
			if (checkRaycastingForPotentialMissile ()) {
				
				// Fire Missle
				firingCooldownCounter++;
				if (firingCooldownCounter > firingCooldownCounterLength) {
					if (shipType == "Dreadnaught") {
						GameObject.Instantiate (bigEnemyProjectile, transform.position, transform.rotation);
						AudioManager.audioManager.playDreadnaughtShot();
					} else {
						GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
						AudioManager.audioManager.playFighterShot();
					}
					aggroedAIState = 2; // Go Away from Player
					aggroedAIStateCounter = 0;
					firingCooldownCounter = 0;
				}
					
			}
		}

	}

	void checkToShootProjectileDreadnaught() {
		// Enemy Ship Aggroed Projectile Shooting AI
		distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);
		if (distanceToPlayer < distanceToShootPlayer) {

			if (checkRaycastingForPotentialMissile ()) {

				// Fire Missle
				firingCooldownCounter++;
				if (firingCooldownCounter > firingCooldownCounterLength) {
					if (shipType == "Dreadnaught") {
						GameObject.Instantiate (bigEnemyProjectile, transform.position, transform.rotation);
						AudioManager.audioManager.playDreadnaughtShot();
					} else {
						GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
						AudioManager.audioManager.playFighterShot();
					}
					firingCooldownCounter = 0;
				}

			}
		}

	}


}
