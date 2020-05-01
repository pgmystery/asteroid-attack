using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://answers.unity.com/questions/1064394/onmousedown-and-mobile.html


public class asteroid : MonoBehaviour {

    // variables
    // private bool isPressed = false;
    private float deltaX, deltaY;

    // reference to Rigidbody2D component
    Rigidbody2D rb;

    // init touchPos
    Vector2 touchPos;

    // boolean if object allowed to move
    bool move = false;


    void Start() {
        rb = GetComponent<Rigidbody2D> ();
    }

    void Update() {
        // check if touches are exists
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase) {
                // init touch
                case TouchPhase.Began:
                    // check if touches the Object
                    // obtain touch position
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) {
                        // get the offset between position and your touchPos
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;

                        rb.freezeRotation = true;
                        rb.velocity = new Vector2(0, 0);

                        move = true;
                    }
                    break;

                // On Moving-Phase
                case TouchPhase.Moved:
                    if (move) {
                        // obtain touch position
                        touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        rb.MovePosition(new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
                    }
                    break;

                // on touch ended
                case TouchPhase.Ended:
                    if (move) {
                        move = false;
                        rb.freezeRotation = false;
                    }
                    break;
            }
        }
    }

    // void OnMouseDown() {
    //     // obtain touch position
    //     touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     // get the offset between position and your touchPos
    //     deltaX = touchPos.x - transform.position.x;
    //     deltaY = touchPos.y - transform.position.y;

    //     rb.freezeRotation = true;
    //     rb.velocity = new Vector2(0, 0);
    // }

    // void OnMouseUp() {
    //     rb.freezeRotation = false;
    // }

    // void OnMouseDrag() {
    //     // obtain touch position
    //     touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
    // }
}
