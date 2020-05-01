using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public static LineCollider lineCollider;

    public bool findSelection = false;
    public List<TargetJoint2D> dragObjects = new List<TargetJoint2D>();

    void Awake() {
        if (lineCollider == null) {
            // DontDestroyOnLoad(gameObject);
            lineCollider = this;
        }
        else if (lineCollider != this) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Debug.Log("COLLIDED");
        if (findSelection == true) {
            if (collider.gameObject.GetComponent<dragable>() != null) {
                TargetJoint2D selection = collider.gameObject.GetComponent<TargetJoint2D>();
                if (selection != null) {
                    if (!dragObjects.Contains(selection)) {
                        selection.enabled = true;
                        dragObjects.Add(selection);
                    }
                }
            }
        }
    }
    public void ClearObjects() {
        findSelection = false;
        for (int i = 0; i < dragObjects.Count; i++) {
            if (dragObjects[i] != null) {
                dragObjects[i].enabled = false;
                // dragObjects.Remove(dragObjects[i]);
            }
        }
        dragObjects.Clear();
    }

    // void Update() {
    void Update() {
        if (dragObjects.Count > 0) {
            for (int i = 0; i < dragObjects.Count; i++) {
                if (dragObjects[i] != null) {
                    dragObjects[i].target = CameraHandler.cameraHandler.InputPosition;
                }
                else {
                    dragObjects.Remove(dragObjects[i]);
                }
            }
        }
    }
}
