using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonManager : Singleton<JsonManager>
{
    [ContextMenu("Load")]
    public void LoadGameData()
    {
        Debug.Log("Load");
        string FilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        string FromJsonData = File.ReadAllText(FilePath);
        GameManager.Instance.Data = JsonUtility.FromJson<Data>(FromJsonData);
    }

    [ContextMenu("Save")]
   public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(GameManager.Instance.Data, true);
        string FilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        File.WriteAllText(FilePath, ToJsonData);
    }
}
