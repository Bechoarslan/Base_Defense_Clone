using System;
using DG.Tweening;
using Runtime.Controllers.NpcController.Enemy;
using Runtime.Data.UnityObjects;
using Runtime.Enums.EnemyStateType;
using Runtime.Interfaces;
using Runtime.Interfaces.EnemyState;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers.EnemyManager
{
    public class EnemyManager : MonoBehaviour,IDamageable
    {
        #region Self Variables

        public float Health { get; set; } = 100;
        #region Public Variables

        public Transform Target;
        public IStateMachine _currentState ;

        #endregion
        #region Serialized Variables

        [SerializeField] private GameObject enemyCollider;
        [SerializeField] private CD_EnemyData enemyData;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private EnemyAnimationController enemyAnimationController;
        [SerializeField] private SkinnedMeshRenderer enemyMaterial;

        #endregion

        #region Private Variables

        private EnemyWalkState _enemyWalkState;
        private EnemyRunState _enemyRunState;
        private EnemyDieState _enemyDieState;
        private EnemyAttackState _enemyAttackState;
        private EnemyAttackWallState _enemyAttackWallState;
        
       
        private EnemyStateType _currentStateType;

        private bool _isDead;
        
        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            _currentStateType = EnemyStateType.Move;
        }

       
        private void OnEnable()
        {
            OnEnemyChangeState(_currentStateType);
        }

        private void GetReferences()
        {
            _enemyWalkState = new EnemyWalkState(this,navMeshAgent,enemyAnimationController);
            _enemyRunState = new EnemyRunState(this,navMeshAgent,enemyAnimationController);
            _enemyDieState = new EnemyDieState(this,navMeshAgent,enemyAnimationController);
            _enemyAttackState = new EnemyAttackState(this,navMeshAgent,enemyAnimationController);
            _enemyAttackWallState = new EnemyAttackWallState(this,navMeshAgent,enemyAnimationController);
            
        }
        
        
        public void OnEnemyChangeState(EnemyStateType stateType)
        {
            if (_currentState != null)
            {
               
                _currentState.OnExitState();
            }
            _currentStateType = stateType;
            switch (_currentStateType)
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
                case EnemyStateType.AttackWall:
                    _currentState = _enemyAttackWallState;
                    break;
              
                    
            }
            _currentState.EnterState();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Target = null;
        }

        private void Update()
        {
            if (_isDead) return;
            _currentState.UpdateState();
           
        }

       
        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            var currentColor = enemyMaterial.material.color;
            enemyMaterial.material.DOColor( Color.gray, 0.5f ).OnComplete(() =>
            {
                enemyMaterial.material.DOColor( currentColor, 0.5f );
            });
            if (!(Health <= 0)) return;
            enemyCollider.tag = "Untagged";
            PlayerSignals.Instance.onEnemyDiedClearFromList?.Invoke(gameObject);
            _isDead = true;
            OnEnemyChangeState(EnemyStateType.Die);
        }
        
        public void OnStopCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }
    }
}