using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
    public static TimerScript timerScript = null;
    public float counter = 0f;
    public bool counting = false;
    public Text timerText;
	// Use this for initialization
	void Start () {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        if (timerScript == null) {
            timerScript = this;
            timerScript.CountBegin();
        }
        else {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (counting)
        {
            counter += Time.deltaTime;
            timerText.text = ((int)counter).ToString();
        }
	}

    public void CountBegin()
    {
        counting = true;
    }

    public void CountStop()
    {
        counting = false;
    }

    public void CountReset()
    {
        counter = 0f;
    }
}
