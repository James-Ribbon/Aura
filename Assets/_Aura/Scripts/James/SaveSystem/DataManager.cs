using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager simpleSave;

    public void SaveData()
    {
        SavedData data = new SavedData();

        //data.currentLevel = GameManager.currentLevel;
        //data.health = GameManager.health;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/gameData.json";
        System.IO.File.WriteAllText(path, json);
    }

    public void LoadData()
    {

        string path = Application.persistentDataPath + "/gameData.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);
            //GameManager.currentLevel = data.currentLevel;
            //GameManager.health = data.health;
        }
    }
}
