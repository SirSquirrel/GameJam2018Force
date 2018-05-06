using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOverTime : MonoBehaviour {
    public float lifeSpan = 10f;
    float lifeCounter = 0f;
	// Use this for initialization
	void Start () {
        lifeCounter = lifeSpan + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (lifeCounter < Time.time)
        {
            Destroy(gameObject);
        }
	}
}
