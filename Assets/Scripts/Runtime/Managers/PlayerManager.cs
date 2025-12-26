using System;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        [SerializeField] private CD_PlayerData playerData;
        #endregion

        #region Private Variables

        private PlayerState _playerState;
        #endregion

        #endregion

        private void Awake()
        {
            SendPlayerDataToControllers();
            ChangePlayerState(PlayerState.Idle);
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.GetPlayerData(playerData);
         
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        
       
            InputSignals.Instance.onInputParamsChanged += OnInputParamsChanged;
            
        }

        private void OnInputParamsChanged(InputParamsKeys inputParams) 
            => playerMovementController.OnInputChanged(inputParams);
        

        private void UnSubscribeEvents()
        {
           
          
            InputSignals.Instance.onInputParamsChanged -= OnInputParamsChanged;
            
           
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void OnStackAmmo(Transform otherTransform, Transform stackHolder, StackType bullet)
        {
            Debug.Log("Stacking Ammo");
            StartCoroutine(GameSignals.Instance.onSendStackObjectToHolder?.Invoke(otherTransform,stackHolder,bullet));
        }

        public void OnSendStackToDeposit(Transform holderTransform, Transform stackHolder, StackType ammo)
        {
            StartCoroutine(GameSignals.Instance.onSendStackObjectToArea?.Invoke(holderTransform, stackHolder, ammo));
        }

        public void ChangePlayerState(PlayerState playerState)
        {
            _playerState = playerState;
           
            playerMovementController.OnStateChanged(_playerState);
        }
    }
}