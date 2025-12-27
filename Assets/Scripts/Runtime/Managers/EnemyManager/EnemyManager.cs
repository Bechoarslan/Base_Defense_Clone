using System;
using Runtime.Data.UnityObjects;
using Runtime.Enums.EnemyStateType;
using Runtime.Interfaces;
using Runtime.Interfaces.EnemyState;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers.EnemyManager
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform WalkPointTransform;

        #endregion
        #region Serialized Variables

        [SerializeField] private CD_EnemyData enemyData;
        [SerializeField] private NavMeshAgent navMeshAgent;

        #endregion

        #region Private Variables

        private EnemyWalkState _enemyWalkState;
        private EnemyRunState _enemyRunState;
        private EnemyDieState _enemyDieState;
        private EnemyAttackState _enemyAttackState;
        
        private IStateMachine _currentState;
        
        private EnemyStateType _currentStateType;
        
        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            _currentStateType = EnemyStateType.Move;
        }

        private void Start()
        {
            
            OnEnemyChangeState(_currentStateType);
        }

        private void GetReferences()
        {
            _enemyWalkState = new EnemyWalkState(this,navMeshAgent);
            _enemyRunState = new EnemyRunState(this,navMeshAgent);
            _enemyDieState = new EnemyDieState(this,navMeshAgent);
            _enemyAttackState = new EnemyAttackState(this,navMeshAgent);
        }
        
        
        public void OnEnemyChangeState(EnemyStateType stateType)
        {
            _currentStateType = stateType;
            switch (stateType)
            {
                case EnemyStateType.Move:
                    _currentState = _enemyWalkState;
                    break;
                case EnemyStateType.Run:
                    _currentState = _enemyRunState;
                    break;
                case EnemyStateType.Die:
                    _currentState = _enemyDieState;
                    break;
                case EnemyStateType.Attack:
                    _currentState = _enemyAttackState;
                    break;
              
                    
            }
            _currentState.EnterState();
        }
    }
}