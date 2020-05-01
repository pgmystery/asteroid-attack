using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// var touchPos : Vector2;
// var aTouch : boolean = false;
 
// if (Application.platform != RuntimePlatform.IPhonePlayer)
// {
//   // use the input stuff
//   aTouch = Input.GetMouseButton(0);
//   touchPos = Input.mousePosition;
// } else {
//   // use the iPhone Stuff
//   aTouch = (iPhoneInput.touchCount > 0);
//   touchPos = iPhoneInput.touches[0].position;
// }


public class asteroid_BACKUP : MonoBehaviour {

    // touch offset allows ball not to shake when it starts moving
    float deltaX, deltaY;

    // reference to Rigidbody2D component
    Rigidbody2D rb;

    // ball movement not allowed if you touches not the ball at the first time
    bool moveAllowed = false;

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody2D> ();

        // Add bouncy material to the ball
        // PhysicsMaterial2D mat = new PhysicsMaterial2D ();
        // mat.bounciness = 0.75f;
        // mat.friction = 0.4f;
        // GetComponent<CircleCollider2D> ().sharedMaterial = mat;

    }

    // Update is called one per frame
    void Update() {

        // Initiating touch event
        // if touch event takes place
        // if (Input.touchCount > 0) {
        if (CheckTouching()) {
            updateTouching();
        }
    }

    bool CheckTouching() {
        // check the platform
        if (Application.platform == RuntimePlatform.Android) {
            return (Input.touchCount > 0);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            return (Input.touchCount > 0);
        }
        else {
            return (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0));
        }
    }

    void updateTouching() {
        if (Application.platform == RuntimePlatform.Android) {
            updateTouchingAndroid();
        }
        else {
            updateTouchingElse();
        }
    }

    void updateTouchingAndroid() {
        // get touch to take a deal with
        Touch touch = Input.GetTouch (0);

        // obtain touch position
        Vector2 touchPos = Camera.main.ScreenToWorldPoint (touch.position);

        // processing touch phases
        switch (touch.phase)
        {
            // if you touches the screen
            case TouchPhase.Began:

                // if you touch the ball
                if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {

                    // get the offset between position you touches
                    // and the center of the game object
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;

                    // if touch befins within the asteroid collider
                    // then it is allowed to move
                    moveAllowed = true;

                    // restrict some rigidbody properties so it moves
                    // more smoothly and correctly
                    rb.freezeRotation = true;
                    rb.velocity = new Vector2 (0, 0);
                    rb.gravityScale = 0;
                    GetComponent<CircleCollider2D> ().sharedMaterial = null;
                }
                break;
            
            // you move you finger
            case TouchPhase.Moved:
                // if you touches the asteroid and movement is allowed then move
                if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos) && moveAllowed)
                    rb.MovePosition (new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
                break;

            // you release your finger
            case TouchPhase.Ended:
                //restore initial parameters
                // when touch is ended
                moveAllowed = false;
                rb.freezeRotation = false;
                PhysicsMaterial2D mat = new PhysicsMaterial2D ();
                mat.bounciness = 0.75f;
                mat.friction = 0.4f;
                GetComponent<CircleCollider2D> ().sharedMaterial = mat;
                break;
        }
    }

    void updateTouchingElse() {
        if (Input.GetMouseButtonUp(0) && moveAllowed) {
            moveAllowed = false;
            rb.freezeRotation = false;
        }
        else if (Input.GetMouseButton(0)) {
            // obtain touch position
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!moveAllowed && GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) {
                // set moveAllowed to true
                moveAllowed = true;

                // get the offset between position and your touchPos
                deltaX = touchPos.x - transform.position.x;
                deltaY = touchPos.y - transform.position.y;

                rb.freezeRotation = true;
                rb.velocity = new Vector2(0, 0);
                // GetComponent<CircleCollider2D>().sharedMaterial = null;
            }
            else if (moveAllowed) {
                rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
            }
        }
            
        //     // you move you finger
        //     case TouchPhase.Moved:
        //         // if you touches the asteroid and movement is allowed then move
        //         if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos) && moveAllowed)
        //             rb.MovePosition (new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
        //         break;

        //     // you release your finger
        //     case TouchPhase.Ended:
        //         //restore initial parameters
        //         // when touch is ended
        //         moveAllowed = false;
        //         rb.freezeRotation = false;
        //         PhysicsMaterial2D mat = new PhysicsMaterial2D ();
        //         mat.bounciness = 0.75f;
        //         mat.friction = 0.4f;
        //         GetComponent<CircleCollider2D> ().sharedMaterial = mat;
        //         break;
        // }
    }
}
