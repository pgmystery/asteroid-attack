using UnityEngine;

public class ItemEffectSimplerocket : MonoBehaviour
{
    public float effectSize=1f;
    public float speed;
    public int health;
    public int damage;

    public Vector3 toPosition;

    private bool moving=false;
    private Vector3 speedVector;

    public void SetEffectSize(float newSize) {
        Vector3 currentScale = transform.localScale;
        currentScale.x = newSize;
        currentScale.y = newSize;
        transform.localScale = currentScale;
        effectSize = newSize;
    }

    public void StartMoving(Vector3 targetPosition) {
        toPosition = targetPosition;
        toPosition.z = 0f;
        float distance = Vector3.Distance(transform.position, toPosition);
        speedVector = Vector3.LerpUnclamped(transform.position, toPosition, speed / distance);
        speedVector.z = .0f;
        moving = true;
    }

    void Update() {
        if (moving) {
            // MOVE ROCKET:
            transform.Translate(speedVector * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderObject) {
        if (colliderObject.GetComponent<Enemy>() != null) {
            Enemy enemyCollider = colliderObject.GetComponent<Enemy>();
            GetDamage(enemyCollider.damage);
            enemyCollider.GetDamage(damage);
        }
    }

    public void GetDamage(int damageNumber) {
        health -= damageNumber;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
