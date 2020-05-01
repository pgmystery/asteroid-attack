using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropTouch : MonoBehaviour
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

    void Start() {
        ingameHandler = IngameHandler.ingameHandler;
        itemMenuIndicator = ItemMenuIndicator.itemMenuIndicator;

        linePoints = new Vector3[2];
        lineCollider = LineCollider.lineCollider.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    // void FixedUpdate()
    {
        Touch[] touch = Input.touches;
        for (int i=0; i < touch.Length; i++) {
            switch (touch[i].phase) {
                case TouchPhase.Began:
                    if (!ingameHandler.dead && !itemMenuIndicator.pointerOverItemMenu) {
                        ray = Camera.main.ScreenToWorldPoint(touch[i].position);
                        CameraHandler.cameraHandler.InputPosition = ray;
                        startPoint = ray;
                        linePoints[0] = ray;
                        lineCollider.transform.position = ray;
                        LineCollider.lineCollider.findSelection = true;
                        selection = true;
                        reddot.transform.position = ray;
                        reddot2.transform.position = ray;
                        Invoke("LetGo", 0.5f);
                    }
                    break;
                case TouchPhase.Moved:
                    if (!ingameHandler.dead) {
                        if (selection == true) {
                            ray = Camera.main.ScreenToWorldPoint(touch[i].position);
                            CameraHandler.cameraHandler.InputPosition = ray;
                            linePoints[1] = ray;
                            lineCollider.transform.position = Vector2.Lerp(ray, startPoint, .5f);
                            Vector2 difference = ray - new Vector2(lineCollider.transform.position.x, lineCollider.transform.position.y);
                            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                            lineCollider.transform.rotation = Quaternion.Euler(.0f, .0f, rotationZ);
                            lineCollider.size = new Vector2(Mathf.Max(Vector2.Distance(startPoint, ray), .2f), lineCollider.size.y);
                            startPoint = ray;  // ???

                            reddot2.transform.position = ray;
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    CancelInvoke("LetGo");
                    LetGo();
                    break;
            }
        }
    }

    void LetGo() {
        CameraHandler.cameraHandler.InputPosition = new Vector2();
        if (selection == true) {
            LineCollider.lineCollider.ClearObjects();
            selection = false;
        }
    }
}
