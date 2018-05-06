using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Debris {
    float thrustSpeed = 12f;
    float powerUse = 5f;
    public bool activated = false;
	ParticleSystem[] thrustingEffect;

	// Use this for initialization
	void Start () {

		thrustingEffect = GetComponentsInChildren<ParticleSystem>();

	}

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (activated)
        {
            Push();
        }
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1)){
            if (!activated)
            {
				activated = true;
				foreach (ParticleSystem c in thrustingEffect) {
					c.Play();
				}
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (activated)
            {
				activated = false;
				foreach (ParticleSystem c in thrustingEffect) {
					c.Stop();
				}
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    protected void Push()
	{
		if (GameManagerScript.gameManager.power <= powerUse) {
			foreach (ParticleSystem c in thrustingEffect) {

				activated = false;
				c.Stop ();
				GetComponent<SpriteRenderer>().color = Color.white;

				//ParticleSystem.EmissionModule em = c.emission;
				//em.rateOverTime = new ParticleSystem.MinMaxCurve (100.0f, new AnimationCurve());


			}
		} else {
			foreach (ParticleSystem c in thrustingEffect) {

			}
		}
        if (GameManagerScript.gameManager.power > powerUse) {
            GameManagerScript.gameManager.power -= powerUse * Time.deltaTime;
            GameManagerScript.gameManager.player.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustSpeed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        if (!activated)
        {
            base.OnMouseDown();
        }
    }
}
