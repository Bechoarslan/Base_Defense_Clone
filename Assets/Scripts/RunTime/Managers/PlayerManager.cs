using System;
using DG.Tweening;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums;
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
        [SerializeField] private PlayerAnimationController playerAnimationController;

        #region Private Variables
        
        private PlayerMovementData _playerMovementData;
        private readonly string _playerDataPath = "Data/CD_PlayerData";

        private Transform _emptyTransform;
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
            PlayerSignals.Instance.onSetPlayerAnimationState += playerAnimationController.SetPlayerAnimationState;
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret += OnPlayerExitInteractWithTurret;
        }

        private void OnPlayerExitInteractWithTurret()
        {
            PlayerSignals.Instance.onSetPlayerAnimationState?.Invoke(PlayerAnimationState.Run);
            playerMovementController.ExitInteractWithTurret();
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        }


        private void OnPlayerInteractWithTurret(GameObject turretObj)
        {
            PlayerSignals.Instance.onSetPlayerAnimationState?.Invoke(PlayerAnimationState.Hold);
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            playerMovementController.InteractWithTurret(turretObj);
        }


        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputValue(inputParams);
            playerAnimationController.UpdateInputParams(inputParams);
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onPlay -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            PlayerSignals.Instance.onSetPlayerAnimationState -= playerAnimationController.SetPlayerAnimationState;
            PlayerSignals.Instance.onPLayerInteractWithTurret -= OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret -= OnPlayerExitInteractWithTurret;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}