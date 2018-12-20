using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class IndexJSON : MonoBehaviour
{
    private PlayerData _playerData;
    public TMP_InputField NameTextInput;

    private void Start()
    {
        _playerData = new PlayerData();

        Load();

        UpdatePlayerNameInput();
    }

    private void UpdatePlayerNameInput()
    {
        NameTextInput.text = _playerData.Name;
    }

    private const string PlayerDataPath = "/json.txt";

    private void Save()
    {
        var json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(Application.dataPath + PlayerDataPath, json);

        print("Save to " + Application.dataPath + PlayerDataPath + " " + json);
    }

    private void Load()
    {
        if (!File.Exists(Application.dataPath + PlayerDataPath)) return;

        var json = File.ReadAllText(Application.dataPath + PlayerDataPath);
        _playerData = JsonUtility.FromJson<PlayerData>(json);

        print("Loaded from " + Application.dataPath + PlayerDataPath + " " + json);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 36), "Load"))
        {
            Load();
            UpdatePlayerNameInput();
        }

        if (GUI.Button(new Rect(10, 50, 150, 36), "Save"))
        {
            _playerData.Name = NameTextInput.text;
            Save();
        }
    }

    public static void Save<T>(T data, string path)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + path, json);
    }

    public static T Load<T>(string path)
    {
//        if (!File.Exists(Application.dataPath + path)) return new <T>();

        var json = File.ReadAllText(Application.dataPath + path);
        return JsonUtility.FromJson<T>(json);

    }
}

public class PlayerData
{
    public string Name;
    public int Coins;
    public int Points;
}