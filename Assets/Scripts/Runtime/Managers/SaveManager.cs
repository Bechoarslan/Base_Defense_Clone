using System.Collections.Generic;
using Runtime.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [Header("Save Settings")]
    [SerializeField] private string defaultSaveFile = "save.es3";

    // SaveKey -> ISaveable
    private readonly Dictionary<string, ISaveable> _saveables = new();

    #region Unity Lifecycle

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RegisterAllSaveables();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    #endregion

    #region Register

    /// <summary>
    /// Sahnedeki tüm ISaveable objeleri toplar
    /// </summary>
    public void RegisterAllSaveables()
    {
        _saveables.Clear();

        MonoBehaviour[] allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);

        foreach (var mono in allMonoBehaviours)
        {
            if (mono is not ISaveable saveable)
                continue;

            string key = saveable.SaveKey;

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError($"[SaveManager] Empty SaveKey on {mono.name}");
                continue;
            }

            if (_saveables.ContainsKey(key))
            {
                Debug.LogError($"[SaveManager] Duplicate SaveKey detected: {key}");
                continue;
            }

            _saveables.Add(key, saveable);
        }
    }

    #endregion

    #region Save / Load (Default)

    public void SaveGame()
    {
        SaveGameInternal(defaultSaveFile);
    }

    [Button("Load Game")]
    public void LoadGame()
    {
        LoadGameInternal(defaultSaveFile);
    }

    #endregion

    #region Save / Load (Slot)

    public void SaveGame(string slotName)
    {
        SaveGameInternal($"{slotName}.es3");
    }

    public void LoadGame(string slotName)
    {
        LoadGameInternal($"{slotName}.es3");
    }

    #endregion

    #region Core Logic

    private void SaveGameInternal(string fileName)
    {
        foreach (var pair in _saveables)
        {
            ES3.Save(pair.Key, pair.Value.CaptureState(), fileName);
        }

        Debug.Log($"[SaveManager] Game Saved -> {fileName}");
    }

    private void LoadGameInternal(string fileName)
    {
        foreach (var pair in _saveables)
        {
            if (!ES3.KeyExists(pair.Key, fileName))
                continue;

            object state = ES3.Load<object>(pair.Key, fileName);
            pair.Value.RestoreState(state);
        }

        Debug.Log($"[SaveManager] Game Loaded -> {fileName}");
    }

    #endregion

    #region Utility

    public bool HasSave()
    {
        return ES3.FileExists(defaultSaveFile);
    }

    public void DeleteSave()
    {
        ES3.DeleteFile(defaultSaveFile);
        Debug.Log("[SaveManager] Save Deleted");
    }

    #endregion
}
