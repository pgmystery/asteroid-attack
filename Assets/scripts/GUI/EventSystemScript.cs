using UnityEngine;

public class EventSystemScript : MonoBehaviour
{
    public static EventSystemScript eventSystemScript;

    void Awake() {
        if (eventSystemScript == null) {
            // DontDestroyOnLoad(gameObject);
            eventSystemScript = this;
        }
        else if (eventSystemScript != this) {
            Destroy(gameObject);
        }
    }
}
