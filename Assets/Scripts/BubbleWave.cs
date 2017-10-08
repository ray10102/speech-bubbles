using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWave : MonoBehaviour {

    private Animator anim;
    private Renderer mesh;
    public Material none, red, green, blue, yellow, purple;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        mesh = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSound() {
        if (!anim) {
            anim = GetComponent<Animator>();
        }
        anim.SetBool("isPlaying", true);
    }

    public void stopSound() {
        if (!anim) {
            anim = GetComponent<Animator>();
        }
        anim.SetBool("isPlaying", false);
    }

    public void changeColor(Bubble.Color color) {
        if(!mesh) {
            mesh = GetComponent<Renderer>();
        }
        switch (color) {
            case Bubble.Color.NONE:
                mesh.material = none;
                break;
            case Bubble.Color.RED:
                setMaterial(red);
                break;
            case Bubble.Color.GREEN:
                setMaterial(green);
                break;
            case Bubble.Color.BLUE:
                setMaterial(blue);
                break;
            case Bubble.Color.YELLOW:
                setMaterial(yellow);
                break;
            case Bubble.Color.PURPLE:
                setMaterial(purple);
                break;
        }
    }

    private void setMaterial(Material mat) {
        if (!mesh) {
            mesh = GetComponent<Renderer>();
        }
        if (mat != null) {
            mesh.material = mat;
        } else {
            mesh.material = none;
        }
    }

}
