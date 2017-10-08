using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closePopup : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void Close() {
        if (anim == null) {
            anim = GetComponent<Animator>();
        }
        anim.SetTrigger("close");
    }
}
