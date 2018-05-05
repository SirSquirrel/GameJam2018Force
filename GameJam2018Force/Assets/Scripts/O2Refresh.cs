using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Refresh : Debris {
    public float oxygen = 10;
	// Use this for initialization
	void Start () {
		
	}

    protected new void attach(GameObject NewParent)
    {
        GameManagerScript.gameManager.oxygen += oxygen;
        Debug.Log(oxygen);
        oxygen = 0;
        base.attach(NewParent);
    }
}
