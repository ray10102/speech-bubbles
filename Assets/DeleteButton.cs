using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour {

    GameObject parent;

	public void DeleteAll() {
        foreach (Transform child in parent.transform) {
            child.GetComponent<Bubble>().resetBubble();
            child.gameObject.SetActive(false);
        }
    }
}
