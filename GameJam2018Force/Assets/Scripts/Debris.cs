﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {
    
    public bool attached = false;
    protected float mouseSelectionCooldown = 0.2f;
    protected float mouseSelectionCounter = 0f;
    protected float reContactCooldown = 2f;
    protected float reContactCounter = 0f;
    protected bool reContactReady = false;
    float speedModifier = 75;
    float objectBonusSpeed = 2;
    public float maxSpeedPerThrow = 5;
    public float currentHP = 20f;
    float maxHP = 20f;
    public bool glued = false;

	public ParticleSystem damagedHullEffect;
	public bool emittingDamagedHullEffect = false;

    // Use this for initialization
	void Start () {

		damagedHullEffect = GetComponentInChildren<ParticleSystem>();
	}

    public void Die()
    {
		Object explosionEffect = Resources.Load ("Explosion");
		Object.Instantiate (explosionEffect, transform.position, transform.rotation);

        AudioManager.audioManager.playExplosion();
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	public void Update () {

		// Damaged Hull Effect
		if (attached) {
			if (currentHP <= maxHP * 0.6f) {
				//damagedHullEffect.GetComponent<ParticleSystem>().Play();
				damagedHullEffect.Play();
				emittingDamagedHullEffect = true;
			}
			else if (emittingDamagedHullEffect) {
				if (currentHP > maxHP * 0.6f) {
					//damagedHullEffect.GetComponent<ParticleSystem>().Stop();
					damagedHullEffect.Stop();
					emittingDamagedHullEffect = false;
				}
			}
		
		}

        if (GameManagerScript.gameManager.selected == this) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameManagerScript.gameManager.selected = null;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (Input.GetKey(KeyCode.Space) && mouseSelectionCounter < Time.time)
            {
                Detach();
            }
        }
        if(reContactReady && reContactCounter < Time.time)
        {
            gameObject.layer = 9;

            foreach (Transform child in transform)
            {
                child.gameObject.layer = 9;
            }
            reContactReady = false;
        }
        if (attached)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().velocity;
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Detach()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (target - transform.position);
        dir.Normalize();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Debris>())
            {
                Debris debrisScript = child.GetComponent<Debris>();
                debrisScript.Detach(dir);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        GameManagerScript.gameManager.selected = null;
        gameObject.layer = 11;
        GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.detach);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        reContactCounter = Time.time + reContactCooldown;
        reContactReady = true;
        dir = dir * speedModifier;
        Debug.Log(dir);
        dir = dir * (transform.childCount + 1);
        transform.parent = null;
        GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x, -dir.y));
        dir = dir * objectBonusSpeed;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y));
        attached = false;
    }

    public void Detach(Vector3 Direction)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Debris>())
            {
                Debris debrisScript = child.GetComponent<Debris>();
                debrisScript.Detach(Direction);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        GameManagerScript.gameManager.selected = null;
        gameObject.layer = 11;
        GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.detach);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        reContactCounter = Time.time + reContactCooldown;
        reContactReady = true;
        if (Direction.magnitude > maxSpeedPerThrow)
        {
        }
        Direction = Direction * speedModifier;
        Direction = Direction * (transform.childCount + 1);
        transform.parent = null;
        GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Direction.x, -Direction.y));
        Direction = Direction * objectBonusSpeed;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction.x, Direction.y));
        attached = false;
    }

    protected void attach(GameObject NewParent)
    {
        if (NewParent.GetComponent<Debris>())
        {
            if (NewParent.GetComponent<Debris>().reContactReady)
            {
                return;
            }
        }
        attached = true;
        transform.parent = NewParent.transform;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<Rigidbody2D>().velocity = GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().velocity;

        gameObject.layer = 10;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = 10;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached)
        {
            if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
            {
                attach(collision.gameObject);
            }
        }
    }

    protected void OnMouseDown()
    {
        if (GameManagerScript.gameManager.selected != this && attached && !glued)
        {
            if (GameManagerScript.gameManager.selected != null)
            {
                GameManagerScript.gameManager.selected.GetComponent<SpriteRenderer>().color = Color.white;
            }

            GetComponent<SpriteRenderer>().color = Color.green;
            GameManagerScript.gameManager.selected = this;
            GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.select);
            mouseSelectionCounter = mouseSelectionCooldown + Time.time;
        }
    }
}
