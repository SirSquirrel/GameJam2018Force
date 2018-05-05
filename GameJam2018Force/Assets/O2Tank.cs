using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Tank : Debris {
    public float OxyMax = 30f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManagerScript.gameManager.selected == this)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                GameManagerScript.gameManager.selected = null;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (Input.GetKey(KeyCode.Space) && mouseSelectionCounter < Time.time)
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
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected new void Detach()
    {
        GameManagerScript.gameManager.maxOxygen -= OxyMax;
        GameManagerScript.gameManager.oxygenSlider.maxValue -= OxyMax;
        base.Detach();
    }

void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached) {
            if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
            {
                attach(collision.gameObject);
            }
        }
    }

    protected new void attach(GameObject NewParent)
    {
        GameManagerScript.gameManager.maxOxygen += OxyMax;
        GameManagerScript.gameManager.oxygenSlider.maxValue += OxyMax;
        base.attach(NewParent);
    }
}
