using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public List<GameObject> enemies;
    public float enemeySpawnRate = 5f;
    public float enemySpawnCounter = 5f;

    public float timeModifier = 1.2f;
    public float timeModifierEvaluated = 30f;
    float timeModifierCounter = 0f;

    public List<GameObject> debris;
    public float debrisSpawnRate = 2f;
    public float debrisSpawnCounter = 2f;

    public List<GameObject> resources;
    public float resourceSpawnRate = 2f;
    public float resourceSpawnCounter = 2f;

    public List<GameObject> asteroids;
    public float asteroidSpawnRate = 10f;
    public float asteroidSpawnCounter = 2f;

    //how far they are required to spawn from the player
    public float rangeFromPlayer = 10f;
    public float spawnRange = 50f;

    // Use this for initialization
    void Start () {
        timeModifierCounter = Time.time + timeModifierEvaluated;
        enemySpawnCounter = enemeySpawnRate  + Time.time;
        debrisSpawnCounter = debrisSpawnRate + Time.time;
        resourceSpawnCounter = resourceSpawnRate + Time.time;
        asteroidSpawnCounter = asteroidSpawnRate + Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        enemyCheck();
        debrisCheck();
        resourceCheck();
	}

    void enemyCheck() {
        if (enemySpawnCounter < Time.time)
        {
            Vector3 spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            while ((GameManagerScript.gameManager.player.transform.position - spawnPoint).magnitude < rangeFromPlayer)
            {
                spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            }
            int enemyNum = Random.Range(0,enemies.Count);

            GameObject enemy = GameObject.Instantiate(enemies[enemyNum],spawnPoint,transform.rotation);
            enemy.transform.Rotate(0,0,Random.Range(0,360));
            enemyNum += 1;
            enemySpawnCounter = enemeySpawnRate  + Time.time;
        }
        if (timeModifierCounter < Time.time)
        {
            timeModifierCounter = Time.time + timeModifierEvaluated;
            enemySpawnCounter = enemySpawnCounter / timeModifier;
        }
    }

    void debrisCheck()
    {
        if (debrisSpawnCounter < Time.time)
        {
            Vector3 spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            while ((GameManagerScript.gameManager.player.transform.position - spawnPoint).magnitude < rangeFromPlayer)
            {
                spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            }
            int debrisNum = Random.Range(0, debris.Count);

            GameObject debrisChunk = GameObject.Instantiate(debris[debrisNum], spawnPoint, transform.rotation);
            debrisChunk.transform.Rotate(0, 0, Random.Range(0, 360));
            debrisNum += 1;
            debrisSpawnCounter = debrisSpawnRate + +Time.time;
        }
    }

    void resourceCheck()
    {
        if (debrisSpawnCounter < Time.time)
        {
            Vector3 spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            while ((GameManagerScript.gameManager.player.transform.position - spawnPoint).magnitude < rangeFromPlayer)
            {
                spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            }
            int resourceNum = Random.Range(0, debris.Count);

            GameObject resourceChunk = GameObject.Instantiate(resources[resourceNum], spawnPoint, transform.rotation);
            resourceChunk.transform.Rotate(0, 0, Random.Range(0, 360));
            resourceSpawnCounter = resourceSpawnRate + +Time.time;
        }
    }

    void asteroidCheck()
    {
        if (asteroidSpawnCounter < Time.time)
        {
            Vector3 spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            while ((GameManagerScript.gameManager.player.transform.position - spawnPoint).magnitude < rangeFromPlayer)
            {
                spawnPoint = (Random.insideUnitCircle * spawnRange) + (Vector2)transform.position;
            }
            int asteroidNum = Random.Range(0, asteroids.Count);

            GameObject asteroid = GameObject.Instantiate(asteroids[asteroidNum], spawnPoint, transform.rotation);
            asteroid.transform.Rotate(0, 0, Random.Range(0, 360));
            asteroidNum += 1;
            asteroidSpawnCounter = asteroidSpawnRate +Time.time;
        }
    }
}
