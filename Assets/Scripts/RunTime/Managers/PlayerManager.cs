using System;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        #region Private Variables
        
        private PlayerMovementData _playerMovementData;
        private readonly string _playerDataPath = "Data/CD_PlayerData";
        #endregion

        #endregion

        private void Awake()
        {
            _playerMovementData = GetPlayerData();
            SendDataToMovementController(_playerMovementData);
        }

        private void SendDataToMovementController(PlayerMovementData playerMovementData)
        {
            playerMovementController.GetMovementDataFromManager(playerMovementData);
        }

        private PlayerMovementData GetPlayerData() => Resources.Load<CD_PlayerData>(_playerDataPath).movementData;
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onPlay += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputValue(inputParams);
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onPlay -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}