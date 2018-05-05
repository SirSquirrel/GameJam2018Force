using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public string shipType = "Fighter";

	int wanderingAIState = 0;
	int wanderingAIStateCounter = 0;
	int wanderingAIStateCounterLength = 50;
	float wanderingAIRotationStrength = 0.3f;
	float maxWanderingVelocity = 1.5f;

	public GameObject enemyProjectile;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		updateWanderingAI();

		if (checkRaycastingForPotentialMissile ()) {
			// Fire Missle
			Debug.Log("Fired Missile");
			if (Random.Range (0, 99) == 0) {
				GameObject.Instantiate (enemyProjectile, transform.position, transform.rotation);
			}
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

	void updateFighterAggroAI() {


	}

	void updateOrbiterAggroAI() {


	}

	bool checkRaycastingForPotentialMissile() {

		Vector2 start = transform.position ;
		Vector2 direction = -transform.up;
		RaycastHit2D hit;

		if (Physics2D.Raycast (start,direction)) {
			Debug.Log ("ray hit");
			return true;
		}

		return false;

	}


}
