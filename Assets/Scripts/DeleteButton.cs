using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour {
    public GameObject parent;

    public void ClearAll() {
        foreach(Transform child in parent.transform) {
            Destroy(child);
        }
    }
}
