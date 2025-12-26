using System;
using System.Collections;
using Runtime.Data.UnityObjects;
using Runtime.Enums.NPCState;
using Runtime.Interfaces;
using Runtime.Interfaces.BulletCarrierState;
using Runtime.Interfaces.BulletCarrirerState;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers.NPCManager.Hostage
{
    public class NPCBulletCarrierManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public INPCStateMachine CurrentState;

        #endregion

        #region Serialized Variables

        [SerializeField] public Transform bulletHolder;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] public CD_NpcData npcData;

        #endregion

        #region Private Variables

        private WaitDepositTurretState _waitDepositTurretState;
        private WaitTakeBulletState _waitTakeBulletState;
        private WalkAmmoAreaState _walkAmmoAreaState;
        private WalkTurretAreaState _walkTurretAreaState;
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            CurrentState = _walkAmmoAreaState;
        }

        private void Start()
        {
            CurrentState.EnterState();
        }

        private void GetReferences()
        {
            _waitDepositTurretState = new WaitDepositTurretState(this);
            _waitTakeBulletState = new WaitTakeBulletState(this);
            _walkAmmoAreaState = new WalkAmmoAreaState(this,ref navMeshAgent);
            _walkTurretAreaState = new WalkTurretAreaState(this, ref navMeshAgent);
            
            
        }

        public void SwitchState(BulletCarrierType waitTakeBullet)
        {
            switch (waitTakeBullet)
            {
                case BulletCarrierType.WaitDepositBullet:
                    CurrentState = _waitDepositTurretState;
                    break;
                case BulletCarrierType.WaitTakeBullet:
                    CurrentState = _waitTakeBulletState;
                    break;
                case BulletCarrierType.WalkAmmoArea:
                    CurrentState = _walkAmmoAreaState;
                    break;
                case BulletCarrierType.WalkTurretArea:
                    CurrentState = _walkTurretAreaState;
                    break;
            }
            CurrentState.EnterState();
        }


        public void StartCor(IEnumerator invoke)
        {
            StartCoroutine(invoke);
        }
    }
}