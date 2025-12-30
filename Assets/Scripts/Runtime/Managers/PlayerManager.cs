using System;
using System.Collections.Generic;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour,IDamageable
    {
        #region Self Variables

        public List<GameObject> EnemyList = new List<GameObject>();
        public float Health { get; set; }
        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerHealthController playerHealthController;
        
        public PlayerState playerState;
        [SerializeField] private CD_PlayerData playerData;
        #endregion

        #region Private Variables

  
        #endregion

        #endregion

        private void Awake()
        {
            SendPlayerDataToControllers();
            ChangePlayerState(PlayerState.Idle);
            Health = playerData.PlayerData.Health;
            playerHealthController.SetHealth(Health);
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
        
       
            PlayerSignals.Instance.onGetPlayerTransform += OnGetPlayerTransform;
            InputSignals.Instance.onInputParamsChanged += OnInputParamsChanged;
            
        }

        private void OnInputParamsChanged(InputParamsKeys inputParams) 
            => playerMovementController.OnInputChanged(inputParams);
        

        private void UnSubscribeEvents()
        {

            PlayerSignals.Instance.onGetPlayerTransform += OnGetPlayerTransform;
            InputSignals.Instance.onInputParamsChanged -= OnInputParamsChanged;
            
           
            
        }

        private Transform OnGetPlayerTransform() => this.transform;
        
        
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
            this.playerState = playerState;
           
            playerMovementController.OnStateChanged(this.playerState);
        }

     
        public void TakeDamage(float damageAmount) =>
            playerHealthController.OnTakeDamage(damageAmount);

        public void SetHealthVisible(bool value)
        {
            playerHealthController.SetHealthVisible(value);
        }
    }
}