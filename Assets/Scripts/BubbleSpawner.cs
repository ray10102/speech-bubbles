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
    private float timeStart;

	void OnEnable() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
	}

	void OnDestroy() {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
	}

    private void Start() {
        // Starts not creating a bubble
        currentBubble = null;
    }

    private void Update() {
        if (currentBubble) {
            currentBubble.transform.localScale += (new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime);
        }
    }

    private Vector3 GetCameraPosition ()
	{
		Matrix4x4 matrix = new Matrix4x4 ();
		matrix.SetColumn (3, arCamera.worldTransform.column3);
		return UnityARMatrixOps.GetPosition (matrix);
	}

    public void StartSpawn() {
        currentBubble = Instantiate(bubblePrefab, getSpawnPosition(), transform.rotation) as GameObject;
        currentBubble.transform.parent = bubbleParent;
        MicrophoneInput.StartRecord(currentBubble.GetComponentInChildren<AudioSource>());
        timeStart = Time.time;
    }

    public void EndSpawn() {
        // Triggers bubble release animation, stops updating bubble location
        currentBubble.GetComponentInChildren<Bubble>().releaseBubble();
        MicrophoneInput.StopRecord();
        currentBubble = null;
    }

    private void ARFrameUpdated(UnityARCamera arCamera) {
        if (currentBubble) {
            Debug.Log("spawning - current spawn location: " + getSpawnPosition());
            Debug.Log("spawning - camera position: " + GetCameraPosition());
            currentBubble.transform.position = getSpawnPosition();
        }
    }

    private Vector3 getSpawnPosition() {
        return GetCameraPosition() + (Camera.main.transform.forward * 2f);
    }
}

