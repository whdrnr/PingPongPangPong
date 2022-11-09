using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonManager : Singleton<JsonManager>
{
    public Data Data
    {
        get
        {
            if(GameManager.Instance.Data == null)
            {
                LoadGameData();
                SaveGameData();
            }

            return GameManager.Instance.Data;
        }
    }

    [ContextMenu("Load")]
    public void LoadGameData()
    {
        string FilePath = Path.Combine(Application.dataPath, "Project/10.JsonData/PlayerData.json");

        if(File.Exists(FilePath) == true)
        {
            string FromJsonData = File.ReadAllText(FilePath);
            GameManager.Instance.Data = JsonUtility.FromJson<Data>(FromJsonData);
        }
        else
        {
            GameManager.Instance.Data = new Data();
        }
    }

    [ContextMenu("Save")]
   public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(Data, true);
        string FilePath = Path.Combine(Application.dataPath, "Project/10.JsonData/PlayerData.json");
        File.WriteAllText(FilePath, ToJsonData);
    }
}
