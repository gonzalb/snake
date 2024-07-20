using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip eatSound;
	public static AudioClip deathSound;


	static AudioSource AudioSource;
	// Use this for initialization
	void Start () {

		eatSound = Resources.Load<AudioClip>("eat");
		deathSound = Resources.Load<AudioClip>("death");

		AudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound (string clip)
	{
		switch (clip)
		{	
			case "eat":
				AudioSource.PlayOneShot(eatSound);
				break;	
			
			case "death":
				AudioSource.PlayOneShot(deathSound);
				break;	
		}
	}
	
}
