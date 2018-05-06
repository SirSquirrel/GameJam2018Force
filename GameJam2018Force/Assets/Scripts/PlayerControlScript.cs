using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {
    float rotationSpeed = 0.2f;
    float maxSpeed = 8f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        Rigidbody2D mover = GetComponent<Rigidbody2D>();
        if (mover.velocity.magnitude > maxSpeed)
        {
            mover.velocity = mover.velocity.normalized * maxSpeed;
        }
	}

    void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,0,rotationSpeed));
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        }
    }
}
