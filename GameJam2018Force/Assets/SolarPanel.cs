using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Debris {

    public float powerRegen = 1f;
    public bool added = false;
    // Update is called once per frame
    new void Update()
    {
        if (added == true && attached == false)
        {
            GameManagerScript.gameManager.powerRegenRate -= powerRegen;
            added = false;
        }
        base.Update();
    }

    new public void Die()
    {
        if (attached)
        {
            GameManagerScript.gameManager.powerRegenRate -= powerRegen;
        }
        base.Die();
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

    protected new void attach(GameObject NewParent)
    {
        GameManagerScript.gameManager.powerRegenRate -= powerRegen;
        added = true;
        base.attach(NewParent);
    }
}
