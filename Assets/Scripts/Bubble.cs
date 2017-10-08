using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class Bubble : MonoBehaviour {

    private AudioSource audioSource;
    private Animator anim;
    private BubbleWave wave;
    private Color color;
    private Renderer mesh;
    public static float scaleUnit;
    public List<Bubble> nextBubbles;
    public enum Color { NONE, RED, BLUE, GREEN, YELLOW, PURPLE };
    public Material none, red, blue, green, yellow, purple;
    public static Color selectedColor;

	// Use this for initialization
	void Start () {
        changeColor(selectedColor);
        mesh = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nextBubbles = new List<Bubble>();
        wave = GetComponentInChildren<BubbleWave>();
        if (wave) {
            wave.playSound();
        }
        scaleUnit = 1.1f;
        selectedColor = Color.RED;
	}

    private void OnTriggerEnter(Collider other) {
		Debug.Log("collided");
        if (other.gameObject.name == "CameraCollider") {
            playSound();
        }
    }

    private void OnTriggerExit(Collider other) {
		Debug.Log("out");
        if (other.gameObject.name == "CameraCollider") {
            stopSound();
        }
    }

    // BUBBLE ACTIONS

    public void releaseBubble() {
        if (!anim) {
            anim = GetComponent<Animator>();
        }
        anim.SetTrigger("release");
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        wave.stopSound();
    }

    public void SetScale(float scale) {
        if (scale > 0) {
            gameObject.transform.parent.localScale = new Vector3(scale, scale, scale);
        } else {
            gameObject.transform.parent.localScale = new Vector3(0, 0, 0);
        }
    }

    public void ScaleUp() {
        gameObject.transform.parent.localScale *= scaleUnit;
    }

    public void ScaleDown() {
        gameObject.transform.parent.localScale /= scaleUnit;
    }

    public void changeColor(Color color) {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (!mesh) {
            mesh = GetComponent<Renderer>();
        }
        wave.changeColor(color);
        switch (color) {
            case Color.NONE:
                mesh.material = none;
                break;
            case Color.RED:
                setMaterial(red);
                break;
            case Color.GREEN:
                setMaterial(green);
                break;
            case Color.BLUE:
                setMaterial(blue);
                break;
            case Color.YELLOW:
                setMaterial(yellow);
                break;
            case Color.PURPLE:
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

    public void addConnection(Bubble other) {
        this.nextBubbles.Add(other);
    }

    public void playSound() {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        if (nextBubbles.Count == 1) {
            audioSource.loop = false;
            audioSource.Play();
            Invoke("playNext", audioSource.clip.length);
            wave.playSound();
        } else {
            audioSource.loop = true;
            audioSource.Play();
            wave.playSound();
        }
    }

    public void stopSound() {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.Stop();
        wave.stopSound();
    }

    private void playNext() {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (nextBubbles.Count > 0) {
            nextBubbles[1].playSound();
            wave.stopSound();
        }
    }
}
