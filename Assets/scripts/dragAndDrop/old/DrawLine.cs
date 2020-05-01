using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public static DrawLine LineTest;

    void Awake() {
        if (LineTest == null) {
            DontDestroyOnLoad(gameObject);
            LineTest = this;
        }
        else if (LineTest != this) {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LineRenderer>().positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
