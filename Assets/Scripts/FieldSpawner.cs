using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class FieldSpawner : MonoBehaviour
{
    public enum FieldType { BUBBLE }
	public GameObject bubblePrefab;
    private UnityARCamera arCamera;
    private Animator anim;
    
    private static Bubble currentBubble;
    private static bool spawning;
    private static bool doneSpawning;
    private static bool isDown;

	void OnEnable() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
	}

	void OnDestroy() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
	}

    private void Start() {
        doneSpawning = true;
        isDown = true;
        // Starts not creating a bubble
        currentBubble = null;
        // Ghost bubble is on by default
        BubbleGhost.SetGhost(true);
        anim = GetComponent<Animator>();

        // spawn distance
        Bubble.SpawnDist = .5f;
        // starting scale
        Bubble.StartScale = .3f;
    }

    private void Update() {
        if (!doneSpawning) {
            currentBubble.Spawning();
        } else if (!isDown) {
            SlideDown();
        }
    }

    public void StartSpawn() {
        spawning = true;
        doneSpawning = false;
        SetGhost(false);
        SlideUp();
    }

    public void EndSpawn() {
        // Triggers bubble release animation, stops updating bubble location
        currentBubble.EndSpawn();
        spawning = false;
        currentBubble = null;
        doneSpawning = true;
        SetGhost(true);
        SlideDown();
    }

    public void ARFrameUpdated(UnityARCamera camera) {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(3, camera.worldTransform.column3);
        Vector3 bubblePosition = UnityARMatrixOps.GetPosition(matrix) + (Camera.main.transform.forward * Bubble.SpawnDist);
        if (spawning) {
            if (currentBubble == null) {
                currentBubble = Instantiate(bubblePrefab, bubblePosition, transform.rotation).GetComponentInChildren<Bubble>();
                currentBubble.StartSpawn();
            } else {
                currentBubble.SetPosition(bubblePosition);
            }
        }
    }

    // Slides the bubble wand down
    private void SlideDown() {
        if (anim != null) {
            anim = GetComponent<Animator>();
        }
        anim.SetTrigger("slideDown");
        isDown = true;
    }

    // Slides the bubble wand up
    private void SlideUp() {
        if (anim != null) {
            anim = GetComponent<Animator>();
        }
        anim.SetTrigger("slideUp");
        isDown = false;
    }

    // Sets the ghost on/off base on the given boolean
    private void SetGhost(bool ghostOn) {
        if (BubbleGhost.instance != null && BubbleGhost.isOn()) {
            BubbleGhost.instance.SetActive(ghostOn);
        }
    }
}

