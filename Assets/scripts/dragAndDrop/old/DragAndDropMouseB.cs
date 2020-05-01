using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropMouseB : MonoBehaviour
{
    Vector2 ray;
    Vector2 oldPosition;
    bool findSelection = false;
    List<TargetJoint2D> selectedObjects = new List<TargetJoint2D>();

    // void FixUpdate()
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            oldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ray = oldPosition;
            findSelection = true;
        }

        if (Input.GetMouseButton(0)) {
            if (findSelection == true) {
                ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(oldPosition, ray);
                Debug.DrawLine(oldPosition, ray);
                if (hits.Length > 0) {
                    for (int i = 0; i< hits.Length; i++) {
                        if (hits[i].transform.gameObject.GetComponent<dragable>() != null) {
                            TargetJoint2D selection = hits[i].transform.gameObject.GetComponent<TargetJoint2D>();
                            if (selection != null) {
                                if (!selectedObjects.Contains(selection)) {
                                    selection.enabled = true;
                                    selectedObjects.Add(selection);
                                }
                            }
                        }
                    }
                }
                if (selectedObjects.Count > 0) {
                    for (int i = 0; i < selectedObjects.Count; i++) {
                        selectedObjects[i].target = ray;
                    }
                }
                oldPosition = ray;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            // if (selection != null) {
                // CancelInvoke("LetGo");
                // LetGo();
            // CancelInvoke("LetGo");
            LetGo();
            // }
        }
    }

    // void LetGo() {
    //     if (selection != null) {
    //         selection.enabled = false;
    //         selection = null;
    //     }
    // }

    void LetGo() {
        if (findSelection == true) {
            findSelection = false;
            if (selectedObjects.Count > 0) {
                for (int i = 0; i < selectedObjects.Count; i++) {
                    selectedObjects[i].enabled = false;
                }
                selectedObjects.Clear();
            }
        }
    }
}
