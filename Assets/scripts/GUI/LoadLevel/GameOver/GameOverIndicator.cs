using UnityEngine;
using UnityEngine.UI;

public class GameOverIndicator : MonoBehaviour
{
    public static GameOverIndicator gameOverIndicator;

    [SerializeField]
    private Button TryAgainButton;
    [SerializeField]
    private Button BackToMenuButton;

    private IngameHandler ingameHandler;

    void Awake() {
        this.gameObject.SetActive(false);
        if (gameOverIndicator == null) {
            gameOverIndicator = this;
        }
        else if (gameOverIndicator != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (TryAgainButton == null) {
            Debug.LogError("GameOverIndicator: No TryAgainButton object referenced!");
        }        
        if (BackToMenuButton == null) {
            Debug.LogError("GameOverIndicator: No BackToMenuButton object referenced!");
        }

        ingameHandler = IngameHandler.ingameHandler;

        TryAgainButton.onClick.AddListener(TryAgain);
        BackToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void ShowGameOver(bool mode) {
        PauseMenuIndicator.pauseMenuIndicator.gameObject.SetActive(!mode);
        gameObject.SetActive(mode);
        GamePointsIndicator.gamePointsIndicator.GetComponent<Animator>().enabled = true;
    }

    void TryAgain() {
        // if (ingameHandler.currentLevel != null) {
        //     gameObject.SetActive(false);
        //     ingameHandler.currentLevel.RestartLevel();
        // }
        GameHandler.game.StartGame(ingameHandler.currentLevel.levelNumber);
    }

    void BackToMenu() {
        GameHandler.game.GoToMenu();
    }
}
