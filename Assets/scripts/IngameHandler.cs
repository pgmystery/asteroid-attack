using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameHandler : MonoBehaviour
{
    public static IngameHandler ingameHandler;
    public int gamePointsAll = 0;
    public int gamePoints = 0;
    public int currentLevelNumber = 1;
    public bool dead = false;
    public LevelHandler currentLevel;

    public delegate void OnGamePointsChangedDelegate(int gamePoints);
    public static event OnGamePointsChangedDelegate OnGamePointsChanged;

    void Awake() {
        if (ingameHandler == null) {
            // DontDestroyOnLoad(gameObject);
            ingameHandler = this;
        }
        else if (ingameHandler != this) {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public void LoadLevel(int levelNumber) {
        currentLevelNumber = levelNumber;
        SceneManager.LoadScene("Level" + levelNumber, LoadSceneMode.Additive);
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Level" + currentLevelNumber) {
            for (int i=0; i < scene.GetRootGameObjects().Length; i++) {
                if (scene.GetRootGameObjects()[i].name == "level") {
                    currentLevel = scene.GetRootGameObjects()[i].GetComponent<LevelHandler>();
                    break;
                }
            }
        }
    }

    public void SetPoints(int points) {
        if (!dead) {
            if (points > gamePoints) {
                gamePointsAll = points;  //TODO: DO I WANT THIS???
            }
            gamePoints = points;
            ChangeGamePoints();
        }
    }

    public void AddPoints(int points) {
        if (!dead) {
            gamePointsAll += points;
            gamePoints += points;
            ChangeGamePoints();
        }
    }

    public void RemovePoints(int points) {
        if (!dead) {
            gamePoints -= points;
            ChangeGamePoints();
        }
    }

    private void ChangeGamePoints() {
        GamePointsIndicator.gamePointsIndicator.SetPoints(gamePoints);
        OnGamePointsChanged(gamePoints);
    }

    public void EndLevel() {
        dead = true;
        GamePointsIndicator.gamePointsIndicator.SetPoints(gamePointsAll);  // TODO:
        GameOverIndicator.gameOverIndicator.ShowGameOver(true);
    }
}
