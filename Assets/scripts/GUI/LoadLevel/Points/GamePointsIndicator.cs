using UnityEngine;
using UnityEngine.UI;

public class GamePointsIndicator : MonoBehaviour
{
    [SerializeField]
    private Text gamePoints;

    public static GamePointsIndicator gamePointsIndicator;

    void Awake() {
        if (gamePointsIndicator == null) {
            gamePointsIndicator = this;
        }
        else if (gamePointsIndicator != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (gamePoints == null) {
            Debug.LogError("GAME_PONTS INDICATOR: No gamePoints object referenced!");
        }
    }

    public void SetPoints(int points) {
        gamePoints.text = points + " Points";
    }
}
