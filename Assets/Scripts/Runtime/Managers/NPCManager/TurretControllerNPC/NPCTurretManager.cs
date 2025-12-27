using System;
using Runtime.Data.UnityObjects;
using Runtime.Interfaces;
using Runtime.Interfaces.NPCTurretState;
using UnityEngine;

namespace Runtime.Managers.NPCManager.Hostage
{
    public class NPCTurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        private IStateMachine _currentState;
        private TurretShooterState _turretShooterState;
        #endregion

        #endregion

        private void Awake()
        {
            _turretShooterState = new TurretShooterState(this);
            _currentState = _turretShooterState;
        }
        
        private void Start() => _currentState.EnterState();
        
    }
}