using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Data;
using System;

// https://stackoverflow.com/questions/20441090/how-can-create-touch-screen-android-scroll-in-unity3d
// https://www.youtube.com/watch?v=PZ-s1lTpmTM

public class LevelSelector : MonoBehaviour
{
    // private string levelPath;
    private int levelCount=0;

    private Transform content;
    private GameObject selectedLevelBox;

    [SerializeField]
    private GameObject levelBox;
    [SerializeField]
    private GameObject MainMenuObject;
    [SerializeField]
    private GameObject LevelSelectionObject;
    [SerializeField]
    private GameObject ItemSelectionMenuObject;
    [SerializeField]
    private Button StartGameButton;
    [SerializeField]
    private Button SelectItemsMenuButton;
    [SerializeField]
    private Button GoToMainMenuButton;

    void Start()
    {
        if (levelBox == null) {
            Debug.LogError("LevelSelector: no 'LevelBox'-object referenced!");
        }
        if (MainMenuObject == null) {
            Debug.LogError("LevelSelector: no 'MainMenuObject'-object referenced!");
        }
        if (LevelSelectionObject == null) {
            Debug.LogError("LevelSelector: no 'LevelSelectionObject'-object referenced!");
        }
        if (ItemSelectionMenuObject == null) {
            Debug.LogError("LevelSelector: no 'ItemSelectionMenuObject'-object referenced!");
        }
        if (StartGameButton == null) {
            Debug.LogError("LevelSelector: no 'StartGameButton'-object referenced!");
        }
        if (SelectItemsMenuButton == null) {
            Debug.LogError("LevelSelector: no 'SelectItemsMenuButton'-object referenced!");
        }
        if (GoToMainMenuButton == null) {
            Debug.LogError("LevelSelector: no 'GoToMainMenuButton'-object referenced!");
        }

        StartGameButton.onClick.AddListener(StartGame);
        SelectItemsMenuButton.onClick.AddListener(GoToSelectItemMenu);
        GoToMainMenuButton.onClick.AddListener(GoToMainMenu);

        for (int i=0; i < gameObject.transform.childCount; i++) {
            if (gameObject.transform.GetChild(i).gameObject.name == "Viewport") {
                content = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0);
            }
        }
        if (content == null || content.name != "LevelSelectionContent") {
            Debug.LogError("LevelSelector: Could not find the 'LevelSelectionContent'-Transform in the LevelSelector!");
        }

        // Debug.Log(Application.persistentDataPath + "/asteroid_attack.db");
        DataTable levels = SQLHandler.sqlHandler.RunCommand("SELECT * FROM alevels");
        // levels.Rows[i].ItemArray  // All db items-name (id, name, etc.)
        if (levels.Rows.Count > 0) {
            for (int i=0; i < levels.Rows.Count; i++) {
                GameObject newLevelBox = CreateLevel(levels.Rows[i]["name"].ToString());
                newLevelBox.GetComponent<LevelBoxSelection>().levelNumber = levels.Rows[i]["id"].ToString();
                newLevelBox.GetComponent<LevelBoxSelection>().levelName = levels.Rows[i]["name"].ToString();
                newLevelBox.GetComponent<LevelBoxSelection>().levelUnlocked = levels.Rows[i]["unlocked"].ToString();
                if (levels.Rows[i]["unlocked"].ToString() == "False") {
                    newLevelBox.transform.Find("LevelBoxUnlocked").gameObject.SetActive(true);
                }
                else {
                    newLevelBox.GetComponent<Button>().onClick.AddListener(() => LevelSelectionChanged(newLevelBox));
                    LevelSelectionChanged(newLevelBox);
                }
            }
        }
    }

    private GameObject CreateLevel(string name) {
        GameObject newLevelBox = Instantiate(levelBox, new Vector3(0, 0, .0f), transform.rotation, content);
        newLevelBox.transform.Find("LevelText").GetComponent<Text>().text = name;
        levelCount++;
        return newLevelBox;
    }

    void LevelSelectionChanged(GameObject levelBoxClicked) {
        if (selectedLevelBox != levelBoxClicked) {
            if (selectedLevelBox != null) {
                selectedLevelBox.transform.Find("LevelSelection").gameObject.SetActive(false);
            }
            levelBoxClicked.transform.Find("LevelSelection").gameObject.SetActive(true);
            selectedLevelBox = levelBoxClicked;
        }
    }

    void GoToMainMenu() {
        LevelSelectionObject.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    void StartGame() {
        GameHandler.game.StartGame(Int32.Parse(selectedLevelBox.GetComponent<LevelBoxSelection>().levelNumber));
    }

    void GoToSelectItemMenu() {
        LevelSelectionObject.SetActive(false);
        ItemSelectionMenuObject.SetActive(true);
    }
}
