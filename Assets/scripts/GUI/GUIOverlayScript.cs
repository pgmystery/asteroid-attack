using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOverlayScript : MonoBehaviour
{
    public static GUIOverlayScript guiOverlayScript;

    void Awake() {
        if (guiOverlayScript == null) {
            // DontDestroyOnLoad(gameObject);
            guiOverlayScript = this;
        }
        else if (guiOverlayScript != this) {
            Destroy(gameObject);
        }
    }
}
