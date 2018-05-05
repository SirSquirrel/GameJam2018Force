using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Refresh : Debris {
    public float oxygen = 10;
	// Use this for initialization
	void Start () {
		
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

    protected new void attach(GameObject NewParent)
    {
        GameManagerScript.gameManager.oxygen += oxygen;
        oxygen = 0;
        base.attach(NewParent);
    }
}
