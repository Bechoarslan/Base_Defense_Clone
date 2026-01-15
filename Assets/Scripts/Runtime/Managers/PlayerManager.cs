using System;
using System.Collections;
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
        public string SaveKey { get; } = "PlayerData";
        public List<GameObject> EnemyList = new List<GameObject>();
        public List<GameObject> HostageList = new List<GameObject>();
        public float Health { get; set; }
        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerHealthController playerHealthController;
        [SerializeField] private PlayerShootingController playerShootingController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        
        private PlayerState _playerState;
        [SerializeField] private CD_PlayerData playerData;
        #endregion

        #region Private Variables

      
  
        private Coroutine _buyableCoroutine;
        #endregion

        #endregion

        private void Awake()
        {
            SendPlayerDataToControllers();
            OnStateChanged(PlayerState.Idle);
            Health = playerData.PlayerData.Health;
            playerHealthController.SetHealth(Health);
            playerShootingController.OnChangeGun(GunType.Pistol);
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.GetPlayerData(playerData);
            playerShootingController.GetPlayerData(playerData);
         
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {


            InputSignals.Instance.onInputReadyToPlay += playerMovementController.OnInputReadyToPlay;
            PlayerSignals.Instance.onChangePlayerPropertyType += OnChangePlayerPropertyType;
            PlayerSignals.Instance.onChangeGun += playerShootingController.OnChangeGun;
            PlayerSignals.Instance.onEnemyDiedClearFromList += playerShootingController.OnEnemyDiedClearFromDic;
            PlayerSignals.Instance.onChangeAnimBool += playerAnimationController.OnChangeAnimBool;
            PlayerSignals.Instance.onTriggerAnimState += playerAnimationController.OnTriggerAnimation;
            PlayerSignals.Instance.onChangeAnimFloat += playerAnimationController.OnChangeSetAnimFloat;
            PlayerSignals.Instance.onStartShootingCoroutine += playerShootingController.StartShootingCoroutineCaller;
            PlayerSignals.Instance.onGetPlayerTransform += OnGetPlayerTransform;
            PlayerSignals.Instance.onChangeAnimLayer += playerAnimationController.OnChangeBaseLayer;
            PlayerSignals.Instance.onChangePlayerState += OnStateChanged;
            InputSignals.Instance.onInputParamsChanged += OnInputParamsChanged;
            
        }

        private void OnChangePlayerPropertyType(PlayerPropertyType propertyType)
        {
            switch (propertyType)
            {
                case PlayerPropertyType.MoveSpeed:
                    playerData.PlayerData.MoveSpeed += 1;
                    break;
                case PlayerPropertyType.Capacity:
                    playerData.PlayerData.StackLimit += 5;
                    break;
                case PlayerPropertyType.Health:
                    playerData.PlayerData.Health += 20;
                    playerHealthController.SetHealth(playerData.PlayerData.Health);
                    
                    break;
                
            }
        }


        private void OnInputParamsChanged(InputParamsKeys inputParams) 
            => playerMovementController.OnInputChanged(inputParams);
        

        private void UnSubscribeEvents()
        {

            
            InputSignals.Instance.onInputReadyToPlay -= playerMovementController.OnInputReadyToPlay;
            PlayerSignals.Instance.onChangePlayerPropertyType -= OnChangePlayerPropertyType;
            PlayerSignals.Instance.onChangeGun -= playerShootingController.OnChangeGun;
            PlayerSignals.Instance.onChangeAnimBool -= playerAnimationController.OnChangeAnimBool;
            PlayerSignals.Instance.onTriggerAnimState -= playerAnimationController.OnTriggerAnimation;
            PlayerSignals.Instance.onChangeAnimFloat -= playerAnimationController.OnChangeSetAnimFloat;
            PlayerSignals.Instance.onStartShootingCoroutine -= playerShootingController.StartShootingCoroutineCaller;
            PlayerSignals.Instance.onGetPlayerTransform -= OnGetPlayerTransform;
            PlayerSignals.Instance.onChangeAnimLayer -= playerAnimationController.OnChangeBaseLayer;
            PlayerSignals.Instance.onChangePlayerState -= OnStateChanged;
            InputSignals.Instance.onInputParamsChanged -= OnInputParamsChanged;
            
           
            
        }

        private Transform OnGetPlayerTransform() => this.transform;
        
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void OnStackAmmo(Transform otherTransform, Transform stackHolder, StackType bullet)
        {
            
            StartCoroutine(GameSignals.Instance.onSendBulletStackObjectToHolder?.Invoke(otherTransform,stackHolder,bullet, playerData.PlayerData.StackLimit));
        }

        public void OnSendStackToDeposit(Transform holderTransform, Transform stackHolder, StackType ammo)
        {
            StartCoroutine(GameSignals.Instance.onSendBulletStackObjectToArea?.Invoke(holderTransform, stackHolder, ammo));
        }

        public void OnStateChanged(PlayerState playerState)
        {
            if (this._playerState == playerState) return;
            playerMovementController.OnStateChanged(playerState);
           
            switch (playerState)
            {
                case PlayerState.Idle:
                  
                    playerHealthController.OnRegenateHealth();
                    playerAnimationController.OnChangeBaseLayer(1, 0);
                    PlayerSignals.Instance.onChangeAnimLayer?.Invoke(1,0);
                    playerShootingController.StopShootingCoroutineCaller();
                    break;
                case PlayerState.Turret:
                    playerAnimationController.OnChangeAnimBool(true,PlayerAnimState.IsHolding);
                    playerAnimationController.OnTriggerAnimation(PlayerAnimState.Hold);
                  
                 
                    break;
                case PlayerState.Shooting:
                    PlayerSignals.Instance.onChangeAnimLayer?.Invoke(1,1f);
                  playerShootingController.StartShootingCoroutineCaller();
                    
                    break;
            }
            this._playerState = playerState;
        }

     
        public void TakeDamage(float damageAmount) =>
            playerHealthController.OnTakeDamage(damageAmount);

        public void SetHealthVisible(bool value)
        {
            playerHealthController.SetHealthVisible(value);
        }

        public void OnGetMoneyStack(Transform stackHolder, GameObject moneyObj)
        {
            GameSignals.Instance.onSendMoneyStackToHolder?.Invoke(stackHolder, moneyObj, playerData.PlayerData.StackLimit);
        }


        public void OnSetTurretPos(Collider other)
        {
            playerMovementController.OnSetTurretPos(other.gameObject);
        }

      
 
      
    }
}