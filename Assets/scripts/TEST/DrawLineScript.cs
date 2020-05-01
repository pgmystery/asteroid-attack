using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    public static DrawLineScript drawLineScript;

    void Awake() {
        if (drawLineScript == null) {
            // DontDestroyOnLoad(gameObject);
            drawLineScript = this;
        }
        else if (drawLineScript != this) {
            Destroy(gameObject);
        }
    }
}
