using UnityEngine;
using System.IO;

public static class JsonSaveLoad
{
    static string FilePath => Application.persistentDataPath + "/10.JsonData/";

    public static void Save(Data _Data, string _Path)
    {
        if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);

        string JsonData = JsonUtility.ToJson(_Data, true);
        string JsonFilePath = FilePath + _Path + ".Json";
        File.WriteAllText(JsonFilePath, JsonData);
    }

    public static Data Load(string _FileName)
    {
        string JsonFilePath = FilePath + _FileName + ".Json";

        if (!File.Exists(JsonFilePath)) return null;

        string SaveFile = File.ReadAllText(JsonFilePath);
        Data SaveData = JsonUtility.FromJson<Data>(SaveFile);

        return SaveData;
    }
}