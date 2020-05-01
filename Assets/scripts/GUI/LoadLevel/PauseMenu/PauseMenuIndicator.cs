using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuIndicator : MonoBehaviour
{
    public static PauseMenuIndicator pauseMenuIndicator;

    public bool pause = false;

    [SerializeField]
    private Button pauseMenuButton;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject mainPauseMenu;
    [SerializeField]
    private GameObject yesNoMenu;

    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button restartLevelButton;
    [SerializeField]
    private Button backToMenuButton;

    [SerializeField]
    private Button yesNoMenuYesButton;
    [SerializeField]
    private Button yesNoMenuNoButton;

    private IngameHandler ingameHandler;

    private float normalTimeScale;
    private Action yesNoMenuCallback;

    void Awake() {
        pauseMenu.SetActive(false);
        if (pauseMenuIndicator == null) {
            pauseMenuIndicator = this;
        }
        else if (pauseMenuIndicator != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (pauseMenuButton == null) {
            Debug.LogError("PauseMenuIndicator: No PauseMenuButton object referenced!");
        }

        if (pauseMenu == null) {
            Debug.LogError("PauseMenuIndicator: No PauseMenu object referenced!");
        }

        if (mainPauseMenu == null) {
            Debug.LogError("PauseMenuIndicator: No mainPauseMenu object referenced!");
        }
        if (yesNoMenu == null) {
            Debug.LogError("PauseMenuIndicator: No yesNoMenu object referenced!");
        }

        if (continueButton == null) {
            Debug.LogError("PauseMenuIndicator: No ContinueButton object referenced!");
        }
        if (restartLevelButton == null) {
            Debug.LogError("PauseMenuIndicator: No RestartLevelButton object referenced!");
        }
        if (backToMenuButton == null) {
            Debug.LogError("PauseMenuIndicator: No BackToMenuButton object referenced!");
        }

        if (yesNoMenuYesButton == null) {
            Debug.LogError("PauseMenuIndicator: No yesNoMenuYesButton object referenced!");
        }
        if (yesNoMenuNoButton == null) {
            Debug.LogError("PauseMenuIndicator: No yesNoMenuNoButton object referenced!");
        }

        normalTimeScale = Time.timeScale;

        ingameHandler = IngameHandler.ingameHandler;

        pauseMenuButton.onClick.AddListener(PauseMenuButtonClicked);
        continueButton.onClick.AddListener(Continue);
        restartLevelButton.onClick.AddListener(RestartButtonClicked);
        backToMenuButton.onClick.AddListener(BackToMenuButtonClicked);

        // YesNoMenu
        yesNoMenuYesButton.onClick.AddListener(YesNoMenuYesClicked);
        yesNoMenuNoButton.onClick.AddListener(YesNoMenuNoClicked);
    }

    void PauseMenuButtonClicked() {
        if (!pause) {
            Pause();
        }
        else {
            Continue();
        }
    }

    public void Pause() {
        Time.timeScale = .0f;
        pause = true;
        pauseMenu.SetActive(true);
    }

    public void Continue() {
        pauseMenu.SetActive(false);
        pause = false;
        Time.timeScale = normalTimeScale;
    }

    private void RestartButtonClicked() {
        ActivateYesNoMenu(true, "Do you really want to restart the level?", RestartLevel);
    }

    private void RestartLevel() {
        GameHandler.game.StartGame(ingameHandler.currentLevel.levelNumber);
    }

    private void BackToMenuButtonClicked() {
        ActivateYesNoMenu(true, "Do you really want to go back to the menu?", BackToMenu);
    }

    void BackToMenu() {
        GameHandler.game.GoToMenu();
    }

    void ActivateYesNoMenu(bool status, string title, Action callback) {
        yesNoMenuCallback = callback;
        mainPauseMenu.SetActive(!status);
        if (title.Length > 0) {
            yesNoMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = title;
        }
        yesNoMenu.SetActive(status);
    }

    void YesNoMenuYesClicked() {
        yesNoMenuCallback();
    }

    void YesNoMenuNoClicked() {
        ActivateYesNoMenu(false, "", null);
    }
}
