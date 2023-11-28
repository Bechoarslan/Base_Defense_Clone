using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Controllers.Turret;
using RunTime.Enums.Pool;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretController turretController;
        [SerializeField]private Transform bulletBoxArea;
        [SerializeField] private Transform bulletHolder;
        [SerializeField] private GameObject laser;

        #endregion
        

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret += turretController.OnPlayerExitInteractWithTurret;
        }

        private void OnPlayerInteractWithTurret(GameObject arg0) => turretController.OnPlayerInteractWithTurret(bulletBoxArea,bulletHolder,laser);

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPLayerInteractWithTurret -= OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret -= turretController.OnPlayerExitInteractWithTurret;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}