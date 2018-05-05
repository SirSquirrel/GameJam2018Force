using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisTurret : Debris{

    float fireRate = 2f;
    float fireCounter = 0f;
    float powerUse = 5f;
    public GameObject bullet;
    public bool activated = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            Shoot();
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
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

    protected void Shoot()
    {
        if (GameManagerScript.gameManager.power > powerUse && fireCounter < Time.time)
        {
            fireCounter = Time.time + fireRate;
            //no time.delta time because there is a timer
            GameManagerScript.gameManager.power -= powerUse;
            GameObject.Instantiate(bullet,transform.position,transform.rotation);
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
