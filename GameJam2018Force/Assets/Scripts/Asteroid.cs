using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	float currentHP = 15f;
	float maxHP = 15f;
	int attackDamage = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHP <= 0)
		{
			Die();
		}

	}

	public void Die()
	{
		AudioManager.audioManager.playExplosion();
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool hit = false;

		if (collision.gameObject.layer == 8) {
			gameObject.GetComponent<Asteroid>().currentHP -= attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
			GameManagerScript.gameManager.currentHealth -= attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
			Debug.Log("DMG:" + attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
			hit = true;

		}

		if (collision.gameObject.layer == 10) {
			gameObject.GetComponent<Asteroid>().currentHP -= attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
			collision.gameObject.GetComponent<Debris>().currentHP -= attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
			Debug.Log("DMG:" + attackDamage * collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
			hit = true;
		}

		if (hit) {
			GameManagerScript.gameManager.player.GetComponent<Rigidbody2D> ().velocity -= gameObject.GetComponent<Rigidbody2D> ().velocity; // Player
			gameObject.GetComponent<Rigidbody2D> ().velocity *= 2.5f; // Asteroid
		}


		hit = false;

	}

}
