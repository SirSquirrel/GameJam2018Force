using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public GameObject player = null;
    public Debris selected = null;
    public static GameManagerScript gameManager;
	// Use this for initialization
	void Start () {
        gameManager = this;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
