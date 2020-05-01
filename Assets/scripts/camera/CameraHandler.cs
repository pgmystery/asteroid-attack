using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler cameraHandler;

    public Vector2 InputPosition;

    void Awake() {
        if (cameraHandler == null) {
            // DontDestroyOnLoad(gameObject);
            cameraHandler = this;
        }
        else if (cameraHandler != this) {
            Destroy(gameObject);
        }
    }
}
