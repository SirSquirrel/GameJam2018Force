using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip select;
    public AudioClip shoot;
    public AudioClip detach;
    public AudioClip explosion;
    public AudioClip deSelect;


	public AudioClip dreadnaughtShot;
	public AudioClip fighterShot;
	public AudioClip orbiterShot;


    public static AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = this;
    }
	
	// Update is called once per frame
	void Update () {

	}

	public void playExplosion(){
		GetComponent<AudioSource> ().PlayOneShot (explosion);
	}

	public void playDreadnaughtShot() {

		GetComponent<AudioSource> ().PlayOneShot (dreadnaughtShot);

	}

	public void playFighterShot() {

		GetComponent<AudioSource> ().PlayOneShot (fighterShot);

	}

	public void playOrbiterShot() {

		GetComponent<AudioSource> ().PlayOneShot (orbiterShot);

	}


}
