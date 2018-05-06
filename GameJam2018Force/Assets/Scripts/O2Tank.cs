using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Tank : Debris {
    public float OxyMax = 30f;
    public bool added = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	new void Update () {
        if (added == true && attached == false)
        {
            GameManagerScript.gameManager.maxOxygen -= OxyMax;
            GameManagerScript.gameManager.oxygenSlider.maxValue -= OxyMax;
            added = false;
        }
        base.Update();
    }

    new public void Die() {
        if (attached)
        {
            GameManagerScript.gameManager.maxOxygen -= OxyMax;
            GameManagerScript.gameManager.oxygenSlider.maxValue -= OxyMax;
        }
        base.Die();
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
        added = true;
        base.attach(NewParent);
    }
}
