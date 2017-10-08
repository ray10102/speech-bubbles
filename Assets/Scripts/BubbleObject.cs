using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;


[System.Serializable]
public class BubbleObject
{
	public BubbleObject (AudioSource audioSource,
	                     Animator anim,
	                     BubbleWave wave,
	                     Vector3 position)
	{	
		this.audioSource = audioSource;
		this.anim = anim;
		this.wave = wave;
		this.position = position;
	}

	public void Set(AudioSource audioSource,
		Animator anim,
		BubbleWave wave,
		Vector3 position) {
		this.audioSource = audioSource;
		this.anim = anim;
		this.wave = wave;
		this.position = position;
	}
	private AudioSource audioSource{ get; set; }

	private Animator anim{ get; set; }

	private BubbleWave wave{ get; set; }

	private Vector3 position{ get; set; }

	private Quaternion rotation { get; set; }
}