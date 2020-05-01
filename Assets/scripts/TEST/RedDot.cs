using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot : MonoBehaviour
{
    public static RedDot reddot;

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
