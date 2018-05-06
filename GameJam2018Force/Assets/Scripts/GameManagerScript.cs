using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public bool gameEnding = false;
    public float gameOverTimer = 0f;

	int gameOverExplosionCounter = 0;

	public ParticleSystem damagedHullEffect;
	public bool emittingDamagedHullEffect = false;

	public ParticleSystem damagedHullEffectSevere;
	public bool emittingDamagedHullEffectSevere = false;


    // Use this for initialization
    void Start() {
        gameManager = this;
        powerSlider.maxValue = maxPower;
        oxygenSlider.maxValue = maxPower;
        healthSlider.maxValue = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

		//damagedHullEffect = GetComponentInChildren<ParticleSystem>();
		//damagedHullEffectSevere = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnding && gameOverTimer < Time.time)
        {
            SceneManager.LoadScene("GameOver");
        }
        powerManagement();
        leakOxygen();
        checkHealth();
		damagedHullLogic();
    }

	private void damagedHullLogic() {

		if (currentHealth <= maxHealth * 0.5f) {
			damagedHullEffect.Play();
			emittingDamagedHullEffect = true;
		}
		else if (emittingDamagedHullEffect) {
			if (currentHealth > maxHealth * 0.5f) {
				damagedHullEffect.Stop();
				emittingDamagedHullEffect = false;
			}
		}
			
		if (currentHealth <= maxHealth * 0.25f) {
			damagedHullEffectSevere.Play();
			emittingDamagedHullEffectSevere = true;
		}
		else if (emittingDamagedHullEffectSevere) {
			if (currentHealth > maxHealth * 0.25f) {
				damagedHullEffectSevere.Stop();
				emittingDamagedHullEffectSevere = false;
			}
		}

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
        if (oxygen <= 0)
        {
            gameOver();
        }
    }

    public void checkHealth()
    {
        healthSlider.value = currentHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
		if (currentHealth <= 0)
		{
			gameOver();
		}
    }

    public void gameOver()
    {
        if (!gameEnding) {
        PlayerStats.timeSurvived = TimerScript.timerScript.counter;
        gameEnding = true;
        gameOverTimer = Time.time + 2f;
        }
        if (gameOverExplosionCounter == 0) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 30) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 60) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 70) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 90) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 120) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
		if (gameOverExplosionCounter == 125) {
			Object explosionEffect = Resources.Load ("Explosion");
			Vector3 newPos = transform.position;
			newPos.x += Random.Range(-1.0f,1.0f);
			newPos.y += Random.Range(-1.0f,1.0f);
			Object.Instantiate (explosionEffect, newPos, transform.rotation);
			AudioManager.audioManager.playExplosion();
		}
        gameOverExplosionCounter++;
    }
}
