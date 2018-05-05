using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
    public GameObject player = null;
    public Debris selected = null;
    public static GameManagerScript gameManager;
    public float power = 100;
    public float maxPower = 100;
    public float powerRegenRate = 1f;
    public Slider powerSlider;
    public float oxygen = 100;
    public float maxOxygen = 100;
    public float oxygenDepleteRate = 0.2f;
    public Slider oxygenSlider;
    // Use this for initialization
    void Start() {
        gameManager = this;
        powerSlider.maxValue = maxPower;
        oxygenSlider.maxValue = maxPower;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        powerManagement();
        leakOxygen();
    }

    private void powerManagement(){
        powerSlider.value = power;
        power += powerRegenRate * Time.deltaTime;
        if (power > maxPower)
        {
            power = maxPower;
        }
    }

    public void leakOxygen()
    {
        oxygenSlider.value = oxygen;
        oxygen -= oxygenDepleteRate*Time.deltaTime;
        if (oxygen > maxOxygen)
        {
            oxygen = maxOxygen;
        }
    }
}
