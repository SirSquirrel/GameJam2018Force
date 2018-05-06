using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {
    float rotationSpeed = 20f;
    public bool activated = false;

    float fireRate = 1.2f;
    float fireCounter = 0f;
    float powerUse = 5f;
    public GameObject bullet;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //activated set by parent
        if (activated)
        {
            Rotate();
            Shoot();
        }
	}

    void Rotate()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }

    void Shoot()
    {
        if (GameManagerScript.gameManager.power > powerUse && fireCounter < Time.time)
        {
            fireCounter = Time.time + fireRate;
            //no time.delta time because there is a timer
            GameManagerScript.gameManager.power -= powerUse;
            bullet = GameObject.Instantiate(bullet, transform.position, transform.rotation);
            bullet.transform.Rotate(0,0,180);
        }
    }
}
