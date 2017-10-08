using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWave : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSound() {
        anim.SetBool("isPlaying", true);
    }

    public void stopSound() {
        anim.SetBool("isPlaying", false);
    }
}
