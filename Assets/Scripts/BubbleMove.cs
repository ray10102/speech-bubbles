using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class BubbleMove : MonoBehaviour {

	//	private Transform bubbleSelection = null;
	public GameObject bubblePrefab;
	private static GameObject bubbleSelect = null;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {

			bubbleSelect = null;
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				bubbleSelect = hit.transform.gameObject;
				Destroy (bubbleSelect);
			}
			Debug.Log (bubbleSelect.transform.position);

		}
	}
}
