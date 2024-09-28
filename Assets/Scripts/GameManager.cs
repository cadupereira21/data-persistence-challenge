using System;
using System.IO;
using UnityEngine;

public class GameManager {
    private const string SAVE_FILE_NAME = "save.json";

    private static GameManager _instance;

    public static GameManager instance => _instance ??= new GameManager();

    public int playerHighestScore;

    public string playerName;

    private GameManager() { }

    public void SaveGame() {
        SaveData saveData = new SaveData(playerHighestScore);

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, json);
    }

    public void LoadGame() {
        string path = Application.persistentDataPath + SAVE_FILE_NAME;

        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        playerHighestScore = saveData.playerHighestScore;
    }

    [Serializable]
    private class SaveData {
        public int playerHighestScore;

        public SaveData(int playerHighestScore) {
            this.playerHighestScore = playerHighestScore;
        }
    }
}