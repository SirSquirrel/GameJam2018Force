using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void Retry()
    {
        SceneManager.LoadScene("kevinTest");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
