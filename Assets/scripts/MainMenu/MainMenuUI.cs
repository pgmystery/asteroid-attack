using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuObject;
    [SerializeField]
    private GameObject LevelSelectionObject;
    [SerializeField]
    private Button StartGameButton;
    [SerializeField]
    private Button ShowStoreButton;
    [SerializeField]
    private Button QuitButton;

    void Start() {
        if (MainMenuObject == null) {
            Debug.LogError("MainMenu: No MainMenuObject object referenced!");
        }
        if (LevelSelectionObject == null) {
            Debug.LogError("MainMenu: No LevelSelectionObject object referenced!");
        }
        if (StartGameButton == null) {
            Debug.LogError("MainMenu: No StartGameButton object referenced!");
        }
        if (ShowStoreButton == null) {
            Debug.LogError("MainMenu: No ShowStoreButton object referenced!");
        }
        if (QuitButton == null) {
            Debug.LogError("MainMenu: No QuitButton object referenced!");
        }

        StartGameButton.onClick.AddListener(ShowLevelSelection);
        ShowStoreButton.onClick.AddListener(ShowStore);
        QuitButton.onClick.AddListener(QuitGame);
    }

    void ShowLevelSelection() {
        MainMenuObject.SetActive(false);
        LevelSelectionObject.SetActive(true);
    }

    void ShowStore() {
        SceneManager.LoadScene("Store", LoadSceneMode.Single);
    }

    void QuitGame() {
        Application.Quit();
    }
}
