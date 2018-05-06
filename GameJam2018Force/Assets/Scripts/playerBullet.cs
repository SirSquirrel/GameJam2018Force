using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour {
    public float projectileSpeed = 5.0f;
    public float damage = 10f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * projectileSpeed;

    }

    void OnTriggerEnter2D(Collider2D target)
    {


        // Destroy Debre ship Collided With
        if ((target.gameObject.layer == 12))
        { // Attached or Thrown Debre
            target.GetComponent<enemyAI>().enemyShipHP -= (int)damage;
        }


        // Play Explosion on Destruction of Enemy Ship
        AudioManager.audioManager.playExplosion();
        GameObject.Destroy(gameObject);

    }
}
