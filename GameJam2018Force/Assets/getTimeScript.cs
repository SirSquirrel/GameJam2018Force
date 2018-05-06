using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getTimeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerStats.timeSurvived > 0)
        {
            GetComponent<Text>().text = "Signal Length:" + (int)PlayerStats.timeSurvived +" Seconds";
        }
	}
}
