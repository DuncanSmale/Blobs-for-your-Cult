using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public AudioSource sfx;
	public AudioSource MusicSource;
	public static SoundController instance = null;
	private float higherPitch = 1.05f;
	private float lowerPitch = 0.95f;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
		//DontDestroyOnLoad (gameObject); keeps music playing
	}

	public void Playone(AudioClip clip){

		sfx.clip = clip;
		sfx.Play ();
	}

	public void RandomPitchandsfx (params AudioClip[] clips){ 		//params creates an array if in the call statement, the items are sent separated by a list
		int randomIndex = Random.Range (0, clips.Length);			//length of the array clips ie the number of sounds sent
		float randomPitch = Random.Range (lowerPitch, higherPitch);	
		sfx.pitch = randomPitch;
		sfx.clip = clips [randomIndex];								//picks a random clip from the array
		sfx.Play ();
	}

}
