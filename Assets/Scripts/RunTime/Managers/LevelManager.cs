using System;
using RunTime.Commands.LevelCommands;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEditor.iOS;
using UnityEngine;

namespace RunTime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] internal GameObject levelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoaderCommand;
        private LevelDestroyerCommand _levelDestroyCommand;
        private CD_LevelData _levelData;
        [ShowInInspector] private int _levelId;
        [ShowInInspector] private int _loadLevelId;
        private readonly string _levelDataPath = "Data/CD_LevelData";
        
        #endregion

        #endregion

        private void Awake()
        {
            Init();
            _levelData = GetLevelData();
        }

        private CD_LevelData GetLevelData()
        {
            return Resources.Load<CD_LevelData>(_levelDataPath);
        }

        private int CalculateLevelId()
        {
            _loadLevelId = _levelId % _levelData.Data.Count;
            return _loadLevelId;
        }


        private void Init()
        {
            _levelLoaderCommand = new LevelLoaderCommand(ref levelHolder);
            _levelDestroyCommand = new LevelDestroyerCommand(ref levelHolder);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel += () => _levelDestroyCommand.Execute();
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;

        }

        private void OnLevelInitialize()
        {
            _levelLoaderCommand.Execute(CalculateLevelId());
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnNextLevel()
        {
            _levelId++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel -= () => _levelDestroyCommand.Execute();
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Start()
        {
            OnLevelInitialize();
        }
    }
}