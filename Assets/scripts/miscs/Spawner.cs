using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // public GameObject asteroidPrefab = Resources.Load<GameObject>("Assets/ojects/asteroids/asteroid1.prefab");
    // float GameSizeObjectSizeX;
    // float GameSizeObjectSizeY;

    // float randomPosX = .0f;
    // float randomPosY = .0f;
    Vector2 earthPos;

    void Start() {
        // GameObject GameSizeObject = GameObject.Find("GameSize");
        // GameSize GameSizeObject = GameSize.gameSize;
        // GameSizeObjectSizeX = GameSizeObject.transform.localScale.x / 2.0f;
        // GameSizeObjectSizeY = GameSizeObject.transform.localScale.y / 2.0f;
        Earth earth = Earth.earth;
        earthPos = new Vector2(earth.transform.position.x, earth.transform.position.y);
    }

    public GameObject SpawnObject(GameObject objectToSpawn, Vector2 spawnPos, float speed) {
        GameObject objectCopy = Instantiate(objectToSpawn, new Vector3(spawnPos.x, spawnPos.y, .0f), transform.rotation);
        Rigidbody2D objectCopyBody = objectCopy.GetComponent<Rigidbody2D>();

        float distance = Vector2.Distance(spawnPos, earthPos);
        float newSpeed = speed / distance;

        // Debug.Log("");
        // Debug.Log(speed);
        // Debug.Log(distance);

        Vector2 speedPoint = Vector2.Lerp(earthPos, spawnPos, newSpeed);
        // speedPoint.x += randomPosX;
        // speedPoint.y += randomPosY;

        // GameObject reddot = GameObject.Find("reddot");
        // reddot.transform.position = new Vector3(speedPoint.x * -1, speedPoint.y * -1, .0f);

        objectCopyBody.velocity = new Vector2(speedPoint.x * -1, speedPoint.y * -1);
        // asteroidCopyBody.AddForce(speedPoint);

        // Debug.Log(spawnPos);
        // Debug.Log(speed);
        // Debug.Log(newSpeed);

        return objectCopy;
    }
}
