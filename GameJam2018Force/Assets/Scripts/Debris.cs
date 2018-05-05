using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {

    bool selected = false;
    public bool attached = false;
    public float mouseSelectionCooldown = 0.2f;
    public float mouseSelectionCounter = 0f;
    public float speedModifier = 5;
    public float maxSpeedPerThrow = 5;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected) {
            if (Input.GetMouseButtonDown(1))
            {
                selected = false;
            }
            else if (Input.GetMouseButtonDown(0) && mouseSelectionCounter < Time.time)
            {
                Throw();
            }
        }
    }

    private void Throw()
    {
        selected = false;
        attached = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = target - transform.position;
        
        if (dir.magnitude > maxSpeedPerThrow)
        {
        }
        dir = dir * (transform.childCount + 1);
        dir = dir * speedModifier;
        transform.parent = null;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y));
        GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x, -dir.y));
    }

    void attach()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.layer = 10;
    }

    void OnMouseDown()
    {
        if (!selected)
        {
            selected = true;
            mouseSelectionCounter = mouseSelectionCooldown + Time.time;
        }
    }
}
