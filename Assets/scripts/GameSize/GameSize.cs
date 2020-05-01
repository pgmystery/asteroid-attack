using UnityEngine;
using System.Collections;

public class GameSize : MonoBehaviour
{
    public static GameSize gameSize;

    void Awake() {
        if (gameSize == null) {
            // DontDestroyOnLoad(gameObject);
            gameSize = this;
        }
        else if (gameSize != this) {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Enemy>()) {
            collider.gameObject.GetComponent<Enemy>().Delete();
        }
        else {
            Destroy(collider.gameObject);
        }
    }
}
