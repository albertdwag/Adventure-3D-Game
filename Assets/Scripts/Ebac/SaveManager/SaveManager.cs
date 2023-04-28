using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private string _path = Application.streamingAssetsPath + "/save.txt";
    [SerializeField] private SaveSetup _saveSetup;

    public Action<SaveSetup> FileLoaded;
    public int lastLevel;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        ResetItems();
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.checkpoint = CheckpointManager.Instance.LastCheckpointKey;
        _saveSetup.currentLife = Player.Instance.healthBase.CurrentLife;
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.healthPacks = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;

        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    private void SaveFile(string json)
    {

        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
    }

    private void ResetItems()
    {
        _saveSetup.lastLevel = 0;
        _saveSetup.checkpoint = 0;
        _saveSetup.playerName = "Albert";
        _saveSetup.currentLife = 100;
        _saveSetup.coins = 0;
        _saveSetup.healthPacks = 0;
    }

    private void OnDestroy()
    {
        ResetItems();
    }
    #endregion
}

[Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int checkpoint;
    public string playerName;
    public float currentLife;
    public int coins;
    public int healthPacks;
}
