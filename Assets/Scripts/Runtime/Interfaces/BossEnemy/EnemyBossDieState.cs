using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using UnityEngine;

namespace Runtime.Interfaces.BossEnemy
{
    public class EnemyBossDieState : IStateMachine
    {
        private EnemyBossManager Manager;
        
        
        public EnemyBossDieState(EnemyBossManager manager)
        {
            Manager = manager;
        }


        public void EnterState()
        {
            Manager.AnimationSetTrigger(EnemyStateType.Die);
            Debug.Log("Boss Died");
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            Manager.Health = 1000f;
            
        }
    }
}