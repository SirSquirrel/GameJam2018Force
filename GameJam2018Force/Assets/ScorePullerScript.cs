using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePullerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("highScore1"))
        {
            PlayerPrefs.SetInt("highScore1",200);
        }
        if (!PlayerPrefs.HasKey("highScore2"))
        {
            PlayerPrefs.SetInt("highScore2", 150);
        }
        if (!PlayerPrefs.HasKey("highScore3"))
        {
            PlayerPrefs.SetInt("highScore3", 100);
        }
        if (!PlayerPrefs.HasKey("highScore4"))
        {
            PlayerPrefs.SetInt("highScore4", 50);
        }
        if (!PlayerPrefs.HasKey("highScore5"))
        {
            PlayerPrefs.SetInt("highScore5", 25);
        }

        GetComponent<Text>().text = PlayerPrefs.GetInt("highScore1").ToString() + "\n" + PlayerPrefs.GetInt("highScore2").ToString() + "\n" + PlayerPrefs.GetInt("highScore3").ToString() + "\n" + PlayerPrefs.GetInt("highScore4").ToString() + "\n" + PlayerPrefs.GetInt("highScore5").ToString() + "\n"   ;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
