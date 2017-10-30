using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class Bubble : ASoundField {

    // Private fields
    protected BubbleWave wave;
    protected List<Bubble> nextBubbles;
    protected Bubble currentlyPlaying;

    // Statics
    public static Transform Parent;
    public static float SpawnDist;
    public static float SpawnScaleRate;
    public static float ScaleFactor;
    public static float StartScale;

    // Calls ASoundField's Start() and initializes bubble specific fields.
    protected override void Start () {
        base.Start();
        nextBubbles = new List<Bubble>();
        wave = GetComponentInChildren<BubbleWave>();
        if (wave) {
            wave.playSound();
        } else {
            Debug.LogError("Bubble's wave not found");
        }
        ScaleFactor = 1.1f;
        currentlyPlaying = null;
	}

    // BUBBLE ACTIONS

    public override void StartSpawn() {
        transform.parent.localScale = new Vector3(StartScale, StartScale, StartScale);
        transform.parent.parent = Parent;
        MicrophoneInput.StartRecord(audioSource);
    }

    // Keep increasing size while spawning
    public override void Spawning() {
        gameObject.transform.localScale += (new Vector3(SpawnScaleRate, SpawnScaleRate, SpawnScaleRate) * Time.deltaTime);
    }

    public override void EndSpawn() {
        if (!anim) {
            anim = GetComponent<Animator>();
        }
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }

        anim.SetTrigger("release");
        wave.stopSound();
        MicrophoneInput.StopRecord();
    }

    private void setScale(float scale) {
        if (scale > 0) {
            gameObject.transform.parent.localScale = new Vector3(scale, scale, scale);
        } else {
            Debug.LogWarning("Attempted to set negative scale.");
            gameObject.transform.parent.localScale = new Vector3(0, 0, 0);
        }
    }

    private void scaleUp() {
        gameObject.transform.parent.localScale *= ScaleFactor;
    }

    private void scaleDown() {
        gameObject.transform.parent.localScale /= ScaleFactor;
    }

    public override void SetColor(Color color) {
        base.SetColor(color);
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        wave.changeColor(color);
    }

    public void addConnection(Bubble other) {
        this.nextBubbles.Add(other);
    }

    public override void OnCollideWithUser() {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        if (nextBubbles.Count == 1) {
            Debug.Log("Playing sound 1 next");
            audioSource.loop = false;
            audioSource.Play();
            Invoke("playNext", audioSource.clip.length);
            wave.playSound();
        } else {
            Debug.Log("Playing sound ");
            audioSource.loop = true;
            audioSource.Play();
            wave.playSound();
        }
        currentlyPlaying = this;
    }

    public override void OnExitWithUser() {
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.Stop();
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        wave.stopSound();
    }

    private void playNext() {
        if (!wave) {
            wave = GetComponentInChildren<BubbleWave>();
        }
        if (nextBubbles.Count > 0) {
            nextBubbles[1].OnCollideWithUser();
            wave.stopSound();
        } else {
            throw new InvalidOperationException("Cannot play the next bubble when there is no next bubble");
        }
    }

    public void SetPosition(Vector3 pos) {
        transform.parent.position = pos;
    }
}
