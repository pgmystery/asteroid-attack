using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable]
// public class EnemyDestroyEvent<object[1]> : EventManagerEvent {}

public class Enemy : MonoBehaviour {
    public int type = 0;
    public int damage = 10;
    public int health = 10;
    public bool damageable = true;
    public int points = 10;
    // public EventManagerEvent destroyEvent;

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.GetComponent<Enemy>() != null) {
            if (damageable == true) {
                GetDamage(collider.gameObject.GetComponent<Enemy>().damage);
            }
        }
    }

    public void GetDamage(int damageNumber) {
        health -= damageNumber;
        if (health <= 0) {
            Destroy(gameObject);
            IngameHandler.ingameHandler.AddPoints(points);
            // UnityEvent<GameObject> enemyDestroyEvent = EventManager.GetEvent("EnemyDestroy");
            EventManager.Trigger("enemyDestroy", new object[1] {gameObject});
            // enemyDestroyEvent.Invoke();
            // destroyEvent.Invoke(gameObject);
        }
    }

    public void Delete() {
        GetDamage(health);
    }
}
