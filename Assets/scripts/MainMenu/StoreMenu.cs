using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreMenu : MonoBehaviour
{
    [SerializeField]
    private Button BackToMenuButton;

    void Start() {
        if (BackToMenuButton == null) {
            Debug.LogError("StoreMenu: No 'BackToMenuButton'-object referenced!");
        }

        BackToMenuButton.onClick.AddListener(BackToMenu);
    }

    void BackToMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
