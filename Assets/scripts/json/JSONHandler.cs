using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class JSONHandler : MonoBehaviour
{
    public static JSONHandler jsonHandler;

    void Awake() {
        if (jsonHandler == null) {
            jsonHandler = this;
        }
        else if (jsonHandler != this) {
            Destroy(gameObject);
        }
    }

    public string LoadFile(string filePath) {
        if (!File.Exists(filePath)) {
            return null;
        }
        string JSONData = File.ReadAllText(filePath);
        return JSONData;
    }

    public void SaveFile(string JSONText, string filePath) {
        using (StreamWriter sw = File.CreateText(filePath)) {
            sw.Write(JSONText);
        }
    }
}
