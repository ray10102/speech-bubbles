using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class Bubble : MonoBehaviour {

    private AudioSource audioSource;
    private Animator anim;
    private BubbleWave wave;
    private Color color;
    private MeshRenderer mesh;
    public static float scaleUnit;
    public List<Bubble> nextBubbles;
    public enum Color { NONE, RED, BLUE, GREEN, YELLOW };
    public Material none, red, blue, green, yellow;
    public static Color selectedColor;

	// Use this for initialization
	void Start () {
        changeColor(selectedColor);
        mesh = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nextBubbles = new List<Bubble>();
        wave = GetComponentInChildren<BubbleWave>();
        scaleUnit = 1.1f;
	}

    private void OnTriggerEnter(Collider other) {
		Debug.Log("collided");
        if (other.gameObject.name == "CameraCollider") {
            stopSound();
        }
    }

    private void OnTriggerExit(Collider other) {
		Debug.Log("out");
        if (other.gameObject.name == "CameraCollider") {
            playSound();
        }
    }

    // BUBBLE ACTIONS

    public void releaseBubble() {
        anim.SetTrigger("release");
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
        switch (color) {
            case Color.NONE:
                mesh.material = none;
                break;
            case Color.RED:
                mesh.material = red;
                break;
            case Color.GREEN:
                mesh.material = green;
                break;
            case Color.BLUE:
                mesh.material = blue;
                break;
            case Color.YELLOW:
                mesh.material = yellow;
                break;
        }
    }

    public void addConnection(Bubble other) {
        this.nextBubbles.Add(other);
    }

    public void playSound() {
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
        audioSource.Stop();
        wave.stopSound();
    }

    private void playNext() {
        nextBubbles[1].playSound();
        wave.stopSound();
    }
}
