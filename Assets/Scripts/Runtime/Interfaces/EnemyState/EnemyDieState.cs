using Runtime.Controllers.NpcController.Enemy;
using Runtime.Managers.EnemyManager;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyDieState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        public EnemyDieState(EnemyManager enemyManager, NavMeshAgent navMeshAgent,
            EnemyAnimationController enemyAnimationController)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
           
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
            
        }
    }
}