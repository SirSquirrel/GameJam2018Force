using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {

    bool selected = false;
    public bool attached = false;
    float mouseSelectionCooldown = 0.2f;
    float mouseSelectionCounter = 0f;
    float reContactCooldown = 2f;
    float reContactCounter = 0f;
    bool reContactReady = false;
    float speedModifier = 15;
    public float maxSpeedPerThrow = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManagerScript.gameManager.selected == this) {
            if (Input.GetMouseButtonDown(1))
            {
                GameManagerScript.gameManager.selected = null;
            }
            else if (Input.GetMouseButtonDown(0) && mouseSelectionCounter < Time.time)
            {
                Detach();
            }
        }
        if(reContactReady && reContactCounter < Time.time)
        {
            gameObject.layer = 9;

            foreach (Transform child in transform)
            {
                child.gameObject.layer = 9;
            }
            reContactReady = false;
        }
    }

    private void Detach()
    {
        gameObject.layer = 11;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 11;
        }
        GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.detach);
        selected = false;
        attached = false;
        reContactCounter = Time.time + reContactCooldown;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = target - transform.position;
        reContactReady = true;
        if (dir.magnitude > maxSpeedPerThrow)
        {
        }
        dir = dir * (transform.childCount + 1);
        dir = dir * speedModifier;
        transform.parent = null;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y));
        GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x, -dir.y));
    }

    void attach(GameObject NewParent)
    {
        if (NewParent.GetComponent<Debris>())
        {
            if (NewParent.GetComponent<Debris>().reContactReady)
            {
                return;
            }
        }
        attached = true;
        transform.parent = NewParent.transform;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().velocity;

        gameObject.layer = 10;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = 10;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            attach(collision.gameObject);
        }
    }

    void OnMouseDown()
    {
        if (GameManagerScript.gameManager.selected == null && attached)
        {
            GameManagerScript.gameManager.selected = this;
            GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.select);
            selected = true;
            mouseSelectionCounter = mouseSelectionCooldown + Time.time;
        }
    }
}
