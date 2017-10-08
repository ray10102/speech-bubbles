using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.iOS;

public class MoveScript : MonoBehaviour {

	//	private Transform bubbleSelection = null;
    private static GameObject bubbleSelect;
    private Vector3 fixedPos;

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
				}
                fixedPos = Camera.main.WorldToViewportPoint(bubbleSelect.transform.position);
            }
			if (touch.phase == TouchPhase.Moved) {
                transform.position = Camera.main.ViewportToWorldPoint(fixedPos);
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
