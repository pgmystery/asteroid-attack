using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
    public float startZoom = 8.266245f;
    public float maxZoomIn = 3.5f;
    public float maxZoomOut = 30.0f;
    public float orthoZoomSpeed = .1f;
    public float perspectiveZoomSpeed = .1f;

    Vector3 reddotSize;
    CapsuleCollider2D lineCollider;

    void Start() {
        // reddotSize = RedDot.reddot.GetComponent<Renderer>().bounds.size;
        reddotSize = RedDot.reddot.transform.localScale;
        // Debug.Log(reddotSize);
        lineCollider = LineCollider.lineCollider.GetComponent<CapsuleCollider2D>();
        if(GetComponent<Camera>().orthographic) {
            GetComponent<Camera>().orthographicSize = startZoom;
        }
        else {
            GetComponent<Camera>().fieldOfView = startZoom;
        }
    }

    void Update() {
    // Touch
        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if(GetComponent<Camera>().orthographic) {
                float CameraZoomLevel = GetComponent<Camera>().orthographicSize;
                float newZoomLevel = deltaMagnitudeDiff * orthoZoomSpeed;
                if (CameraZoomLevel + newZoomLevel <= maxZoomIn) {
                    GetComponent<Camera>().orthographicSize = maxZoomIn;
                }
                else if (CameraZoomLevel + newZoomLevel >= maxZoomOut) {
                    GetComponent<Camera>().orthographicSize = maxZoomOut;
                }
                else {
                    GetComponent<Camera>().orthographicSize += newZoomLevel;
                }
                // GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                // GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, .1f);
                float newReddotSize = GetComponent<Camera>().orthographicSize / startZoom;
                RedDot.reddot.transform.localScale = new Vector3(newReddotSize, newReddotSize, 1.0f);
                lineCollider.size = new Vector2(lineCollider.size.x, newReddotSize);
            }
            else {
                float CameraZoomLevel = GetComponent<Camera>().fieldOfView;
                float newZoomLevel = deltaMagnitudeDiff * perspectiveZoomSpeed;
                if (CameraZoomLevel + newZoomLevel <= maxZoomIn) {
                    GetComponent<Camera>().fieldOfView = maxZoomIn;
                }
                else if (CameraZoomLevel + newZoomLevel >= maxZoomOut) {
                    GetComponent<Camera>().fieldOfView = maxZoomOut;
                }
                else {
                    GetComponent<Camera>().fieldOfView += newZoomLevel;
                }
                // GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                // GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, .1f, 179.9f);
                float newReddotSize = GetComponent<Camera>().fieldOfView / startZoom;
                RedDot.reddot.transform.localScale = new Vector3(newReddotSize, newReddotSize, 1.0f);
                lineCollider.size = new Vector2(lineCollider.size.x, newReddotSize);
            }
        }

    // MouseWheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {  // ZoomIn
            float CameraZoomLevel = GetComponent<Camera>().orthographicSize;
            if (CameraZoomLevel != maxZoomIn) {
                if (CameraZoomLevel - 1 <= maxZoomIn) {
                    GetComponent<Camera>().orthographicSize = maxZoomIn;
                }
                else {
                    GetComponent<Camera>().orthographicSize--;
                }
            }
            float newReddotSize = GetComponent<Camera>().orthographicSize / startZoom;
            RedDot.reddot.transform.localScale = new Vector3(newReddotSize, newReddotSize, 1.0f);
            lineCollider.size = new Vector2(lineCollider.size.x, newReddotSize);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {  // ZoomOut
            float CameraZoomLevel = GetComponent<Camera>().orthographicSize;
            if (CameraZoomLevel != maxZoomOut) {
                if (CameraZoomLevel + 1 >= maxZoomOut) {
                    GetComponent<Camera>().orthographicSize = maxZoomOut;
                }
                else {
                    GetComponent<Camera>().orthographicSize++;
                }
                // Debug.Log(GetComponent<Camera>().scaledPixelWidth);
                // Debug.Log(GetComponent<Camera>().scaledPixelHeight);
                // Debug.Log("");
            }
            float newReddotSize = GetComponent<Camera>().orthographicSize / startZoom;
            // Debug.Log(RedDot.reddot);
            RedDot.reddot.transform.localScale = new Vector3(newReddotSize, newReddotSize, 1.0f);  // TODO: GOT AN ERROR ONCE ?!
            lineCollider.size = new Vector2(lineCollider.size.x, newReddotSize);
        }
    }

    // void Start() {
    //     Debug.Log(GetComponent<Camera>().orthographicSize);
    //     Debug.Log(GetComponent<Camera>().scaledPixelWidth);
    //     Debug.Log(GetComponent<Camera>().scaledPixelHeight);
    //     Debug.Log("");
    // }
}
