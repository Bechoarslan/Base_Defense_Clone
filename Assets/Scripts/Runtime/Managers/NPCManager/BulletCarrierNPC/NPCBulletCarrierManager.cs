using System;
using System.Collections;
using Runtime.Controllers.NpcController;
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

        public IStateMachine CurrentState;
        public CD_NpcData npcData;

        #endregion

        #region Serialized Variables

        [SerializeField] public Transform bulletHolder;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private NPCAnimationController animationController; 

        #endregion

        #region Private Variables

        private WaitDepositTurretState _waitDepositTurretState;
        private WaitTakeBulletState _waitTakeBulletState;
        private WalkAmmoAreaState _walkAmmoAreaState;
        private WalkTurretAreaState _walkTurretAreaState;
        
        private BulletCarrierStateType _bulletCarrierStateType = BulletCarrierStateType.None;
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
         
        }

        private void OnEnable()
        {
            SwitchState(_bulletCarrierStateType);
        }

        private void GetReferences()
        {
            _waitDepositTurretState = new WaitDepositTurretState(this);
            _waitTakeBulletState = new WaitTakeBulletState(this,npcData.Data);
            _walkAmmoAreaState = new WalkAmmoAreaState(this,ref navMeshAgent);
            _walkTurretAreaState = new WalkTurretAreaState(this, ref navMeshAgent);
            SwitchState( BulletCarrierStateType.WalkAmmoArea);
            
        }

        public void SwitchState(BulletCarrierStateType waitTakeBullet)
        {
            if(_bulletCarrierStateType == waitTakeBullet) return;
            if (CurrentState != null)
            {
                CurrentState.OnExitState();
            }
            _bulletCarrierStateType = waitTakeBullet;
            switch (_bulletCarrierStateType)
            {
                case BulletCarrierStateType.WaitDepositBullet:
                    CurrentState = _waitDepositTurretState;
                    break;
                case BulletCarrierStateType.WaitTakeBullet:
                    CurrentState = _waitTakeBulletState;
                    break;
                case BulletCarrierStateType.WalkAmmoArea:
                    CurrentState = _walkAmmoAreaState;
                    break;
                case BulletCarrierStateType.WalkTurretArea:
                    CurrentState = _walkTurretAreaState;
                    break;
            }
            Debug.Log(CurrentState.GetType().Name);
            CurrentState.EnterState();
        }


        public void StartCor(IEnumerator invoke)
        {
            StartCoroutine(invoke);
        }

        public void OnSetTriggerAnim(string triggerName)
        {
            animationController.OnTriggerAnimation(triggerName);
        }
    }
}