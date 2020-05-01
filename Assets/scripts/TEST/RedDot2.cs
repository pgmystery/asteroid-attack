using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot2 : MonoBehaviour
{
    public static RedDot2 reddot;

    void Awake() {
        if (reddot == null) {
            // DontDestroyOnLoad(gameObject);
            reddot = this;
        }
        else if (reddot != this) {
            Destroy(gameObject);
        }
    }
}
