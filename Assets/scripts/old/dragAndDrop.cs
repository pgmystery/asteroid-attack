using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragAndDrop : MonoBehaviour
{

    float tempZAxis;
    // public SpriteRenderer selection;
    public Rigidbody2D selection;
    void Update()
    {
        Touch[] touch = Input.touches;
        for (int i = 0; i < touch.Length; i++)
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            // Debug.Log(hit);
            switch (touch[i].phase)
            {
                case TouchPhase.Began:
                    if (hit)
                    {
                        // selection = hit.transform.gameObject.GetComponent<SpriteRenderer>();
                        selection = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                        if (selection != null)
                        {
                            tempZAxis = selection.transform.position.z;
                            selection.freezeRotation = true;
                            selection.velocity = new Vector2(0, 0);
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    Vector3 tempVec = Camera.main.ScreenToWorldPoint(touch[i].position);
                    tempVec.z = tempZAxis; //Make sure that the z zxis never change
                    if (selection != null)
                    {
                        // selection.transform.position = tempVec;
                        selection.MovePosition(tempVec);
                    }
                    break;
                case TouchPhase.Ended:
                    selection.freezeRotation = false;
                    selection = null;
                    break;
            }

        }
    }

}
