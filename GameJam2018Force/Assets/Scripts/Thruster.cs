﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Debris {
    float thrustSpeed = 12f;
    float powerUse = 5f;
    public bool activated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!attached && activated == true)
        {
            activated = false;
        }

        if (GameManagerScript.gameManager.selected == this)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameManagerScript.gameManager.selected = null;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (!glued && Input.GetKey(KeyCode.Space) && mouseSelectionCounter < Time.time)
            {
                Detach();
            }
        }
        if (reContactReady && reContactCounter < Time.time)
        {
            gameObject.layer = 9;

            foreach (Transform child in transform)
            {
                child.gameObject.layer = 9;
            }
            reContactReady = false;
        }
        if (activated)
        {
            Push();
        }
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1)){
            if (!activated)
            {
                activated = true;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (activated)
            {
                activated = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    protected void Push()
    {
        if (GameManagerScript.gameManager.power > powerUse) {
            GameManagerScript.gameManager.power -= powerUse * Time.deltaTime;
            GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustSpeed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        if (!activated)
        {
            base.OnMouseDown();
        }
    }
}
