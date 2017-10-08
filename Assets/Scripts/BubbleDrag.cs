using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class BubbleDrag : MonoBehaviour {

	public LayerMask touchInputMask;
	public GameObject bubblePrefab;
	private static GameObject bubbleSelect = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Touch touch in Input.touches) {
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			RaycastHit hit;

//			if (Physics.Raycast (ray, out hit, 100)) {
			if (Physics.Raycast (ray, out hit, touchInputMask)) {
				GameObject bubbleObj = hit.transform.gameObject;
			}
		}
	}
}
