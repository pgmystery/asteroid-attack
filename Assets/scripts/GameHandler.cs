using UnityEngine;
using UnityEngine.SceneManagement;
// using System.Data;
using System.IO;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour
{
    public static GameHandler game;
    public static GameDataHandler gameData;
    // public int gamePoints = 0;
    public int currentLevelNumber = 1;
    public Dictionary<string, dynamic> settings=new Dictionary<string, dynamic>();

    public IngameHandler ingameHandler;
    public SQLHandler sql;
    public JSONHandler json;

    private float normalTimeScale;
    [SerializeField]
    private int numberOfLevels;

    void Awake() {
        if (game == null) {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else if (game != this) {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    void Start() {
        sql = SQLHandler.sqlHandler;
        json = JSONHandler.jsonHandler;

        string JSONData = json.LoadFile(Application.persistentDataPath + "/game.json");
        if (JSONData != null) {
            gameData = JsonUtility.FromJson<GameDataHandler>(JSONData);
        }
        else {
            gameData = new GameDataHandler();
        }

        // load all levels
        
        // while (levels.Read()) {
        //     Debug.Log(levels[1].ToString());
        // }

        normalTimeScale = Time.timeScale;
    }

    public void StartGame(int levelNumber) {
        // if (ingameHandler != null) {
        //     // TODO: Remove old Level !!!
        // }
        currentLevelNumber = levelNumber;
        SceneManager.LoadScene("LoadLevel", LoadSceneMode.Single);
    }

    public void GoToMenu() {
        Time.timeScale = normalTimeScale;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void ActiveSceneChanged(Scene current, Scene nextScene) {
        if (nextScene.name == "LoadLevel") {
            Time.timeScale = normalTimeScale;
            // ingameHandler = GameObject.Find("Ingame").GetComponent<IngameHandler>();
            ingameHandler = IngameHandler.ingameHandler;
            ingameHandler.LoadLevel(currentLevelNumber);
        }
    }

    public void AddPoints(int points) {
        gameData.gamePoints += points;
        gameData.SaveGameData();
    }
}


public class GameDataHandler
{
    public int gamePoints=0;

    public void SaveGameData() {
        string gameDataJSON = JsonUtility.ToJson(this);
        using (StreamWriter sw = File.CreateText(Application.persistentDataPath + "/game.json")) {
            sw.Write(gameDataJSON);
        }
    }
}
