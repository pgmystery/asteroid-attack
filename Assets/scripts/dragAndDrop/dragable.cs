using UnityEngine;
using System.Collections;

public class dragable : MonoBehaviour
{
    void Start() {
        GetComponent<TargetJoint2D>().enabled = false;
    }
}
