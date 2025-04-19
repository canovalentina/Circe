using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour {

    public AudioSource audioSrc;

		void Awake () 
		{
				audioSrc = GetComponent<AudioSource>();
		}
}
