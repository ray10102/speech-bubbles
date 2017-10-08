using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class BubbleMove : MonoBehaviour {

	//	private Transform bubbleSelection = null;
	public GameObject bubblePrefab;
	private static GameObject bubbleSelect = null;

	// Adapted from UnityARHitTestExample
	public Transform m_HitBubbleTransform;

	bool HitBubblesWithResultType (ARPoint point, ARHitTestResultType resultTypes) {
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
				Debug.Log ("Got hit!");
				m_HitBubbleTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
				m_HitBubbleTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitBubbleTransform.position.x, m_HitBubbleTransform.position.y, m_HitBubbleTransform.position.z));
				return true;
			}
		}
		return false;
	}



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {

			bubbleSelect = null;
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				bubbleSelect = hit.transform.gameObject;

			}
			Debug.Log (bubbleSelect.transform.position);

		}
	}
}
