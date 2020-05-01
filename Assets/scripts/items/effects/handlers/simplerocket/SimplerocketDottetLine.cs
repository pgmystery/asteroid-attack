using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplerocketDottetLine : MonoBehaviour
{
    public Vector2 selfPosition=Vector2.zero;
    public Vector2 targetPosition=Vector2.zero;

    void Update() {
        DottedLine.DottedLine.Instance.DrawDottedLine(selfPosition, targetPosition);
    }
}
