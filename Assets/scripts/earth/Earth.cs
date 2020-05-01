using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public static Earth earth;
    public int health = 100;

    [SerializeField]
    private EarthDamageIndicator earthDamageIndicator;

    void Awake() {
        if (earth == null) {
            // DontDestroyOnLoad(gameObject);
            earth = this;
        }
        else if (earth != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (earthDamageIndicator == null) {
            Debug.LogError("EARTH: No EarthDamageIndicator set!");
        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.GetComponent<Enemy>() != null) {
            GetDamage(collider.gameObject.GetComponent<Enemy>().damage);
            Destroy(collider.gameObject);
        }
    }

    void GetDamage(int damageNumber) {
        health -= Mathf.Max(damageNumber, 0);
        earthDamageIndicator.SetHealth(health);
        if (health <= 0) {
            Destroy(this.gameObject);
            IngameHandler.ingameHandler.EndLevel();
        }
    }
}
