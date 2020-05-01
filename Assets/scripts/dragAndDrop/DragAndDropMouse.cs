using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropMouse : MonoBehaviour
{
    public GameObject reddot;
    public GameObject reddot2;

    bool selection = false;
    Vector2 ray;
    Vector2 startPoint;
    Vector3[] linePoints;
    CapsuleCollider2D lineCollider;

    private IngameHandler ingameHandler;
    private ItemMenuIndicator itemMenuIndicator;

    Vector3[] drawLinePoints;

    void Start() {
        ingameHandler = IngameHandler.ingameHandler;
        itemMenuIndicator = ItemMenuIndicator.itemMenuIndicator;

        drawLinePoints = new Vector3[2];
        linePoints = new Vector3[2];
        lineCollider = LineCollider.lineCollider.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    // void FixUpdate()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!ingameHandler.dead && !itemMenuIndicator.pointerOverItemMenu) {
                ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CameraHandler.cameraHandler.InputPosition = ray;
                startPoint = ray;
                linePoints[0] = ray;
                lineCollider.transform.position = ray;
                LineCollider.lineCollider.findSelection = true;
                selection = true;
                reddot.transform.position = ray;
                reddot2.transform.position = ray;
                Invoke("LetGo", 0.5f);
                drawLinePoints[0] = ray;
            }
        }

        if (Input.GetMouseButton(0)) {
            if (!ingameHandler.dead) {
                if (selection == true) {
                    ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CameraHandler.cameraHandler.InputPosition = ray;
                    linePoints[1] = ray;
                    lineCollider.transform.position = Vector2.Lerp(ray, startPoint, .5f);
                    Vector2 difference = ray - new Vector2(lineCollider.transform.position.x, lineCollider.transform.position.y);
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    lineCollider.transform.rotation = Quaternion.Euler(.0f, .0f, rotationZ);
                    lineCollider.size = new Vector2(Mathf.Max(Vector2.Distance(startPoint, ray), .2f), lineCollider.size.y);
                    startPoint = ray;  // ???

                    reddot2.transform.position = ray;
                    drawLinePoints[1] = ray;
                    DrawLineScript.drawLineScript.GetComponent<LineRenderer>().SetPositions(drawLinePoints);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            CancelInvoke("LetGo");
            LetGo();
        }
    }

    void LetGo() {
        CameraHandler.cameraHandler.InputPosition = new Vector2();
        if (selection == true) {
            LineCollider.lineCollider.ClearObjects();
            selection = false;
        }
        // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // Debug.Log(lineCollider.transform.position);
        // Debug.Log("");
        // Debug.Log("");
    }
}
