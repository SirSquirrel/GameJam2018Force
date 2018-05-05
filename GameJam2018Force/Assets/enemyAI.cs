﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public string shipType = "Fighter";
	public bool aggroed = false;
	public float distanceToAggroPlayer = 10;
	public float distanceToShootPlayer = 5;
	float distanceToPlayer = 1000;

	int wanderingAIState = 0;
	int wanderingAIStateCounter = 0;
	int wanderingAIStateCounterLength = 50;
	float wanderingAIRotationStrength = 0.3f;
	float maxWanderingVelocity = 1.5f;

	int aggroedAIState = 0;
	int aggroedAIStateCounter = 0;
	int aggroedAIStateCounterLength = 50;
	float maxAggroedVelocity = 1.5f;

	float turningSpeed = 3.0f;
	float xRandomMoveTo = 0;
	float yRandomMoveTo = 0;
	float xRandomMoveTo2 = 0;
	float yRandomMoveTo2 = 0;
	float distanceToTurnAwayFromPlayer = 3;

	int firingCooldownCounter = 0;
	int firingCooldownCounterLength = 50;

	Vector3 playerPosition;
	Vector3 dir;


	public GameObject enemyProjectile;

	// Use this for initialization
	void Start () {
		
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
			updateFighterAggroAI();





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
		//gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * 0.4f;
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
			Debug.Log ("Enemy Ship Aggroed Player because In Detection Range! " + distanceToPlayer + "  " + distanceToAggroPlayer);
		}

	}

	void updateFighterAggroAI() {

		playerPosition = GameManagerScript.gameManager.player.transform.position;
		dir = playerPosition - transform.position;

		// Check To Shoot Projectile
		checkToShootProjectile();

		aggroedAIStateCounter++;
		if (aggroedAIStateCounter > aggroedAIStateCounterLength) {
			aggroedAIState = Random.Range(1, 5);
			aggroedAIStateCounterLength = Random.Range(100, 250);
			aggroedAIStateCounter = 0;

			// Steer Away Cancel Velocity
			if (aggroedAIState == 2) {
				//gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}

			// Firing Random Rate
			firingCooldownCounterLength = Random.Range(45,55);

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

		// Steer Directly To the Player
		if (aggroedAIState == 1) {

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
			}
				
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
			
	}

	void updateOrbiterAggroAI() {


	}

	bool checkRaycastingForPotentialMissile() {

		Vector2 start = transform.position ;
		Vector2 direction = -transform.up;
		RaycastHit2D hit = Physics2D.Raycast (start,direction);

		if (hit) {
			if ((hit.collider.gameObject.layer == 8) || (hit.collider.gameObject.layer == 10) || (hit.collider.gameObject.layer == 11)) {
				Debug.Log ("DETECTED");
				return true;
			}
		}

		return false;

	}

	void OnCollisionEnter2D(Collision2D target) {

		// Destroy Debre ship Collided With
		if ((target.gameObject.layer == 8) || (target.gameObject.layer == 10) || (target.gameObject.layer == 11)) { // Attached or Thrown Debre

			GameObject.Destroy(target.gameObject);
			// Decrease HP instead******

		}
			
		// Play Explosion on Destruction of Enemy Ship
		AudioManager.audioManager.playExplosion();
		GameObject.Destroy(gameObject);

	}

	void checkToShootProjectile() {
		// Enemy Ship Aggroed Projectile Shooting AI
		distanceToPlayer = Vector2.Distance (transform.position,GameManagerScript.gameManager.player.transform.position);
		if (distanceToPlayer < distanceToShootPlayer) {
			
			if (checkRaycastingForPotentialMissile ()) {
				
				// Fire Missle
				firingCooldownCounter++;
				if (firingCooldownCounter > firingCooldownCounterLength) {
					GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
					firingCooldownCounter = 0;
				}
					
			}
		}

	}


}