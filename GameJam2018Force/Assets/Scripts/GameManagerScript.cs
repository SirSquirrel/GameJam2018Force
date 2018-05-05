﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
    public GameObject player = null;
    public Debris selected = null;
    public static GameManagerScript gameManager;
    public float power = 100;
    public float maxPower = 100;
    public float powerRegenRate = 2f;
    public Slider powerSlider;
    public float oxygen = 100;
    public float maxOxygen = 100;
    public float oxygenDepleteRate = 0.8f;
    public Slider oxygenSlider;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public float gameAreaSize = 500f;
    public float tractorBeamSpeed = 500f;
    // Use this for initialization
    void Start() {
        gameManager = this;
        powerSlider.maxValue = maxPower;
        oxygenSlider.maxValue = maxPower;
        healthSlider.maxValue = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        powerManagement();
        leakOxygen();
        checkHealth();
        checkBounds();
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

    public void checkHealth()
    {
        if (currentHealth < 0)
        {
            gameOver();
        }
        healthSlider.value = currentHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void checkBounds()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude > gameAreaSize)
        {
            player.GetComponent<Rigidbody2D>().AddForce(-direction);
        }
    }

    public void gameOver()
    {
        Debug.Log("over");
    }
}
