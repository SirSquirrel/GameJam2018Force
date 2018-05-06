using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisTurret : Debris{

    float fireRate = 2f;
    float fireCounter = 0f;
    float powerUse = 5f;
    public GameObject bullet;
    public bool activated = false;
    void Start () {
		
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if (activated)
        {
            Shoot();
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && attached)
        {
            if (!activated)
            {
                activated = true;
                transform.FindChild("ToggleOff").gameObject.SetActive(false);
                transform.FindChild("ToggleOn").gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (activated)
            {
                activated = false;
                transform.FindChild("ToggleOff").gameObject.SetActive(true);
                transform.FindChild("ToggleOn").gameObject.SetActive(false);
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
