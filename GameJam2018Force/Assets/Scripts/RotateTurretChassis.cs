using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurretChassis : Debris{
   
    new void Update()
    {
        if (GetComponentInChildren<RotateTurret>().activated && attached == false)
        {
            GetComponentInChildren<RotateTurret>().activated = false;
        }
        base.Update();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && attached)
        {
            bool activated = GetComponentInChildren<RotateTurret>().activated;
            if (!activated)
            {
                transform.FindChild("ToggleOff").gameObject.SetActive(false);
                transform.FindChild("ToggleOn").gameObject.SetActive(true);
                activated = true;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (activated)
            {
                activated = false;
                transform.FindChild("ToggleOff").gameObject.SetActive(true);
                transform.FindChild("ToggleOn").gameObject.SetActive(false);
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            GetComponentInChildren<RotateTurret>().activated = activated;
        }
    }
}
