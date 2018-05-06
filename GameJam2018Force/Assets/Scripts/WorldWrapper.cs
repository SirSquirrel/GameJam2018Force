using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWrapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 direction = collision.transform.position - transform.position;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.transform.position -= new Vector3 (2*direction.x, 2 * direction.y,0);
    }
}
