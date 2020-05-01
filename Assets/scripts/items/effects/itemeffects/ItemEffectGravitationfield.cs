﻿using UnityEngine;

public class ItemEffectGravitationfield : MonoBehaviour
{
    public int effectTime;
    public float effectSize=3.0f;

    void Start() {
        Invoke("End", effectTime);
    }

    public void SetEffectSize(float newSize) {
        Vector3 currentScale = transform.localScale;
        currentScale.x = newSize;
        currentScale.y = newSize;
        transform.localScale = currentScale;
        effectSize = newSize;
    }

    private void OnTriggerEnter2D(Collider2D colliderObject) {
        if (colliderObject.GetComponent<Enemy>() != null) {
            Vector3 direction = colliderObject.transform.position - transform.position;
            colliderObject.attachedRigidbody.velocity = direction.normalized * colliderObject.attachedRigidbody.velocity.magnitude;
        }
    }

    private void End() {
        Destroy(gameObject);
    }
}
