using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {
    
    public bool attached = false;
    protected float mouseSelectionCooldown = 0.2f;
    protected float mouseSelectionCounter = 0f;
    protected float reContactCooldown = 2f;
    protected float reContactCounter = 0f;
    protected bool reContactReady = false;
    float speedModifier = 15;
    float objectBonusSpeed = 2;
    public float maxSpeedPerThrow = 5;
    public float currentHP = 100f;
    float maxHP = 100f;
    public bool glued = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
        if (GameManagerScript.gameManager.selected == this) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameManagerScript.gameManager.selected = null;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (Input.GetKey(KeyCode.Space) && mouseSelectionCounter < Time.time)
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
        if (attached)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().velocity;
        }
    }

    protected void Detach()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GameManagerScript.gameManager.selected = null;
        gameObject.layer = 11;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 11;
        }
        GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.detach);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = target - transform.position;
        reContactCounter = Time.time + reContactCooldown;
        reContactReady = true;
        if (dir.magnitude > maxSpeedPerThrow)
        {
        }
        dir = dir * (transform.childCount + 1);
        dir = dir * speedModifier;
        transform.parent = null;
        GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x, -dir.y));
        dir = dir * objectBonusSpeed;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y));
        attached = false;
        foreach (Transform child in transform)
        {
            Debris debrisScript = child.GetComponent<Debris>();
            debrisScript.attached = false;
            debrisScript.reContactCounter = Time.time + reContactCooldown;
            debrisScript.reContactReady = true;
        }
    }

    protected void attach(GameObject NewParent)
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
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<Rigidbody2D>().velocity = GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().velocity;

        gameObject.layer = 10;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = 10;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached)
        {
            if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
            {
                attach(collision.gameObject);
            }
        }
    }

    protected void OnMouseDown()
    {
        if (GameManagerScript.gameManager.selected != this && attached && !glued)
        {
            if (GameManagerScript.gameManager.selected != null)
            {
                GameManagerScript.gameManager.selected.GetComponent<SpriteRenderer>().color = Color.white;
            }

            GetComponent<SpriteRenderer>().color = Color.green;
            GameManagerScript.gameManager.selected = this;
            GetComponent<AudioSource>().PlayOneShot(AudioManager.audioManager.select);
            mouseSelectionCounter = mouseSelectionCooldown + Time.time;
        }
    }
}
