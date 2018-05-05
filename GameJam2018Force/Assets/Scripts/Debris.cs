using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {

    bool selected = false;
    public bool attached = false;
    public float mouseSelectionCooldown = 0.2f;
    public float mouseSelectionCounter = 0f;
    public float speedModifier = 5;


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
                selected = false;
                attached = false;
                Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 dir = target - transform.position;
                dir = dir * speedModifier;
                transform.parent = null;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y));
                GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x, -dir.y));
            }
        }
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
