using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class BubbleGhost : MonoBehaviour {

    private static bool ghostOn;
    public static GameObject instance;

    private void Awake() {
        if (instance == null) {
            instance = this.gameObject;
        } else {
            Debug.Log("Destroying this duplicate GhostBubble");
            Destroy(this);
        }
    }

    void OnEnable() {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
    }

    void OnDestroy() {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
    }

    void Start() {
        transform.localScale = new Vector3(Bubble.StartScale, Bubble.StartScale, Bubble.StartScale);
    }

    public void ARFrameUpdated(UnityARCamera camera) {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(3, camera.worldTransform.column3);
        transform.position = UnityARMatrixOps.GetPosition(matrix) + (Camera.main.transform.forward * Bubble.SpawnDist);
    }

    public static void ToggleGhost() {
        ghostOn = !ghostOn;
    }

    public static void SetGhost(bool ghostOn) {
        BubbleGhost.ghostOn = ghostOn;
    }

    public static bool isOn() {
        return ghostOn;
    }

    public void ToggleGhostButton() {
        ToggleGhost();
        BubbleGhost.instance.SetActive(BubbleGhost.isOn());
    }
}
