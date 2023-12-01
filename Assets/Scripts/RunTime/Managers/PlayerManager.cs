using System;
using Cinemachine;
using DG.Tweening;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using RunTime.Enums.Camera;
using RunTime.Enums.Gun;
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
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private StackManager stackManager;

        #region Private Variables
        
        private PlayerData _playerData;
        private readonly string _playerDataPath = "Data/CD_PlayerData";
        private Transform _emptyTransform;
        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SendDataToMovementController(_playerData);
  
        }
        
        private void SendDataToMovementController(PlayerData playerData)
        {
            playerMovementController.GetMovementDataFromManager(playerData);
        }
        
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>(_playerDataPath).data;
        
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
            PlayerSignals.Instance.onSetPlayerAnimation += playerAnimationController.SetPlayerAnimationState;
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret += OnPlayerExitInteractWithTurret;
            PlayerSignals.Instance.onPlayerInteractEnterArea += OnPlayerInteractEnterArea;
            PlayerSignals.Instance.onSetAnimationLayer += OnSetAnimationLayer;
            PlayerSignals.Instance.onPlayerInteractExitArea += OnPlayerInteractExitArea;
        }

        
        private void OnSetAnimationLayer(PlayerAnimLayer layer,short weight) => playerAnimationController.SetAnimationLayer(layer,weight);
        
        private void OnPlayerInteractExitArea()
        {
            PlayerSignals.Instance.onSetAnimationLayer?.Invoke(PlayerAnimLayer.Outside,1);
        }


        private void OnPlayerInteractEnterArea()
        {
            PlayerSignals.Instance.onSetAnimationLayer?.Invoke(PlayerAnimLayer.Outside,0);
        }

        private void OnPlayerExitInteractWithTurret()
        {
            PlayerSignals.Instance.onSetPlayerAnimation?.Invoke(PlayerAnimationState.Run);
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraEnums.Start);
        }


        private void OnPlayerInteractWithTurret(GameObject turretObj)
        {
            PlayerSignals.Instance.onSetPlayerAnimation?.Invoke(PlayerAnimationState.Hold);
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
            PlayerSignals.Instance.onSetPlayerAnimation -= playerAnimationController.SetPlayerAnimationState;
            PlayerSignals.Instance.onPLayerInteractWithTurret -= OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret -= OnPlayerExitInteractWithTurret;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}