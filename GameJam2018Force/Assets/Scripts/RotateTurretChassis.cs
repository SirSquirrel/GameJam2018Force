using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurretChassis : Debris{
   

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && attached)
        {
            bool activated = GetComponentInChildren<RotateTurret>().activated;
            if (!activated)
            {
                activated = true;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (activated)
            {
                activated = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            GetComponentInChildren<RotateTurret>().activated = activated;
        }
    }
}
