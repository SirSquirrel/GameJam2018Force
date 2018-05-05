using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Debris {
    float thrustSpeed = 8f;
    public bool glued = false;
    
    float powerUse = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManagerScript.gameManager.selected == this)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameManagerScript.gameManager.selected = null;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (!glued && Input.GetMouseButtonDown(0) && mouseSelectionCounter < Time.time)
            {
                Detach();
            }
            Push();
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
    }

    protected void Push()
    {
        if (GameManagerScript.gameManager.power > powerUse) {
            GameManagerScript.gameManager.power -= powerUse * Time.deltaTime;
            GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustSpeed * Time.deltaTime);
        }
    }
}
