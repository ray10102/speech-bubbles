using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GhostBubble : MonoBehaviour {

    void OnEnable() {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
    }

    void OnDestroy() {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
    }

    public void ARFrameUpdated(UnityARCamera camera) {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(3, camera.worldTransform.column3);
        transform.position = UnityARMatrixOps.GetPosition(matrix) + (Camera.main.transform.forward * BubbleSpawner.spawnDist);
    }
}
