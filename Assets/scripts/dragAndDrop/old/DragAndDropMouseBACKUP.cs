using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropMouseBACKUP : MonoBehaviour
{
    public GameObject reddot;
    public GameObject reddot2;
    // public TargetJoint2D selection;
    public bool reddotSelection = false;

    Vector2 reddotOldPosition;
    Vector2 ray;
    Vector2 startPoint;
    Rigidbody2D reddotRigidbody;
    RedDotDragAndDrop redDotDragAndDrop;
    // RaycastHit2D hit;
    Vector3[] linePoints;
    CapsuleCollider2D lineCollider;

    void Start() {
        reddotRigidbody = reddot.GetComponent<Rigidbody2D>();
        redDotDragAndDrop = reddot.GetComponent<RedDotDragAndDrop>();
        linePoints = new Vector3[2];
        lineCollider = LineCollider.lineCollider.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    // void FixUpdate()
    {
        if (Input.GetMouseButtonDown(0)) {
            ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint = ray;
            linePoints[0] = ray;
            // RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            // GameObject.Find("reddot").transform.position = new Vector3(ray.x, ray.y, .0f);
            // reddot.transform.position = new Vector3(ray.x, ray.y, .0f);
            // reddotOldPosition = ray;
            reddotRigidbody.position = ray;
            // reddotRigidbody.MovePosition(ray);
            lineCollider.transform.position = ray;
            // redDotDragAndDrop.findSelection = true;
            LineCollider.lineCollider.findSelection = true;

            // if (hit) {
            //     if (hit.transform.gameObject.GetComponent<dragable>() != null) {
            //         selection = hit.transform.gameObject.GetComponent<TargetJoint2D>();
            //         if (selection != null) {
            //             selection.enabled = true;
            //             Invoke("LetGo", 0.5f);
            //         }
            //     }
            // }

            reddotSelection = true;
            // Invoke("LetGoReddot", 0.5f);
            reddot2.transform.position = ray;
        }

        if (Input.GetMouseButton(0)) {
            if (reddotSelection == true) {
                ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Vector2 direction = new Vector2(reddotOldPosition.x - ray.x, reddotOldPosition.y - ray.y);
                // RaycastHit2D[] hits = Physics2D.RaycastAll(reddotOldPosition, ray);
                // if (hits.Length > 0) {
                //     for (int i = 0; i< hits.Length; i++) {
                //         Debug.Log(hits[i].transform.gameObject);
                //         if (hits[i].transform.gameObject.GetComponent<dragable>() != null) {
                //             Debug.Log("SET_POS");
                //             TargetJoint2D selection = hits[i].transform.gameObject.GetComponent<TargetJoint2D>();
                //             selection.enabled = true;
                //             selection.target = ray;
                //         }
                //     }
                // }
                // reddot.transform.position = new Vector3(ray.x, ray.y, .0f);
                reddotRigidbody.MovePosition(ray);
                // Debug.Log(ray);
                linePoints[1] = ray;
                // DrawLine.LineTest.GetComponent<LineRenderer>().SetPositions(linePoints);
                // lineCollider.transform.LookAt(ray);
                lineCollider.transform.position = Vector2.Lerp(ray, startPoint, .5f);
                Vector2 difference = ray - new Vector2(lineCollider.transform.position.x, lineCollider.transform.position.y);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                lineCollider.transform.rotation = Quaternion.Euler(.0f, .0f, rotationZ);
                // lineCollider.size = new Vector2(lineCollider.transform.position.x - ray.x, lineCollider.size.y);
                lineCollider.size = new Vector2(Mathf.Max(Vector2.Distance(startPoint, ray), .2f), lineCollider.size.y);

                startPoint = ray;  // ???
            }
            // reddotOldPosition = ray;
            // if (selection != null) {
            //     selection.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // }
        }

        if (Input.GetMouseButtonUp(0)) {
            // if (selection != null) {
                // CancelInvoke("LetGo");
                // LetGo();
            CancelInvoke("LetGoReddot");
            LetGoReddot();
            // }
        }
    }

    // void LetGo() {
    //     if (selection != null) {
    //         selection.enabled = false;
    //         selection = null;
    //     }
    // }

    void LetGoReddot() {
        if (reddotSelection == true) {
            // redDotDragAndDrop.ClearObjects();
            LineCollider.lineCollider.ClearObjects();
            reddotSelection = false;
        }
    }
}
