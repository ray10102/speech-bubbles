using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class BubbleMove : MonoBehaviour {

	//	private Transform bubbleSelection = null;
	public GameObject bubblePrefab;
	private static GameObject bubbleSelect = null;
//	public float speed;
//	float step = speed * Time.deltaTime;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			bubbleSelect = null;
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 100)) {
					bubbleSelect = hit.transform.gameObject;
					Debug.Log ("Bubble selected" + bubbleSelect);
					Debug.Log ("Bubble pos" + bubbleSelect.transform.position);
				}
			}

			Debug.Log ("Touch Position" + touch.position);
			if (touch.phase == TouchPhase.Moved) {
				Vector3 fingerPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 10.0f));
				Debug.Log ("Finger Position" + fingerPosition);
//				bubbleSelect.transform.position = Vector3.MoveTowards (bubbleSelect.transform.position, fingerPosition, 2 * Time.deltaTime);
//				bubbleSelect.transform.position = fingerPosition;
				bubbleSelect.transform.position = new Vector3(fingerPosition.x, fingerPosition.y, fingerPosition.z);
				Debug.Log ("Bubble pos2" + bubbleSelect.transform.position);

//				bubbleSelect.transform.position.y = Vector3.MoveTowards(bubbleSelect.position.y, fingerPosition.position.y, step);
			}
		}

//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
//
//			bubbleSelect = null;
//			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
//			RaycastHit hit;
//
//			if (Physics.Raycast (ray, out hit, 100)) {
//				bubbleSelect = hit.transform.gameObject;
//				Destroy (bubbleSelect);
//			}
//			Debug.Log (bubbleSelect.transform.position);
//
//		}
	}
}
