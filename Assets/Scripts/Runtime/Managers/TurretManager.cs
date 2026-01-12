using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        public Transform  standPoint;
        #region Serialized Variables

        [Header("Transforms")]
        [SerializeField] private Transform turret;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform ammoHolder;

        [Header("Controllers")] 
        [SerializeField] private TurretController turretController;
        #endregion

        #region Private Variables
        
        private int _ammoCounter;
        private bool _isHaveSeater;
       [SerializeField] public TurretState _turretState = TurretState.None;
        #endregion

        #endregion

        private void Start()
        {
            GameSignals.Instance.onSendAmmoStackHolderTransform?.Invoke(ammoHolder);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onEnemyDiedClearFromList += turretController.OnEnemyDiedClearFromList;
            GameSignals.Instance.onTurretStateChange += OnTurretStateChange;
        }

        

        public void OnTurretStateChange(TurretState turretState)
        {
            
            switch (turretState)
            {
                case TurretState.PlayerIn:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    break;
                case TurretState.AutoTurret:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    StartCoroutine(turretController.RotateToEnemy());
                    break;
            }
            _turretState = turretState;
        }

        [Button("Ready To Shoot") ]
        public void ReadyToShoot()
        {
            switch (_turretState)
            {
                case TurretState.PlayerIn:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    break;
                case TurretState.AutoTurret:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    StartCoroutine(turretController.RotateToEnemy());
                    break;
                case TurretState.None:
                    StopAllCoroutines();
                    break;
                
            }
        }
  
       
        


        private void UnSubscribeEvents()
        {
            
            GameSignals.Instance.onTurretStateChange -= OnTurretStateChange;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        

        

        
    }
}