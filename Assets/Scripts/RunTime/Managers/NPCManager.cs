using System;
using System.Collections;
using RunTime.Enums.NPC;
using RunTime.Enums.Pool;
using RunTime.Interfaces;
using RunTime.Signals;
using RunTime.State;
using RunTime.State.Enemy;
using RunTime.State.Hostage;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.Managers
{
    public class NPCManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        [SerializeField] private NPCEnums npcType;
      

        #endregion

        #region Private Variables
        
        public INpcStates _iNpcStates;

        private EnemyStartState _enemyStartState;
        private EnemyRunPlayerState _enemyRunPlayerState;
        private EnemyAttackState _enemyAttackState;
        private HostageStartState _hostageStartState;
        

        #endregion


        #endregion

        private void Awake()
        {
            _enemyStartState = new EnemyStartState(ref navMeshAgent, ref animator,this);
            _enemyRunPlayerState = new EnemyRunPlayerState(ref navMeshAgent, ref animator, this);
            _enemyAttackState = new EnemyAttackState(ref navMeshAgent, ref animator, this);
            Init();
            
        }

        private void Init()
        {

            switch (npcType)
            {
                case NPCEnums.Enemy:
                    _iNpcStates = _enemyStartState;
                    break;
                case NPCEnums.Hostage:
                    _iNpcStates = _hostageStartState;
                    break;
                
                
            }
        }

        private void OnEnable()
        {
            _iNpcStates.StartAction();
      
        }

       

      
        public void ChangeCurrentState(NPCStateType stateType)
        {
            switch (stateType)
            {
                case NPCStateType.EnemyRun:
                    _iNpcStates = _enemyRunPlayerState;
                    break;
                case NPCStateType.EnemyWalk:
                    _iNpcStates = _enemyStartState;
                    break;
                case NPCStateType.EnemyAttack:
                    _iNpcStates = _enemyAttackState;
                    break;
                
            }
            
            _iNpcStates.StartAction();
        }
        
        private void OnDisable()
        {
            
        }

        private void FixedUpdate()
        {
            _iNpcStates.UpdateAction();
        }
    }
}