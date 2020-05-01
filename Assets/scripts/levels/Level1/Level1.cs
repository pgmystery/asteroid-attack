using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level1 : LevelHandler
{
    [SerializeField]
    private int maxAsteroidsCounter;

    private bool waitForEnd=false;
    private int createdAsteroidsCounter=0;
    private int currentAsteroidCounter=0;
    private float GameSizeObjectSizeX;
    private float GameSizeObjectSizeY;

    public GameObject asteroidPrefab;
    public Spawner SpawnerScript;

    private GameStartCounter gameStartCounter;
    // private EnemyDestroyEvent enemyDestroyEvent;

    void OnEnable() {
        EventManager.Subscribe("enemyDestroy", EnemyDestroyed);
        // EventManager.StartListening("enemyDestroyed", a => {EnemyDestroyed(GameObject)});
        // EventManager.enemyDestroyEvent.AddListener(EnemyDestroyed);
        // EventManager.enemyDestroyEvent.AddListener(EnemyDestroyed);
    }

    void OnDisable() {
        EventManager.Unsubscribe("enemyDestroy", EnemyDestroyed);
        // EventManager.enemyDestroyEvent.RemoveListener(EnemyDestroyed);
    }

    void Start() {
        GameSize GameSizeObject = GameSize.gameSize;
        GameSizeObjectSizeX = GameSizeObject.transform.localScale.x / 2.0f;
        GameSizeObjectSizeY = GameSizeObject.transform.localScale.y / 2.0f;

        gameStartCounter = GameStartCounter.gameStartCounter;
        StartCoroutine(gameStartCounter.FirstStartCounting(gameStartCounter.startTime, StartLevel));
    }

    void StartLevel() {
        SpawnerScript = GameObject.Find("Ingame").GetComponent<Spawner>();
        // asteroidPrefab = Resources.Load<GameObject>("objects/asteroids/asteroid1.prefab");  // NOT WORKING, NEED TO BE IN THE RESOURCE FOLDER
        InvokeRepeating("SpawnAsteroids", 5.0f, 1.0f);
        // InvokeRepeating("SpawnAsteroids", 0.5f, 9999999999999999999999.0f);
    }

    public new void RestartLevel() {
        // Debug.Log("RESTART_LEVEL");
        // foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("asteroid")) {
        //     Destroy(asteroid);
        // }
        GameHandler.game.StartGame(levelNumber);
    }

    public new void EndLevel() {
        CancelInvoke("SpawnAsteroids");
    }

    void SpawnAsteroids() {
        int alignment = Random.Range(0, 3);

        float randomPosX = 9999999.0F;
        float randomPosY = 9999999.0F;
        if (alignment == 0) {  // TOP
            randomPosX = Random.Range(-GameSizeObjectSizeX, GameSizeObjectSizeX);
            randomPosY = -GameSizeObjectSizeY;
        }
        else if (alignment == 1) {  // RIGHT
            randomPosX = GameSizeObjectSizeX;
            randomPosY = Random.Range(-GameSizeObjectSizeY, GameSizeObjectSizeY);
        }
        else if (alignment == 2) {  // BOTTOM
            randomPosX = Random.Range(-GameSizeObjectSizeX, GameSizeObjectSizeX);
            randomPosY = GameSizeObjectSizeY;
        }
        else if (alignment == 3) {  // LEFT
            randomPosX = -GameSizeObjectSizeX;
            randomPosY = Random.Range(-GameSizeObjectSizeY, GameSizeObjectSizeY);
        }
        Vector2 spawnPos = new Vector2(randomPosX, randomPosY);

        SpawnerScript.SpawnObject(asteroidPrefab, spawnPos, Random.Range(0.1f, 10.0f));

        createdAsteroidsCounter++;
        currentAsteroidCounter++;
        if (createdAsteroidsCounter >= maxAsteroidsCounter) {
            CancelInvoke("SpawnAsteroids");
            // TODO: END LEVEL WITH A WIN!? OR CHECK IF ALL DESTROYED???
            waitForEnd = true;
        }
    }

    void EnemyDestroyed(object[] parameters) {
    // void EnemyDestroyed() {
        currentAsteroidCounter--;
        if (waitForEnd && currentAsteroidCounter == 0) {
            // EndLevel();
            Debug.Log("LEVEL FINISHED!");
        }
    }
}
