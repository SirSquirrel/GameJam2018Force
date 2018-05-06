using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    public void PlayGame(){
        SceneManager.LoadScene("kevinTest");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Highscore()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
