using System.Collections;
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
    new void Update()
    {
        base.Update();
        if (activated)
        {
            Push();
        }
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && attached){
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
