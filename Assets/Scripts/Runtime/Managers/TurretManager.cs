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

        #region Serialized Variables

        [Header("Transforms")]
        [SerializeField] private Transform turret;
        [SerializeField] private Transform standPoint;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform ammoHolder;

        [Header("Controllers")] 
        [SerializeField] private TurretController turretController;
        #endregion

        #region Private Variables

        [SerializeField]public List<GameObject> enteredEnemies = new List<GameObject>();
        private int _ammoCounter;
        private bool _isHaveSeater;
        private TurretState _turretState = TurretState.None;
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onGetTurretStandPointAndTurretTransform += OnGetTurretStandPoint;
            GameSignals.Instance.onGetTurretHolderTransform += OnGetHolderTransform;
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
                    break;
            }
            _turretState = turretState;
        }

        [Button("Ready To Shoot")]
        public void ReadyToShoot()
        {
            switch (_turretState)
            {
                case TurretState.PlayerIn:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    break;
                case TurretState.AutoTurret:
                    StartCoroutine(turretController.EnemyShooting(ammoHolder));
                    StartCoroutine(turretController.RotateToEnemy(enteredEnemies));
                    break;
                case TurretState.None:
                    StopAllCoroutines();
                    break;
                
            }
        }
        private Transform OnGetHolderTransform() => ammoHolder;
       
        

        private (Transform ,Transform) OnGetTurretStandPoint() =>(turret, standPoint);


       

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onGetTurretStandPointAndTurretTransform -= OnGetTurretStandPoint;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        

        public void AddEnemyToList(GameObject otherGameObject) => enteredEnemies.Add(otherGameObject);

        
    }
}