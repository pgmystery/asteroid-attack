using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDotDragAndDrop : MonoBehaviour
{
    public bool findSelection = false;
    public List<TargetJoint2D> dragObjects = new List<TargetJoint2D>();

    // void OnTriggerEnter2D(Collider2D collider) {
    //     // Debug.Log("COLLIDED");
    //     if (findSelection == true) {
    //         if (collider.gameObject.GetComponent<dragable>() != null) {
    //             TargetJoint2D selection = collider.gameObject.GetComponent<TargetJoint2D>();
    //             if (selection != null) {
    //                 if (!dragObjects.Contains(selection)) {
    //                     selection.enabled = true;
    //                     dragObjects.Add(selection);
    //                 }
    //             }
    //         }
    //     }
    // }

    // // void OnCollisionEnter2D(Collision2D collision) {
    // //     Debug.Log("COLLIDED 2");
    // // }

    // public void ClearObjects() {
    //     // findSelection = false;
    //     // dragObjects.Clear();
    //     for (int i = 0; i < dragObjects.Count; i++) {
    //         dragObjects[i].enabled = false;
    //         dragObjects.Remove(dragObjects[i]);
    //     }
    // }

    // // void Update() {
    // void Update() {
    //     if (dragObjects.Count > 0) {
    //         for (int i = 0; i < dragObjects.Count; i++) {
    //             if (dragObjects[i] != null) {
    //                 dragObjects[i].target = gameObject.transform.position;
    //             }
    //             else {
    //                 dragObjects.Remove(dragObjects[i]);
    //             }
    //         }
    //     }
    // }
}
