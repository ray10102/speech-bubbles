using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class BubbleSpawner : MonoBehaviour
{
    public float scaleSpeed;
	public GameObject bubblePrefab;
    public Transform bubbleParent;
    private static GameObject currentBubble;
    private UnityARCamera arCamera;
    public static float spawnDist;
    private bool spawning;
    private bool done;

	void OnEnable() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
	}

	void OnDestroy() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
	}

    private void Start() {
        done = true;
        // Starts not creating a bubble
        currentBubble = null;
        spawnDist = 1f;
    }

    private void Update() {
        if (!done) {
            currentBubble.transform.localScale += (new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime);
        }
    }

    public void StartSpawn() {
        spawning = true;
        done = false;
    }

    public void EndSpawn() {
        // Triggers bubble release animation, stops updating bubble location
        currentBubble.GetComponentInChildren<Bubble>().releaseBubble();
        MicrophoneInput.StopRecord();
        currentBubble = null;
        done = true;
    }

    public void ARFrameUpdated(UnityARCamera camera) {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(3, camera.worldTransform.column3);
        Vector3 bubblePosition = UnityARMatrixOps.GetPosition(matrix) + (Camera.main.transform.forward * BubbleSpawner.spawnDist);
        if (spawning) {
            if (currentBubble == null) {
                currentBubble = Instantiate(bubblePrefab, bubblePosition, transform.rotation) as GameObject;
                currentBubble.transform.parent = bubbleParent;
                MicrophoneInput.StartRecord(currentBubble.GetComponentInChildren<AudioSource>());
            } else {
                currentBubble.transform.position = bubblePosition;
            }
        }
    }
}

