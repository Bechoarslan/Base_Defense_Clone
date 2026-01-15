using System;
using Runtime.Controllers.NpcController;
using Runtime.Controllers.NpcController.Enemy;
using Runtime.Enums.EnemyStateType;
using Runtime.Interfaces;
using Runtime.Interfaces.BossEnemy;
using Runtime.Signals;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Managers.EnemyManager
{
    public class EnemyBossManager : MonoBehaviour,IDamageable
    {
        #region Self Variables

        public float Health { get; set; } = 1000f;
        public IStateMachine _currentState;
        public Transform target;
        public Transform grenadeHolder;
        #region Serialized Variables

        [SerializeField] private EnemyAnimationController npcAnimationController;
        [SerializeField] private TextMeshPro healthText;

        #endregion

        #region Private Variables

        private EnemyBossWaitState _enemyBossWaitState;
        private EnemyBossStartThrowGrenade _enemyBossStartThrowGrenade;
        private EnemyBossDieState _enemyBossDieState;

        private bool _isDead;
        private EnemyBossStateType _enemyStateType;

        #endregion

        #endregion


        private void Awake()
        {
            SetStates();
           SetHealthText();
        }

        private void SetHealthText()
        {
            healthText.text = Health.ToString();
        }

        private void SetStates()
        {
            _enemyBossWaitState = new EnemyBossWaitState(this);
            _enemyBossStartThrowGrenade = new EnemyBossStartThrowGrenade(this);
            _enemyBossDieState = new EnemyBossDieState(this);
            
        }

        private void OnEnable()
        {
            SwitchState(EnemyBossStateType.WaitState);
        }

        private void OnDisable()
        {
           
        }

        public void SwitchState(EnemyBossStateType enemyStateType)
        {
            if (_currentState != null)
            {
                _currentState.OnExitState();
            }

            _enemyStateType = enemyStateType;
            switch (_enemyStateType)
            {
                
                case EnemyBossStateType.WaitState:
                    _currentState = _enemyBossWaitState;
                    break;
                case EnemyBossStateType.BossThrowGrenadeState:
                    _currentState = _enemyBossStartThrowGrenade;
                    break;
                case EnemyBossStateType.BossDieState:
                    _currentState = _enemyBossDieState;
                    break;
            }
            _currentState.EnterState();
        }

       
        public void AnimationSetTrigger(EnemyStateType trigger)
        {
            npcAnimationController.OnSetTriggerAnimation(trigger);
        }

        public void TakeDamage(float damageAmount)
        {
           
            if (Health <= 0 && !_isDead)
            {
                _isDead = true;
                Debug.Log("Boss Died");
                PlayerSignals.Instance.onEnemyDiedClearFromList?.Invoke(gameObject);
                SwitchState(EnemyBossStateType.BossDieState);
                return;
            }
            Health -= damageAmount;
            SetHealthText();
           
            
        }
    }
}